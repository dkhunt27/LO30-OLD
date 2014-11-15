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

      var constApisUrl = "api/v2";

      #region gameRoster(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiGameRosters",
          routeTemplate: constApisUrl + "/gameRosters/{gameId}/{homeTeam}",
          defaults: new { controller = "GameRosters", gameId = RouteParameter.Optional, homeTeam = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameRoster",
          routeTemplate: constApisUrl + "/gameRoster/{gameRosterId}",
          defaults: new { controller = "GameRoster"}
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameRoster2",
          routeTemplate: constApisUrl + "/gameRoster/{gameTeamId}/{playerNumber}",
          defaults: new { controller = "GameRoster"}
      );
      #endregion

      #region game(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiGames",
          routeTemplate: constApisUrl + "/games/{gameId}",
          defaults: new { controller = "Games", gameId = RouteParameter.Optional }
      );
      #endregion

      #region gameTeam(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiGameTeams",
          routeTemplate: constApisUrl + "/gameTeams/{gameId}",
          defaults: new { controller = "GameTeams", gameId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameTeam",
          routeTemplate: constApisUrl + "/gameTeam/{gameTeamId}",
          defaults: new { controller = "GameTeam" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameTeam2",
          routeTemplate: constApisUrl + "/gameTeam/{gameId}/{homeTeam}",
          defaults: new { controller = "GameTeam" }
      );
      #endregion

      #region player(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiPlayers",
          routeTemplate: constApisUrl + "/players",
          defaults: new { controller = "Players" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiPlayer",
          routeTemplate: constApisUrl + "/player/{playerId}",
          defaults: new { controller = "Player" }
      );
      #endregion

      #region playerStat(s)Career routes
      config.Routes.MapHttpRoute(
          name: "ApiPlayerStatsCareer",
          routeTemplate: constApisUrl + "/playerStatsCareer/{playerId}",
          defaults: new { controller = "PlayerStatsCareer", playerId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiPlayerStatCareer",
          routeTemplate: constApisUrl + "/playerStatCareer/{playerId}/{sub}",
          defaults: new { controller = "PlayerStatCareer" }
      );
      #endregion

      #region playerStat(s)Season routes
      config.Routes.MapHttpRoute(
          name: "ApiPlayerStatsSeason",
          routeTemplate: constApisUrl + "/playerStatsSeason/{playerId}/{seasonId}",
          defaults: new { controller = "PlayerStatsSeason", playerId = RouteParameter.Optional, seasonId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiPlayerStatSeason",
          routeTemplate: constApisUrl + "/playerStatSeason/{playerId}/{seasonId}/{sub}",
          defaults: new { controller = "PlayerStatSeason" }
      );
      #endregion

      #region playerStat(s)SeasonTeam routes
      config.Routes.MapHttpRoute(
          name: "ApiPlayerStatsSeasonTeam",
          routeTemplate: constApisUrl + "/playerStatsSeasonTeam/{playerId}/{seasonId}",
          defaults: new { controller = "PlayerStatsSeasonTeam", playerId = RouteParameter.Optional, seasonId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiPlayerStatSeasonTeam",
          routeTemplate: constApisUrl + "/playerStatSeasonTeam/{playerId}/{seasonTeamId}",
          defaults: new { controller = "PlayerStatSeasonTeam" }
      );
      #endregion

      #region playerStat(s)Game routes
      config.Routes.MapHttpRoute(
          name: "ApiPlayerStatsGame",
          routeTemplate: constApisUrl + "/playerStatsGame/{playerId}/{seasonId}",
          defaults: new { controller = "PlayerStatsGame", playerId = RouteParameter.Optional, seasonId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiPlayerStatGame",
          routeTemplate: constApisUrl + "/playerStatGame/{playerId}/{gameId}",
          defaults: new { controller = "PlayerStatGame" }
      );
      #endregion

      #region playersSubSearch(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiPlayersSubSearch",
          routeTemplate: constApisUrl + "/playersSubSearch/{position}/{ratingMin}/{ratingMax}",
          defaults: new { controller = "PlayersSubSearch" }
      );
      #endregion

      #region teamRoster(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiTeamRosters",
          routeTemplate: constApisUrl + "/teamRosters/{seasonTeamId}/{yyyymmdd}",
          defaults: new { controller = "TeamRosters", seasonTeamId = RouteParameter.Optional, yyyymmdd = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiTeamRoster",
          routeTemplate: constApisUrl + "/teamRoster/{seasonTeamId}/{yyyymmdd}/{playerId}",
          defaults: new { controller = "TeamRosters" }
      );
      #endregion

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