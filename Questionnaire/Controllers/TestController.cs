using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Questionnaire.Models;
using SnapFramework;
using SnapFramework.EFModel;
using System.Web.Script.Serialization;
using SnapFramework.Signals;
using SnapReports;

namespace Questionnaire.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class TestController : BaseController
    {
        private RequestContext _homeContext;

        public RequestContext HomeContext {
            set { _homeContext = value; }
        }

        [HttpPost]
        public ActionResult SelectTest(SelectTestModel viewModel)
        {
            using (CCCSnapEntities dbContext = new CCCSnapEntities())
            {
                IntakeClientRole icr = dbContext.IntakeClientRoles.Include("IntakeFile").FirstOrDefault(c => c.ClientID == viewModel.ClientID && c.IntakeFileID == viewModel.IntakeFileID && (c.IntakeFile.Status == 3 || c.IntakeFile.Status == 4));
                // if no client+intake, return error view
                if (icr == null)
                    return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.IntakesNotFound });

                var respCheck = ClientQuestionnaireResponseRepository.ResponseExists(icr.IntakeFileID, icr.ClientID, viewModel.IsPreSession);
                if (respCheck != Guid.Empty)
                    return NextTest(respCheck, this.Request.RequestContext);

                //return View("SelectTest", SelectTestModel.GenerateSelectTestModel(icr.ClientID, icr.IntakeFileID, viewModel.IsPreSession));
                return View("~/Views/Home/SelectPrePostSession.cshtml", new SelectPrePostSessionModel
                {
                    ClientID = viewModel.ClientID,
                    IntakeID = viewModel.IntakeFileID,
                    LanguageList = dbContext.QuestionnaireLanguages.ToList()
                });
            }
        }

        public ActionResult StartTest(ClientSelectedTests viewModel)
        {
            // create response record
            using (CCCSnapEntities dbContext = new CCCSnapEntities())
            {
                IntakeClientRole icr = dbContext.IntakeClientRoles.Include("IntakeFile").FirstOrDefault(c => c.ClientID == viewModel.ClientID && c.IntakeFileID == viewModel.IntakeFileID);
                // if no client+intake, return error view
                if (icr == null)
                    return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.IntakesNotFound });

                ClientQuestionnaireResponse resp = new ClientQuestionnaireResponse();
                //resp.ID = Guid.NewGuid();
                resp.ID = UrbanLighthouse.Shared.Statics.NewUniqueGUID();
                resp.IsPreSession = viewModel.IsPreSession;

                //if(!String.IsNullOrWhiteSpace(viewModel.SRSTest))
                //    viewModel.SelectedTests = new string[1]{viewModel.SRSTest};

                foreach (string selectedTest in viewModel.SelectedTests)
                {
                    ClientQuestionnaireTest test = new ClientQuestionnaireTest();

                    int testID = 0;
                    int.TryParse(selectedTest, out testID);

                    test.ID = UrbanLighthouse.Shared.Statics.NewUniqueGUID();

                    if (testID > 1000) // this is the YOQ-P test for the client listed
                    {
                        test.QuestionnaireType = dbContext.QuestionnaireTypes.FirstOrDefault(t => t.QuestionnaireName == "YOQ-P");
                        test.ChildClientId = testID;
                    }
                    else
                    {
                        test.QuestionnaireType = dbContext.QuestionnaireTypes.FirstOrDefault(m => m.ID == testID);
                    }

                    

                    if (test.QuestionnaireType != null)
                    {
                        resp.ClientQuestionnaireTests.Add(test);
                    }
                }
                    
                resp.IsComplete = false;
                resp.DateStarted = DateTime.Now;
                    
                icr.ClientQuestionnaireResponses.Add(resp);

                try
                {

                dbContext.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw;
                }

                return NextTest(resp.ID, this.Request != null ? this.Request.RequestContext : _homeContext);
            }
        }

        [HttpPost]
        public ActionResult NextTest(Guid responseId, RequestContext _requestContext)
        {
            ModelState.Clear();
            using (CCCSnapEntities dbContext = new CCCSnapEntities())
            {
                ClientQuestionnaireResponse resp = dbContext.ClientQuestionnaireResponses.Include("ClientQuestionnaireTests.ClientQuestionnaireAnswers").Include("IntakeClientRole").FirstOrDefault(m => m.ID == responseId);
                if (resp != null)
                {
                    // get the first test that doesn't have any responses
                    ClientQuestionnaireTest nextTest =  resp.ClientQuestionnaireTests.FirstOrDefault(m => m.ClientQuestionnaireAnswers.Count == 0);

                    if (nextTest != null)
                    {
                        // load client name for test display 
                        

                        ClientTestDisplay ctd = new ClientTestDisplay();
                        ctd.TestID = nextTest.TestID;
                        ctd.ResponseID = responseId;
                        if (nextTest.TestID == 1)
                        {
                            ctd.RightToLeftLanguage = System.Globalization.CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;
                        }
                        ctd.ClientQuestionnaireTestId = nextTest.ID;
                        if (nextTest.ChildClientId.HasValue)
                        {
                            Client cl = dbContext.Clients.FirstOrDefault(c => c.ClientID == nextTest.ChildClientId);
                            if (cl != null)
                            {
                                ctd.ClientDisplayName = cl.FirstName + " " + cl.LastName;
                            }
                        }

                        string lang = "en-CA";
                        if(nextTest.TestID == 1)
                        {
                            lang = GetCulture(_requestContext ?? this.Request.RequestContext);
                        }
                        
                        ctd.Questions = GetTestQuestions(nextTest.TestID, lang);
                        
                        return View("~/Views/Test/CompleteTest.cshtml", ctd );
                    }
                    
                    // done all tests
                    resp.IsComplete = true;
                    resp.DateCompleted = DateTime.Now;

                    dbContext.SaveChanges();

                    var complete = new CompleteTestModel { IntakeID = resp.IntakeClientRole.IntakeFileID.ToString(), ClientID = resp.IntakeClientRole.ClientID.ToString() };

                    var test = resp.ClientQuestionnaireTests.FirstOrDefault(t => t.TestID == 1);
                    complete.ShowOQChart = test != null;

                    return View("ResponseComplete", complete);
                }

                return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.TestNotFound });
            }
        }


        private IEnumerable<Question> GetTestQuestions(int TestID, string language)
        {
            var qs = QuestionnaireRepository.GetQuestionnaireQuestions(TestID, language);
            return qs.AsEnumerable();
        }

        [HttpPost]
        public ActionResult SubmitTest(string data)
        {
            ClientTestDisplay responseData = JsonConvert.DeserializeObject<ClientTestDisplay>(data, new JsonSerializerSettings
                                                                                                  {
                                                                                                      TypeNameHandling =TypeNameHandling.Auto
                                                                                                  });

            bool updated = true;
            //prevent resubmitting test results
            if (!ClientTestRepository.GetTestAnswers(responseData.ClientQuestionnaireTestId).Any())
            {
                List<ClientQuestionnaireAnswer> answers = new List<ClientQuestionnaireAnswer>();

                foreach (Question q in responseData.Questions)
                {
                    ClientQuestionnaireAnswer ans = new ClientQuestionnaireAnswer();
                    //ans.ID = Guid.NewGuid();
                    ans.ID = UrbanLighthouse.Shared.Statics.NewUniqueGUID();
                    ans.QuestionID = q.QuestionID;
                    ans.Value = q.Answer;
                    ans.NoAnswer = string.IsNullOrEmpty(q.Answer);
                    ans.TestID = responseData.TestID;
                    answers.Add(ans);
                }


                string lang = GetCulture(this.Request.RequestContext);
                updated = ClientTestRepository.UpdateClientTest(responseData.ClientQuestionnaireTestId, responseData.TestID, answers, lang);

                if (updated && responseData.TestID == 1)
                {
                    QueueOQPrintJob(responseData.ResponseID);
                }// 3 or 4 for YOQ-P/YOQSR

                if(updated && responseData.TestID == 3)
                {
                    
                    QueueYoqPPrintJob(responseData.ResponseID, responseData.ClientQuestionnaireTestId);
                }
                if (updated && responseData.TestID == 4)
                {
                    QueueYoqSRPrintJob(responseData.ResponseID, responseData.ClientQuestionnaireTestId);
                }
            }

            if (updated)
            {
                return NextTest(responseData.ResponseID, this.Request.RequestContext);
            }
            return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.TestNotSaved });
        }

        private void QueueOQPrintJob(Guid responseID)
        {
            ClientQuestionnaireResponse resp = ClientQuestionnaireResponseRepository.GetQuestionnaireResponse(responseID);

            if (resp.IntakeClientRole != null)
            {
                
                OQAreaChart rpt = new OQAreaChart();
                Telerik.Reporting.InstanceReportSource r = new Telerik.Reporting.InstanceReportSource();
                rpt.ReportParameters["IntakeID"].Value = resp.IntakeClientRole.IntakeFileID;
                rpt.ReportParameters["ClientID"].Value = resp.IntakeClientRole.ClientID;
                
                r.ReportDocument = rpt;
                
                //PrintChartJob.PrintChart(r);
                ThreadPool.QueueUserWorkItem(s => PrintChartJob.PrintChart(r, resp.IntakeClientRole.IntakeFileID));
            }
        }

        private void QueueYoqPPrintJob(Guid responseID, Guid testId)
        {
            ClientQuestionnaireResponse resp = ClientQuestionnaireResponseRepository.GetQuestionnaireResponse(responseID);
            ClientQuestionnaireTest test = ClientQuestionnaireResponseRepository.GetClientQuestionnaireTest(testId);

            if (resp.IntakeClientRole != null && test != null)
            {

                YOQAreaChart rpt = new YOQAreaChart("YOQ-P");
                Telerik.Reporting.InstanceReportSource r = new Telerik.Reporting.InstanceReportSource();
                rpt.ReportParameters["IntakeID"].Value = resp.IntakeClientRole.IntakeFileID;
                rpt.ReportParameters["ClientID"].Value = resp.IntakeClientRole.ClientID;
                rpt.ReportParameters["ChildClientID"].Value = test.ChildClientId;

                r.ReportDocument = rpt;

                //PrintChartJob.PrintChart(r);
                ThreadPool.QueueUserWorkItem(s => PrintChartJob.PrintChart(r, resp.IntakeClientRole.IntakeFileID));
            }
        }

        private void QueueYoqSRPrintJob(Guid responseID, Guid testId)
        {
            ClientQuestionnaireResponse resp = ClientQuestionnaireResponseRepository.GetQuestionnaireResponse(responseID);
            ClientQuestionnaireTest test = ClientQuestionnaireResponseRepository.GetClientQuestionnaireTest(testId);

            if (resp.IntakeClientRole != null && test != null)
            {

                YOQAreaChart rpt = new YOQAreaChart("YOQ-SR");
                Telerik.Reporting.InstanceReportSource r = new Telerik.Reporting.InstanceReportSource();
                rpt.ReportParameters["IntakeID"].Value = resp.IntakeClientRole.IntakeFileID;
                rpt.ReportParameters["ClientID"].Value = resp.IntakeClientRole.ClientID;
                rpt.ReportParameters["ChildClientID"].Value = null;

                r.ReportDocument = rpt;

                //PrintChartJob.PrintChart(r);
                ThreadPool.QueueUserWorkItem(s => PrintChartJob.PrintChart(r, resp.IntakeClientRole.IntakeFileID));
            }
        }

        [HttpPost]
        public ActionResult OpenChart(OpenChartModel viewModel)
        {

            ClientQuestionnaireResponse resp = ClientQuestionnaireResponseRepository.GetQuestionnaireResponse(viewModel.ResponseId);

            if (resp != null)
            {
                OQAreaChart rpt = new OQAreaChart();
                Telerik.Reporting.InstanceReportSource r = new Telerik.Reporting.InstanceReportSource();
                rpt.ReportParameters["IntakeID"].Value = resp.IntakeClientRole.IntakeFileID;
                rpt.ReportParameters["ClientID"].Value = resp.IntakeClientRole.ClientID;
                r.ReportDocument = rpt;

                Telerik.Reporting.Processing.ReportProcessor reportProcessor =
                    new Telerik.Reporting.Processing.ReportProcessor();

                Telerik.Reporting.Processing.RenderingResult renderingResult = reportProcessor.RenderReport("PDF", rpt,
                    null);

                MemoryStream ms = new MemoryStream();
                ms.Write(renderingResult.DocumentBytes, 0, renderingResult.DocumentBytes.Length);
                ms.Flush();

                FileContentResult result = new FileContentResult(ms.GetBuffer(), renderingResult.MimeType);

                result.FileDownloadName = "";

                return result;
            }
            else
            {

                return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.Exception });
            }
        }


        //public ActionResult TestNotes()
        //{
        //    new NotificationProcessor().ProcessSMSQueue();
        //    return View();
        //}
    }
}
