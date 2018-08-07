using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Filters;
using Questionnaire.Models;
using SnapFramework;
using SnapFramework.EFModel;
using SnapFramework.Signals;


namespace Questionnaire.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class HomeController : BaseController
    {
        private CCCSnapEntities _dbContext = null;
        public HomeController()
        {
            _dbContext = new CCCSnapEntities();
        }

        #region Language Selection

        public ActionResult Index()
        {
            return View(GetLanguageSelect());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Language)
        {

            if (SetLanguage(Language))
            {
                return RedirectToAction("ValidateClient");
            }

            ModelState.AddModelError("SelectLanguageError", Resources.Home.Views.Resource.SelectLanguageError);
            return View("Index", GetLanguageSelect());
        }

        private LanguageSelect GetLanguageSelect()
        {
            try
            {
                return new LanguageSelect()
                {
                    LanguageList = _dbContext.QuestionnaireLanguages.ToList()
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool SetLanguage(string language)
        {
            if (!String.IsNullOrWhiteSpace(language) && _dbContext.QuestionnaireLanguages.FirstOrDefault(l => l.Language == language) != null)
            {
                SetupCulture(language, this.Request.RequestContext);
                return true;
            }

            return false;
        }

        #endregion


        #region Validate Client
        
        public ActionResult ValidateClient()
        {
            return View(new ValidateClient(){SessionTimeStamp = DateTime.Now});
        }

        [HttpPost]
        [SessionTimeStampFilter]
        public ActionResult ReValidateClient(ValidateClient val)
        {
            if (val.ClientBD_Day.GetValueOrDefault(0) > 0 && val.ClientBD_Month.GetValueOrDefault(0) > 0 && val.ClientBD_Year.GetValueOrDefault(0) > 0)
            {
                DateTime temp = new DateTime(val.ClientBD_Year.Value,val.ClientBD_Month.Value,val.ClientBD_Day.Value);
                val.ClientBirthDate = temp;
            }

            return View("ValidateClient", val);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionTimeStampFilter]
        public ActionResult ValidateClient(ValidateClient val)
        {
            if (ModelState.IsValid)
            {
                var results = SnapFramework.Signals.ClientValidation.ValidatePrimaryInformation(val.ClientFirstName, val.ClientLastName, val.ClientBirthDate.Value);
                if (results == ClientValidation.ValidationResults.Error)
                {
                    return View("~/Views/Shared/Error.cshtml", new ErrorModel
                        {
                            ErrorType = ErrorTypes.ClientNotFound, 
                            ClientBD_Day = val.ClientBD_Day,
                            ClientBD_Month = val.ClientBD_Month,
                            ClientBD_Year = val.ClientBD_Year,
                            ClientBirthDate = val.ClientBirthDate,
                            ClientFirstName = val.ClientFirstName,
                            ClientLastName = val.ClientLastName
                        });
                }
                
                ValidateChallenge chal = new ValidateChallenge();
                chal.ClientFirstName = val.ClientFirstName;
                chal.ClientLastName = val.ClientLastName;
                chal.ClientBirthDate = val.ClientBirthDate.Value;
                chal.ClientBD_Day = val.ClientBD_Day.Value;
                chal.ClientBD_Month = val.ClientBD_Month.Value;
                chal.ClientBD_Year = val.ClientBD_Year.Value;
                chal.SessionTimeStamp = DateTime.Now;

                chal.Results = results;
                return View("ValidateSecondary", chal);
            }

            return View("ValidateClient", val);
        }

        #endregion


        #region Validate Challenge

        [HttpPost]
        [SessionTimeStampFilter]
        public ActionResult ValidateChallenge(ValidateChallenge chal)
        {
            int clientID = ClientValidation.ValidateChallenge(chal.ClientFirstName, chal.ClientLastName,chal.ClientBirthDate, chal.Results,chal.ChallengeResponse);
            if (clientID == 0)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorModel
                {
                    ErrorType = ErrorTypes.ClientNotFound,
                    ClientBD_Day = chal.ClientBD_Day,
                    ClientBD_Month = chal.ClientBD_Month,
                    ClientBD_Year = chal.ClientBD_Year,
                    ClientBirthDate = chal.ClientBirthDate,
                    ClientFirstName = chal.ClientFirstName,
                    ClientLastName = chal.ClientLastName
                });
            }

            List<IntakeFile> clientIntakes = ClientValidation.GetClientIntakes(clientID);

            // if exactly one open/assigned intake for the client, show test selection view
            if (clientIntakes.Count() == 1)
            {
                return View("SelectPrePostSession", new SelectPrePostSessionModel
                    {
                        ClientID = clientID, 
                        IntakeID = clientIntakes[0].IntakeFileID, 
                        ClientFirstName = chal.ClientFirstName, 
                        ClientLastName = chal.ClientLastName, 
                        ClientBD_Day = chal.ClientBD_Day, 
                        ClientBD_Month = chal.ClientBD_Month, 
                        ClientBD_Year = chal.ClientBD_Year,
                        SessionTimeStamp = DateTime.Now,
                        LanguageList = _dbContext.QuestionnaireLanguages.ToList()
                });
            }

            if (clientIntakes.Count() > 1)
            {
                // if more than one intake for the client, show select intake view
                var model = SelectCounsellorModel.GetSelectCounsellorModel(clientID, clientIntakes);
                model.ClientFirstName = chal.ClientFirstName;
                model.ClientLastName = chal.ClientLastName;
                model.ClientBD_Day = chal.ClientBD_Day;
                model.ClientBD_Month = chal.ClientBD_Month;
                model.ClientBD_Year = chal.ClientBD_Year;
                model.SessionTimeStamp = DateTime.Now;
                return View("SelectCounsellor", model);
            }

            return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.IntakesNotFound });
        }

        #endregion


        #region Select Pre Post Session

        //public ActionResult SelectPrePostSession(int ClientID, int IntakeID)
        //{
        //    using (CCCSnapEntities dbContext = new CCCSnapEntities())
        //    {
        //        IntakeClientRole icr = dbContext.IntakeClientRoles.Include("IntakeFile").FirstOrDefault(c => c.ClientID == ClientID && c.IntakeFileID == IntakeID);
        //        // if no client+intake, return error view
        //        if (icr == null)
        //            return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.IntakesNotFound });

        //        return View(new SelectPrePostSessionModel{ ClientID = ClientID, IntakeID = IntakeID });
        //    }
        //}

        private bool IsOQAndUricaOnly(SelectTestModel model)
        {
            bool val = true;

            foreach (QuestionnaireType test in model.Tests)
            {
                if (test.QuestionnaireName != "OQ" && test.QuestionnaireName != "URICA")
                {
                    val = false;
                }
            }
            
            return val;
        }

        [HttpPost]
        [SessionTimeStampFilter]
        public ActionResult SelectPrePostSession(SelectPrePostSessionModel selectModel)
        {
            if (!selectModel.IsPreSession && !selectModel.IsPostSession)
            {
                selectModel.LanguageList = _dbContext.QuestionnaireLanguages.ToList();
                return View(selectModel);
            }

            SetLanguage(selectModel.Language);

            //check for a previous response created today of pre or post type
            var respID = ClientQuestionnaireResponseRepository.ResponseExists(selectModel.IntakeID, selectModel.ClientID, selectModel.IsPreSession);
            if (respID != Guid.Empty)
                return new TestController().NextTest(respID, this.Request.RequestContext);

            if (selectModel.IsPreSession)
            {
                var presessiontests = SelectTestModel.GenerateSelectTestModel(selectModel.ClientID, selectModel.IntakeID, selectModel.IsPreSession);
                if (presessiontests.Tests.Count == 1)
                {
                    string testname = presessiontests.Tests[0].QuestionnaireName;
                    QuestionnaireType test = _dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == testname);
                    if (test == null)
                        return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.TestNotFound });

                    var testController = new TestController();
                    testController.HomeContext = this.Request.RequestContext;
                    return testController.StartTest(new ClientSelectedTests { ClientID = selectModel.ClientID, IntakeFileID = selectModel.IntakeID, SelectedTests = new string[] { test.ID.ToString() }, IsPreSession = selectModel.IsPreSession });
                }

                // added logic to check if OQ and URICA are only test options - if so, preselect them 
                if (IsOQAndUricaOnly(presessiontests))
                {
                    List<string> testIds = new List<string>();

                    foreach (QuestionnaireType test in presessiontests.Tests)
                    {
                        QuestionnaireType t = _dbContext.QuestionnaireTypes.FirstOrDefault(q => q.QuestionnaireName == test.QuestionnaireName);
                        if (t == null)
                        {
                            return View("~/Views/Shared/Error.cshtml",
                                        new ErrorModel {ErrorType = ErrorTypes.TestNotFound});
                        }
                        else
                        {
                            testIds.Add(t.ID.ToString());
                        }

                    }

                    string[] tests = testIds.ToArray();

                    var testController = new TestController();
                    testController.HomeContext = this.Request.RequestContext;
                    return testController.StartTest(new ClientSelectedTests { ClientID = selectModel.ClientID, IntakeFileID = selectModel.IntakeID, SelectedTests = tests, IsPreSession = selectModel.IsPreSession });
                }

                presessiontests.ClientFirstName = selectModel.ClientFirstName;
                presessiontests.ClientLastName = selectModel.ClientLastName;
                presessiontests.ClientBD_Day = selectModel.ClientBD_Day;
                presessiontests.ClientBD_Month = selectModel.ClientBD_Month;
                presessiontests.ClientBD_Year = selectModel.ClientBD_Year;
                return View("~/Views/Test/SelectTest.cshtml", presessiontests);
            }

            var postSessionTests = SelectTestModel.GenerateSelectTestModel(selectModel.ClientID, selectModel.IntakeID, selectModel.IsPreSession);
            if (postSessionTests.Tests.Count == 1)
            {
                string testname = postSessionTests.Tests[0].QuestionnaireName;
                QuestionnaireType test = _dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == testname);
                if (test == null)
                    return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.TestNotFound });

                var testController = new TestController();
                testController.HomeContext = this.Request.RequestContext;
                return testController.StartTest(new ClientSelectedTests { ClientID = selectModel.ClientID, IntakeFileID = selectModel.IntakeID, SelectedTests = new string[] { test.ID.ToString() }, IsPreSession = selectModel.IsPreSession });
            }
            postSessionTests.ClientFirstName = selectModel.ClientFirstName;
            postSessionTests.ClientLastName = selectModel.ClientLastName;
            postSessionTests.ClientBD_Day = selectModel.ClientBD_Day;
            postSessionTests.ClientBD_Month = selectModel.ClientBD_Month;
            postSessionTests.ClientBD_Year = selectModel.ClientBD_Year;
            return View("~/Views/Test/SelectTest.cshtml", postSessionTests);
        }

        #endregion

        #region Client Override

        public ActionResult ValidateClientID()
        {
            return View("ValidateClientId", new OverrideClient());
        }

        #endregion Client Override


        public ActionResult Restart()
        {
            return View();
        }
    }
}
