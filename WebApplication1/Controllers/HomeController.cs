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
    public class HomeController : Controller
    {
        private OQDevSNAPEntities _dbContext = null;
        public HomeController()
        {
            _dbContext = new OQDevSNAPEntities();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SelectGroupProgram(SelectGroupProgramModel viewModel)
        {
            var programSelection = SelectGroupProgramModel.GenerateSelectGroupProgramModel(viewModel.ProgramID, viewModel.ProgramName);

            string programName = programSelection.Programs[0].ProgramName;

            GroupProgram pro = _dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == programName);

            var testController = new TestController();
            testController.HomeContext = this.Request.RequestContext;
            return testController.StartTest(new ClientSelectedProgram { ClientID = viewModel.ClientID, IntakeFileID = viewModel.IntakeFileID, SelectedProgram = new string[] { pro.ID.ToString()} });
        }

        public ActionResult Restart()
        {
            return View();
        }
    }
}