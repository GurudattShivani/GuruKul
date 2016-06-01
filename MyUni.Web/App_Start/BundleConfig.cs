﻿using System.Web;
using System.Web.Optimization;

namespace Gurukul.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

             #region CSS Bundles

            //
            // AdminLTE theme
            //
            bundles.Add(new StyleBundle("~/bundles/adminltecss").Include(
                "~/Content/bootstrap.css",
                "~/Content/font-awesome.css",
                "~/Content/ionicons.css",
                "~/Content/plugins/dataTables.bootstrap.css",
                "~/Content/AdminLTE.css",
                "~/Content/skins/skin-blue-light.css",
                "~/Content/Site.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/datepickercss").Include(
                "~/Content/bootstrap-datepicker.css",
                "~/Content/bootstrap-datepicker3.css"
                ));

            #endregion
            

            #region Script Bundles

            bundles.Add(new ScriptBundle("~/bundles/adminltejs").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/bootstrap.js",

                      "~/Scripts/plugins/jquery.dataTables.js",
                      "~/Scripts/plugins/dataTables.bootstrap.js",

                      "~/Scripts/adminlte.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/datepickerjs").Include(
                "~/Scripts/bootstrap-datepicker.min.js"
                ));

            #endregion
        }
    }
}
