using System.Web.Optimization;

namespace AccountExternalWeb.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/AngularJS")
                .Include("~/Scripts/Angular/app.module.js")
                .IncludeDirectory("~/Scripts/Angular/Controller", "*.js", true)
                //.IncludeDirectory("~/Scripts/Angular/Directive", "*.js", true)
                .IncludeDirectory("~/Scripts/Angular/Service", "*.js", true));

            BundleTable.EnableOptimizations = true;
        }
    }
}