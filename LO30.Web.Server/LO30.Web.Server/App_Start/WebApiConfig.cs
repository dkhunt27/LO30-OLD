using LO30.Web.Server.Controllers;
using Microsoft.Restier.WebApi;
using Microsoft.Restier.WebApi.Batch;
using System.Web.Http;


namespace LO30.Web.Server.App_Start
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();
      RegisterNorthwind(config, GlobalConfiguration.DefaultServer);
      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
    }

    public static async void RegisterNorthwind(HttpConfiguration config, HttpServer server)
    {
      await config.MapODataDomainRoute<LO30Controller>(
         "LO30Api", "api/LO30",
          new ODataDomainBatchHandler(server));
    }
  }
}