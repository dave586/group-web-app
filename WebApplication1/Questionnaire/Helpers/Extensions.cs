using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Helpers
{
    public static class Extensions
    {
        public static string FullyQualifiedApplicationPath(this UrlHelper urlhelper)
        {
            //Return variable declaration
            string appPath = null;

            //Getting the current context of HTTP request
            var context = HttpContext.Current;

            //Checking the current context content
            if (context != null)
            {
                //Formatting the fully qualified website url/name
                appPath = String.Format("{0}://{1}{2}{3}", context.Request.Url.Scheme, context.Request.Url.Host, context.Request.Url.Port == 80 ? String.Empty : ":" + context.Request.Url.Port, context.Request.ApplicationPath);
            }

            if (!appPath.EndsWith("/"))
                appPath += "/";

            return appPath;
        }
    }
}