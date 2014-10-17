using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LO30;

namespace LO30
{
  public class CSharpRazorViewEngine : RazorViewEngine
  {
    public CSharpRazorViewEngine()
    {
      AreaViewLocationFormats = new[]
      {
        "~/Areas/{2}/{1}.{0}.cshtml",
        "~/Areas/{2}/{0}.cshtml",
        "~/Areas/{2}/Shared/{1}.{0}.cshtml",
        "~/Areas/{2}/Shared/{0}.cshtml",
        "~/Areas/{1}.{0}.cshtml",
        "~/Areas/{0}.cshtml",
        "~/Areas/Shared/{0}.cshtml"
      };

      AreaMasterLocationFormats = new[]
      {
        "~/Areas/{2}/{1}.{0}.cshtml",
        "~/Areas/{2}/{0}.cshtml",
        "~/Areas/{2}/Shared/{1}.{0}.cshtml",
        "~/Areas/{2}/Shared/{0}.cshtml",
        "~/Views/{1}/{0}.cshtml",
        "~/Views/Shared/{0}.cshtml"
      };

      AreaPartialViewLocationFormats = new[]
      {
        "~/Areas/{2}/{1}.{0}.cshtml",
        "~/Areas/{2}/{0}.cshtml",
        "~/Areas/{2}/Shared/{1}.{0}.cshtml",
        "~/Areas/{2}/Shared/{0}.cshtml",
        "~/Views/{1}/{0}.cshtml",
        "~/Views/Shared/{0}.cshtml"
      };

      ViewLocationFormats = new[]
      {
        "~/Views/{1}/{0}.cshtml",
        "~/Views/Shared/{0}.cshtml"
      };

      MasterLocationFormats = new[]
      {
        "~/Views/{1}/{0}.cshtml",
        "~/Views/Shared/{0}.cshtml"
      };

      PartialViewLocationFormats = new[]
      {
        "~/Views/{1}/{0}.cshtml",
        "~/Views/Shared/{0}.cshtml"
      };
    }
  }

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("format", "json", "application/json"));

            // Allow looking up views in ~/Content/ directory
            /*var razorEngine = ViewEngines.Engines.OfType<RazorViewEngine>().First();
            razorEngine.ViewLocationFormats = razorEngine.ViewLocationFormats.Concat(new string[] 
            { 
                "~/Content/{1}/{0}.cshtml",
                "~/Content/Shared/{0}.cshtml"
            }).ToArray();

            razorEngine.PartialViewLocationFormats = razorEngine.PartialViewLocationFormats.Concat(new string[] 
            { 
                "~/Content/{1}/{0}.cshtml",
                "~/Content/Shared/{0}.cshtml"
            }).ToArray();

            razorEngine.PartialViewLocationFormats = razorEngine.AreaViewLocationFormats.Concat(new string[] 
            { 
                "~/Content/{1}/{0}.cshtml",
                "~/Content/Shared/{0}.cshtml"
            }).ToArray();*/

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CSharpRazorViewEngine());

        }
    }
}