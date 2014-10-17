using System.Web;
using System.Web.Optimization;

namespace LO30
{
  public class BundleConfig
  {
    // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    public static void RegisterBundles(BundleCollection bundles)
    {
      bundles.Add(new ScriptBundle("~/bundles/adminDataProcessing").Include(
                    "~/Areas/AdminDataProcessing/AdminDataProcessingAngular.js"));

      /*bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                  "~/Scripts/jquery-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                  "~/Scripts/jquery-ui-{version}.js"));

      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                  "~/Scripts/jquery.unobtrusive*",
                  "~/Scripts/jquery.validate*"));

      // Use the development version of Modernizr to develop with and learn from. Then, when you're
      // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
      bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                  "~/Scripts/modernizr-*"));

      bundles.Add(new StyleBundle("~/Site/css").Include("~/Site/site.css"));

      bundles.Add(new StyleBundle("~/Site/themes/base/css").Include(
                  "~/Site/themes/base/jquery.ui.core.css",
                  "~/Site/themes/base/jquery.ui.resizable.css",
                  "~/Site/themes/base/jquery.ui.selectable.css",
                  "~/Site/themes/base/jquery.ui.accordion.css",
                  "~/Site/themes/base/jquery.ui.autocomplete.css",
                  "~/Site/themes/base/jquery.ui.button.css",
                  "~/Site/themes/base/jquery.ui.dialog.css",
                  "~/Site/themes/base/jquery.ui.slider.css",
                  "~/Site/themes/base/jquery.ui.tabs.css",
                  "~/Site/themes/base/jquery.ui.datepicker.css",
                  "~/Site/themes/base/jquery.ui.progressbar.css",
                  "~/Site/themes/base/jquery.ui.theme.css"));*/
    }
  }
}