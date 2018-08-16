using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            string culture = requestContext.HttpContext.Session != null && requestContext.HttpContext.Session["culture"] != null ? requestContext.HttpContext.Session["culture"].ToString() : "en-CA";
            if (!String.IsNullOrEmpty(culture))
            {
                SetupCulture(culture, requestContext);
            }
            base.Initialize(requestContext);
        }

        public void SetupCulture(string culture, RequestContext requestContext)
        {
            culture = culture ?? "en-CA";
            ViewBag.Culture = culture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(culture);

            DateTimeFormatInfo englishDateTimeFormat = new CultureInfo("en-CA").DateTimeFormat; //keep datetime format same independent of culture
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = englishDateTimeFormat;

            if (requestContext != null && requestContext.HttpContext != null)
                requestContext.HttpContext.Session["culture"] = culture;
        }

        public string GetCulture(RequestContext requestContext)
        {
            return (requestContext != null && requestContext.HttpContext != null && requestContext.HttpContext.Session != null) ? requestContext.HttpContext.Session["culture"].ToString() : "en-CA";
        }
    }
}