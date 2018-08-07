using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Models;

namespace Questionnaire.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index(Questionnaire.Models.ErrorModel model)
        {
            return View("~/Views/Shared/Error.cshtml", model);
        }

    }
}
