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

        [HttpPost]
        public ActionResult SelectActivityType (SelectActivityTypeModel viewModel)
        {
            using (OQDevSNAPEntities dbContext = new OQDevSNAPEntities())
            {

            }
            return null;
        }

        public ActionResult SelectGroupProgram (SelectGroupProgramModel viewModel)
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