using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Questionnaire.Models;


namespace Questionnaire.Filters
{
    public class SessionTimeStampFilter : ActionFilterAttribute, IActionFilter
    {
        private const int _timeStamp_MaxDuration = 60*5;//5 minutes

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.OnActionExecuted(filterContext);
        }

        private bool ValidTimeStamp(DateTime testTimeStamp)
        {
            if (testTimeStamp == DateTime.MinValue)
                return true;

            return (DateTime.Now - testTimeStamp).Duration().TotalSeconds <= _timeStamp_MaxDuration;
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isValid = true;
            foreach(var ap in filterContext.ActionParameters)
            {
                if(ap.Value.GetType() == typeof(SelectPrePostSessionModel))
                {
                    var actual = (SelectPrePostSessionModel)ap.Value;
                    isValid = ValidTimeStamp(actual.SessionTimeStamp);
                    break;
                }
                if (ap.Value.GetType() == typeof(ValidateClient))
                {
                    var actual = (ValidateClient)ap.Value;
                    isValid = ValidTimeStamp(actual.SessionTimeStamp);
                    break;
                }
                if (ap.Value.GetType() == typeof(ValidateChallenge))
                {
                    var actual = (ValidateChallenge)ap.Value;
                    isValid = ValidTimeStamp(actual.SessionTimeStamp);
                    break;
                }
            }

            if(!isValid)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Home", action = "Index" })
                );
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }
            
            this.OnActionExecuting(filterContext);
        }
    }
}