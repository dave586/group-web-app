using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebApplication1.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/knockout").Include("~/Scripts/knockout-{version}.js", "~/Scripts/knockout.mapping-latest.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.js"));

            bundles.Add(new ScriptBundle("~/bundles/utilities").Include(
            "~/Scripts/Utilities.js"));

            bundles.Add(new ScriptBundle("~/bundles/signals").Include(
                "~/Scripts/jvfloat.js",
                "~/Scripts/signals.js",
                "~/Scripts/ul.js",
                "~/Scripts/onstart.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/ResponseComplete").Include(
                "~/Scripts/ResponseComplete.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/Kendo").Include(
                "~/Scripts/kendo/2015.2.624/kendo.all.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/ReportViewer").Include(
                "~/ReportViewer/js/telerikReportViewer-8.1.14.804.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/themes/base/jquery.ui.all.css",
                "~/Content/bootstrap.css",
                "~/Content/jvfloat.css",
                "~/Content/site.css",
                "~/Content/font-awesome.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/Kendo").Include(
                "~/Content/kendo/2015.2.624/kendo.common.min.css",
                "~/Content/kendo/2015.2.624/kendo.blueopal.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/ReportViewer").Include(
                "~/ReportViewer/styles/telerikReportViewer-8.1.14.804.css"
                ));
        }
    }
}
