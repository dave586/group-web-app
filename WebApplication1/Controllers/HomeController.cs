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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SelectPrePostSession()
        {
            return View();
        }

        public ActionResult SelectGroupProgram(SelectGroupProgramModel viewModel)
        {
            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {

            }
            return View("~/Views/SelectGroupProgram.cshtml", new SelectGroupProgramModel
            {
                ProgramID = viewModel.ProgramID,
                ProgramName = viewModel.ProgramName
            });
        }
    }
}