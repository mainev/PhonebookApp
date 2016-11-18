using System.Web;
using System.Web.Optimization;

namespace PhonebookApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //angular bundles
            bundles.Add(new ScriptBundle("~/bundles/bower_components/angularjs_scripts").Include(
                    "~/bower_components/angular/angular.min.js",
                    "~/bower_components/angular-animate/angular-animate.min.js",
                    "~/bower_components/angular-resource/angular-resource.min.js",
                    "~/bower_components/angular-route/angular-route.min.js",
                    "~/bower_components/angular-cookies/angular-cookies.min.js"));

         

            bundles.Add(new ScriptBundle("~/bundles/angularControllers")
              .IncludeDirectory("~/Scripts/controllers", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularServices")
             .IncludeDirectory("~/Scripts/Services", "*.js"));

         
        }
    }
}
