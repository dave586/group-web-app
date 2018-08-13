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

            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
                foreach (string selectedTest in viewModel.SelectedProgram)
                {

                }
            }
            return View("~/Views/Home/SelectGroupProgram.cshtml", new SelectGroupProgramModel
            {
                //ProgramID = viewModel.ProgramID,
                //ProgramName = viewModel.ProgramName
            });
        }

        public ActionResult NextTest (Guid responseID, RequestContext _requestContext)
        {
            ModelState.Clear();
            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {
                //GroupProgram pro = dbContext.GroupPrograms.Include('GroupPackageActivity').Include('GroupActivityType').FirstOrDefault()

                ClientTestDisplay ctd = new ClientTestDisplay();
                return View("~/Views/Test/CompleteTest.cshtml", ctd);
            }
        }
    }
}