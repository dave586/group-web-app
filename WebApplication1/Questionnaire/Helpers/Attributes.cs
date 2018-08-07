using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Questionnaire.Models;

namespace Questionnaire.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidBirthDateAttribute : ValidationAttribute, IClientValidatable
    {
        public ValidBirthDateAttribute(): base("")
        {

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime test = new DateTime();
                if (!DateTime.TryParse(value.ToString(), out test))
                {
                    return new ValidationResult(Resources.Home.Views.Resource.BirthDateInvalid);
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var clientValidationRule = new ModelClientValidationRule()
            {
                ErrorMessage = Resources.Home.Views.Resource.BirthDateInvalid,
                ValidationType = "validbirthdate"
            };

            return new[] { clientValidationRule };
        }
    }

    //public class ValidYearAttribute : ValidationAttribute, IClientValidatable
    //{
    //    public ValidYearAttribute(): base("")
    //    {

    //    }

    //    public override bool IsValid(object value)
    //    {
    //        return Convert.ToInt32(value) <= DateTime.Now.Year;
    //    }

    //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    //    {
    //        var rule = new ModelClientValidationRule();
    //        rule.ErrorMessage = Resources.Home.Views.Resource.BirthYearInvalid;
    //        rule.ValidationType = "validbirthyear";
    //        yield return rule;
    //    }
    //}

    public class CustomHandleErrorAttribute:HandleErrorAttribute
    {
        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            Exception e = filterContext.Exception;

            // if the request is AJAX return JSON else view.
            if (IsAjax(filterContext))
            {
                //Because its a exception raised after ajax invocation
                //Lets return Json
                filterContext.Result = new JsonResult()
                {
                    Data = filterContext.Exception.Message,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                UrbanLighthouse.Shared.Utilities.Logging.WriteToLog("Ajax Error: " + e.Message, e);
            }
            else
            {
                if (e.GetType() == typeof(HttpException))
                {
                    int statusCode = ((HttpException)e).GetHttpCode();
                    UrbanLighthouse.Shared.Utilities.Logging.WriteToLog("Http Error " + statusCode + ": " + e.Message, e);
                }
                else
                {
                    // Not an HTTP related error so this is a problem in our code, set status to
                    // 500 (internal server error)
                    UrbanLighthouse.Shared.Utilities.Logging.WriteToLog("Server Error 500: " + e.Message, e);
                }
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();

            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}