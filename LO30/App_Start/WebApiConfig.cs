using LO30.Formatters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace LO30
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {

      var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
      jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

      var constApisUrl = "api/v1";

      config.Routes.MapHttpRoute(
          name: "ApiGameRosters",
          routeTemplate: constApisUrl + "/gamerosters/{gameId}/{homeTeam}",
          defaults: new { controller = "GameRosters", gameId = RouteParameter.Optional, homeTeam = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameRosters1",
          routeTemplate: constApisUrl + "/gameroster/{gameRosterId}",
          defaults: new { controller = "GameRoster"}
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameRosters2",
          routeTemplate: constApisUrl + "/gameroster/{gameTeamId}/{playerNumber}",
          defaults: new { controller = "GameRoster"}
      );

      config.Routes.MapHttpRoute(
          name: "ApiGames",
          routeTemplate: constApisUrl + "/games/{gameId}/",
          defaults: new { controller = "Games", gameId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: constApisUrl + "/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );

      config.Formatters.Add(new BrowserJsonFormatter());
      config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

      // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
      // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
      // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
      //config.EnableQuerySupport();
    }
  }
}