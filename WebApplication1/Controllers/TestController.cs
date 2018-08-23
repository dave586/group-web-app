using System;
using System.Collections.Generic;
//using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using WebApplication1.Models;
using GroupQuestionnaireApp;
using GroupQuestionnaireApp.EFModel;
using GroupQuestionnaireApp.Signals;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class TestController : BaseController
    {
        private RequestContext _homeContext;

        public RequestContext HomeContext
        {
            set { _homeContext = value; }
        }

        public ActionResult StartTest (ClientSelectedProgram viewModel)
        {
			int proID = 0;
            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
				//ClientProgramDisplay cpd = new ClientProgramDisplay();
				//cpd.programID = 1;
				//cpd.Activities = GetPackageActivities(cpd.programID);

				foreach (string selectedProgram in viewModel.SelectedProgram)
				{
					int.TryParse(selectedProgram, out proID);
				}
			}
            return NextTest(proID);
        }

        private IEnumerable<Package> GetPackageActivities (int programID)
        {
			return GroupPackageRepository.GetPackageActivities(programID).AsEnumerable();
        }

        private IEnumerable<Question> GetTestQuestions (string questionnaireType, string language)
        {
            return QuestionnaireRepository.GetQuestionnaireQuestions(questionnaireType, language).AsEnumerable();
        }

        public ActionResult NextTest(int programID)
        {
            ModelState.Clear();
            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
                ClientTestDisplay ctd = new ClientTestDisplay();

				ctd.Package = GetPackageActivities(programID);

				//for (int i = 0; i < ctd.Package.Count(); i++)
				//{
				//	ctd.TestID = ctd.Package.ElementAt(i).ActivityID;
				//}
                ctd.TestID = "1"; //Change it so TestID is not hard coded
                //ctd.Questions = GetTestQuestions(ctd.TestID, "en-CA");
                return View("~/Views/Test/CompleteTest.cshtml", ctd);
            }
        }

        [HttpPost]
        public ActionResult SubmitTest (string data)
        {
            ClientTestDisplay responseData = JsonConvert.DeserializeObject<ClientTestDisplay>(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return null;
        }
    }
}