using System.Web;
using System.Web.Optimization;

namespace Sw
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/util.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                      "~/Scripts/shBrushXml.js",
                      "~/Scripts/shCore.js",
                      "~/Scripts/custom.js",
                      "~/Scripts/pace/pace.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                       "~/Content/shCoreDefault.css",
                      "~/Content/docs.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/custom.css",
                      "~/fonts/css/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/template").Include(
                      "~/Content/shCoreDefault.css",
                      "~/Content/docs.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/custom.css",
                      "~/fonts/css/font-awesome.css"));
        }
    }
}
