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

            return NextTest(proID, this.Request != null ? this.Request.RequestContext : _homeContext);
        }

        private IEnumerable<Package> GetPackageActivities (int programID)
        {
			return GroupPackageRepository.GetPackageActivities(programID).AsEnumerable();
        }

		private int GetTestID (ClientTestDisplay ctd)
		{
			int testID = 0;

			// Trying to get the individual testID's out of the ctd.Package object and display the appropriate questionnaire 
			for (int i = 0; i < ctd.Package.Count(); i++)
			{
				testID = int.Parse(ctd.Package.ElementAt(i).ActivityID);
			}

			return testID;
		}

        public ActionResult NextTest(int programID, RequestContext _requestContext)
        {
            ModelState.Clear();
            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
                ClientTestDisplay ctd = new ClientTestDisplay();

				ctd.Package = GetPackageActivities(programID);
				ctd.TestID = GetTestID(ctd);

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

            return View("~/Views/Test/ResponseComplete.cshtml");
        }
    }
}