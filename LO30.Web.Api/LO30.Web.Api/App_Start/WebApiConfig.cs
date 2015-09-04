using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Restier.WebApi;
using Microsoft.Restier.WebApi.Batch;
using LO30.Web.Api.Controllers;

namespace LO30.Web.Api
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
