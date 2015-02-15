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

      #region forWebGoalieStats routes
      config.Routes.MapHttpRoute(
          name: "ApiForWebGoalieStats",
          routeTemplate: constApisUrl + "/forWebGoalieStats/{seasonId}/{playoffs}",
          defaults: new { controller = "forWebGoalieStats" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiForWebGoalieStatsDataGoodThru",
          routeTemplate: constApisUrl + "/forWebGoalieStatsDataGoodThru/{seasonId}",
          defaults: new { controller = "forWebGoalieStatsDataGoodThru" }
      );
      #endregion

      #region forWebPlayerStats routes
      config.Routes.MapHttpRoute(
          name: "ApiForWebPlayerStats",
          routeTemplate: constApisUrl + "/forWebPlayerStats/{seasonId}/{playoffs}",
          defaults: new { controller = "forWebPlayerStats" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiForWebPlayerStatsDataGoodThru",
          routeTemplate: constApisUrl + "/forWebPlayerStatsDataGoodThru/{seasonId}",
          defaults: new { controller = "forWebPlayerStatsDataGoodThru" }
      );
      #endregion

      #region forWebTeamStandings routes
      config.Routes.MapHttpRoute(
          name: "ApiForWebTeamStandings",
          routeTemplate: constApisUrl + "/forWebTeamStandings/{seasonId}/{playoffs}",
          defaults: new { controller = "forWebTeamStandings" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiForWebTeamStandingsDataGoodThru",
          routeTemplate: constApisUrl + "/forWebTeamStandingsDataGoodThru/{seasonId}",
          defaults: new { controller = "forWebTeamStandingsDataGoodThru" }
      );
      #endregion

      #region gameRoster(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiGameRosters",
          routeTemplate: constApisUrl + "/gameRosters/{gameId}/{homeTeam}",
          defaults: new { controller = "GameRosters", gameId = RouteParameter.Optional, homeTeam = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameRoster",
          routeTemplate: constApisUrl + "/gameRoster/{gameRosterId}",
          defaults: new { controller = "GameRoster" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameRoster2",
          routeTemplate: constApisUrl + "/gameRoster/{gameTeamId}/{playerNumber}",
          defaults: new { controller = "GameRoster" }
      );
      #endregion

      #region gameScore(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiGameScores1",
          routeTemplate: constApisUrl + "/gameScores/{fullDetail}",
          defaults: new { controller = "GameScores" }
      );
      config.Routes.MapHttpRoute(
          name: "ApiGameScores2",
          routeTemplate: constApisUrl + "/gameScores/{gameId}/{fullDetail}",
          defaults: new { controller = "GameScores" }
      );
      config.Routes.MapHttpRoute(
          name: "ApiGameScores3",
          routeTemplate: constApisUrl + "/gameScores/{gameId}/{homeTeam}/{fullDetail}",
          defaults: new { controller = "GameScores"}
      );
      #endregion

      #region gameOutcome(s) routes
      config.Routes.MapHttpRoute(
          name: "ApiGameOutcomes",
          routeTemplate: constApisUrl + "/gameOutcomes/{fullDetail}",
          defaults: new { controller = "GameOutcomes" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameOutcomes2",
          routeTemplate: constApisUrl + "/gameOutcomes/{gameId}/{fullDetail}",
          defaults: new { controller = "GameOutcomes" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameOutcomes3",
          routeTemplate: constApisUrl + "/gameOutcomesBySeasonTeam/{seasonId}/{playoffs}/{seasonTeamId}/{fullDetail}",
          defaults: new { controller = "GameOutcomesBySeasonTeam" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGameOutcome",
          routeTemplate: constApisUrl + "/gameOutcome/{gameId}/{homeTeam}/{fullDetail}",
          defaults: new { controller = "GameOutcome" }
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

      #region goalieStat(s)Career routes
      config.Routes.MapHttpRoute(
          name: "ApiGoalieStatsCareer",
          routeTemplate: constApisUrl + "/goalieStatsCareer/{playerId}",
          defaults: new { controller = "GoalieStatsCareer", playerId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGoalieStatCareer",
          routeTemplate: constApisUrl + "/goalieStatCareer/{playerId}/{sub}",
          defaults: new { controller = "GoalieStatCareer" }
      );
      #endregion

      #region goalieStat(s)Season routes
      config.Routes.MapHttpRoute(
          name: "ApiGoalieStatsSeason",
          routeTemplate: constApisUrl + "/goalieStatsSeason/{playerId}/{seasonId}",
          defaults: new { controller = "GoalieStatsSeason", playerId = RouteParameter.Optional, seasonId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGoalieStatSeason",
          routeTemplate: constApisUrl + "/goalieStatSeason/{playerId}/{seasonId}/{sub}",
          defaults: new { controller = "GoalieStatSeason" }
      );
      #endregion

      #region goalieStat(s)SeasonTeam routes
      config.Routes.MapHttpRoute(
          name: "ApiGoalieStatsSeasonTeam",
          routeTemplate: constApisUrl + "/goalieStatsSeasonTeam/{playerId}/{seasonId}",
          defaults: new { controller = "GoalieStatsSeasonTeam", playerId = RouteParameter.Optional, seasonId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGoalieStatSeasonTeam",
          routeTemplate: constApisUrl + "/goalieStatSeasonTeam/{playerId}/{seasonTeamId}",
          defaults: new { controller = "GoalieStatSeasonTeam" }
      );
      #endregion

      #region goalieStat(s)Game routes
      config.Routes.MapHttpRoute(
          name: "ApiGoalieStatsGame",
          routeTemplate: constApisUrl + "/goalieStatsGame/{playerId}/{seasonId}",
          defaults: new { controller = "GoalieStatsGame", playerId = RouteParameter.Optional, seasonId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "ApiGoalieStatGame",
          routeTemplate: constApisUrl + "/goalieStatGame/{playerId}/{gameId}",
          defaults: new { controller = "GoalieStatGame" }
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

      #region scoreSheetEntry(s)Processed routes
      config.Routes.MapHttpRoute(
          name: "ApiScoreSheetEntriesProcessed",
          routeTemplate: constApisUrl + "/scoreSheetEntriesProcessed/{fullDetail}",
          defaults: new { controller = "ScoreSheetEntriesProcessed" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiScoreSheetEntriesProcessed2",
          routeTemplate: constApisUrl + "/scoreSheetEntriesProcessed/{gameId}/{fullDetail}",
          defaults: new { controller = "ScoreSheetEntriesProcessed" }
      );
      #endregion

      #region scoreSheetEntryPenalty(s)Processed routes
      config.Routes.MapHttpRoute(
          name: "ApiScoreSheetEntryPenaltiesProcessed",
          routeTemplate: constApisUrl + "/scoreSheetEntryPenaltiesProcessed/{fullDetail}",
          defaults: new { controller = "ScoreSheetEntryPenaltiesProcessed" }
      );

      config.Routes.MapHttpRoute(
          name: "ApiScoreSheetEntryPenaltiesProcessed2",
          routeTemplate: constApisUrl + "/scoreSheetEntryPenaltiesProcessed/{gameId}/{fullDetail}",
          defaults: new { controller = "ScoreSheetEntryPenaltiesProcessed" }
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