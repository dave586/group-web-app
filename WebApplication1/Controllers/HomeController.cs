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
        public ActionResult SelectGroupProgram(SelectGroupProgramModel programModel)
        {
            var groupProgram = SelectGroupProgramModel.GenerateSelectGroupProgramModel(programModel.ProgramID, programModel.ProgramName);
            string programName = groupProgram.Programs[0].ProgramName;
            GroupProgram pro = _dbContext.GroupPrograms.FirstOrDefault(p => p.ProgramName == programName);

            groupProgram.ProgramID = programModel.ProgramID;
            groupProgram.ProgramName = programModel.ProgramName;
            groupProgram.ProgramDisplayName = programModel.ProgramDisplayName;
            return View("~/Views/Home/SelectGroupProgram.cshtml", groupProgram);
            //if (pro == null)
            //    return View("~/Views/Shared/Error.cshtml", new ErrorModel { ErrorType = ErrorTypes.TestNotFound });
            //var testController = new TestController();
            //testController.HomeContext = this.Request.RequestContext;

            //return testController.StartTest(new ClientSelectedProgram { ProgramID = programModel.ProgramID, ClientID = programModel.ClientID, SelectedProgram = new string[] { pro.ProgramID.ToString() } });

        }

        public ActionResult Restart()
        {
            return View();
        }
    }
}