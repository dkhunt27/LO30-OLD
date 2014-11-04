using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30Repository : ILo30Repository
  {
    Lo30Context _ctx;
    Lo30DataService _lo30DataService;
    Lo30ContextService _contextService;

    Player _unknownPlayer;

    public Lo30Repository(Lo30Context ctx, Lo30ContextService contextService)
    {
      _ctx = ctx;
      _unknownPlayer = new Player()
        {
          PlayerId = 0,
          FirstName = "Unknown",
          LastName = "Player",
          Suffix = null,
          PreferredPosition = "X",
          Shoots = "X",
          BirthDate = DateTime.MinValue,
          Profession = null,
          WifesName = null
        };

      _lo30DataService = new Lo30DataService();
      _contextService = contextService;

      // force the context to populate the data...just for improved error messaging

      try
      {
        var seasons = _ctx.Seasons.ToList();
      } 
      catch (Exception ex)
      {
        ErrorHandlingService.PrintFullErrorMessage(ex);
        throw ex;
      }
    }

    #region Data Services

    #region Games
    public List<Game> GetGames()
    {
      return _ctx.Games
                    .Include("Season")
                    .ToList();
    }

    public Game GetGameByGameId(int gameId)
    {
      return _contextService.FindGame(gameId);
    }
    #endregion

    #region GameTeams
    public List<GameTeam> GetGameTeams()
    {
      return _ctx.GameTeams
                    .Include("Game")
                    .Include("Game.Season")

                    .Include("SeasonTeam")
                    .Include("SeasonTeam.Season")
                    .Include("SeasonTeam.Team")
                    .Include("SeasonTeam.Team.Coach")
                    .Include("SeasonTeam.Team.Sponsor")
                    .ToList();
    }

    public GameTeam GetGameTeamByGameTeamId(int gameTeamId)
    {
      return _contextService.FindGameTeam(gameTeamId);
    }

    public GameTeam GetGameTeamByGameIdAndHomeTeam(int gameTeamId, bool homeTeam)
    {
      return _contextService.FindGameTeamByPK2(gameTeamId, homeTeam);
    }
    #endregion

    #region GameRosters
    public List<GameRoster> GetGameRosters()
    {
      return _ctx.GameRosters
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")

                    .Include("Player")
                    .Include("SubbingForPlayer")
                    .ToList();
    }

    public List<GameRoster> GetGameRostersByGameId(int gameId)
    {
      return GetGameRosters().Where(x => x.GameTeam.GameId == gameId).ToList();
    }

    public List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return GetGameRosters().Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam).ToList();
    }

    public GameRoster GetGameRosterByGameRosterId(int gameRosterId)
    {
      return _contextService.FindGameRoster(gameRosterId);
    }

    public GameRoster GetGameRosterByGameTeamIdAndPlayerNumber(int gameTeamId, string playerNumber)
    {
      return _contextService.FindGameRosterByPK2(gameTeamId, playerNumber);
    }
    #endregion
    #endregion

    public List<ScoreSheetEntry> GetScoreSheetEntries()
    {
      return _ctx.ScoreSheetEntries.ToList();
    }

    public List<Article> GetArticles()
    {
      return _ctx.Articles.ToList();
    }

    public List<TeamStanding> GetTeamStandings()
    {
      return _ctx.TeamStandings.Include("seasonTeam").Include("seasonTeam.team").ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeason()
    {
      return _ctx.PlayerStatsSeason.Include("season").Include("player").Include("seasonTeamPlayingFor").Include("seasonTeamPlayingFor.team").ToList();
    }

    public List<ForWebPlayerStat> GetPlayerStatsForWeb()
    {
      return _ctx.ForWebPlayerStats.ToList();
    }

    public List<ForWebGoalieStat> GetGoalieStatsForWeb()
    {
      return _ctx.ForWebGoalieStats.ToList();
    }

    public List<ForWebTeamStanding> GetTeamStandingsForWeb()
    {
      return _ctx.ForWebTeamStandings.ToList();
    }

    public bool Save()
    {
      try
      {
        return _ctx.SaveChanges() > 0;
      }
      catch (Exception ex)
      {
        // TODO log this error
        return false;
      }
    }

    public void SaveTablesToJson()
    {
      _contextService.SaveTablesToJson();
    }

    public bool AddArticle(Article newArticle)
    {
      try
      {
        _ctx.Articles.Add(newArticle);
        return true;
      }
      catch (Exception ex)
      {
        // TODO log this error
        return false;
      }
    }

    public ProcessingResult ProcessScoreSheetEntryPenalties(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        var scoreSheetEntryPenalties = _ctx.ScoreSheetEntryPenalties.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameTeams = _ctx.GameTeams.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameRosters = _ctx.GameRosters
                                .Include("GameTeam")
                                .Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId)
                                .ToList();
        var penalties = _ctx.Penalties.ToList();

        result.toProcess = scoreSheetEntryPenalties.Count;

        var scoreSheetEntryPenaltiesProcessed = _lo30DataService.DeriveScoreSheetEntryPenaltiesProcessed(scoreSheetEntryPenalties, gameTeams, gameRosters, penalties);

        result.modified = _contextService.SaveOrUpdateScoreSheetEntryPenaltyProcessed(scoreSheetEntryPenaltiesProcessed);

        // AUDIT ScoreSheetEntries and ScoreSheetEntriesProcessed should have same ids 
        var inputs = _ctx.ScoreSheetEntryPenalties.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var outputs = _ctx.ScoreSheetEntryPenaltiesProcessed.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();

        if (inputs.Count != outputs.Count)
        {
          result.error = "Error processing ScoreSheetEntryPenalties. The ScoreSheetEntryPenalties count (" + inputs.Count + ") does not match ScoreSheetEntryPenaltiesProcessed count (" + outputs.Count + ")";
        }
        else
        {
          foreach (var input in inputs)
          {
            var output = outputs.Where(x => x.ScoreSheetEntryPenaltyId == input.ScoreSheetEntryPenaltyId).FirstOrDefault();

            if (output == null)
            {
              result.error = "Error processing ScoreSheetEntryPenalties. The ScoreSheetEntryPenaltyId (" + input.ScoreSheetEntryPenaltyId + ") is missing from ScoreSheetEntryPenaltiesProcessed";
            }
          }
        }
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    public ProcessingResult ProcessScoreSheetEntries(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        var scoreSheetEntries = _ctx.ScoreSheetEntries.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameTeams = _ctx.GameTeams.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameRosters = _ctx.GameRosters
                                .Include("GameTeam")
                                .Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId)
                                .ToList();

        result.toProcess = scoreSheetEntries.Count;

        var scoreSheetEntriesProcessed = _lo30DataService.DeriveScoreSheetEntriesProcessed(scoreSheetEntries, gameTeams, gameRosters);

        result.modified = _contextService.SaveOrUpdateScoreSheetEntryProcessed(scoreSheetEntriesProcessed);

        // AUDIT ScoreSheetEntries and ScoreSheetEntriesProcessed should have same ids 
        var inputs = _ctx.ScoreSheetEntries.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var outputs = _ctx.ScoreSheetEntriesProcessed.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();

        if (inputs.Count != outputs.Count)
        {
          result.error = "Error processing ScoreSheetEntries. The ScoreSheetEntries count (" + inputs.Count + ") does not match ScoreSheetEntriesProcessed count (" + outputs.Count + ")";
        }
        else
        {
          foreach (var input in inputs)
          {
            var output = outputs.Where(x => x.ScoreSheetEntryId == input.ScoreSheetEntryId).FirstOrDefault();

            if (output == null)
            {
              result.error = "Error processing ScoreSheetEntries. The ScoreSheetEntryId (" + input.ScoreSheetEntryId + ") is missing from ScoreSheetEntriesProcessed";
            }
          }
        }
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    public ProcessingResult ProcessScoreSheetEntriesIntoGameResults(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        // get list of game entries for these games (use game just in case there was no score sheet entries...0-0 game with no penalty minutes)
        var games = _ctx.Games.Where(s => s.GameId >= startingGameId && s.GameId <= endingGameId).ToList();

        result.toProcess = games.Count;

        // get a list of periods
        var periods = new int[] { 1, 2, 3, 4 };

        var modifiedCount = 0;

        // loop through each game
        for (var g = 0; g < games.Count; g++)
        {
          var gameId = games[g].GameId;
          int scoreHomeTeamTotal = 0;
          int scoreAwayTeamTotal = 0;
          int penaltyHomeTeamTotal = 0;
          int penaltyAwayTeamTotal = 0;

          // look up the home and away season team id
          var gameTeams = _ctx.GameTeams.Where(gt => gt.GameId == gameId).ToList();
          var homeSeasonTeamId = gameTeams.Where(gt => gt.HomeTeam == true).FirstOrDefault().SeasonTeamId;
          var awaySeasonTeamId = gameTeams.Where(gt => gt.HomeTeam == false).FirstOrDefault().SeasonTeamId;

          var homeGameTeam = _ctx.GameTeams.Where(x => x.GameId == gameId && x.SeasonTeamId == homeSeasonTeamId).FirstOrDefault();
          if (homeGameTeam == null)
          {
            throw new ArgumentNullException("homeGameTeam", "GameTeam was not found for GameId:" + gameId + " SeasonTeamId:" + homeSeasonTeamId);
          }
          var awayGameTeam = _ctx.GameTeams.Where(x => x.GameId == gameId && x.SeasonTeamId == awaySeasonTeamId).FirstOrDefault();
          if (awayGameTeam == null)
          {
            throw new ArgumentNullException("awayGameTeam", "GameTeam was not found for GameId:" + gameId + " SeasonTeamId:" + awaySeasonTeamId);
          }

          #region loop through each period
          for (var p = 0; p < periods.Length; p++)
          {
            var period = periods[p];
            var scoreHomeTeamPeriod = 0;
            var scoreAwayTeamPeriod = 0;

            #region process all score sheet entries for this specific game/period
            var scoreSheetEntries = _ctx.ScoreSheetEntries.Where(s => s.GameId == gameId && s.Period == period).ToList();

            for (var s = 0; s < scoreSheetEntries.Count; s++)
            {
              var scoreSheetEntry = scoreSheetEntries[s];

              if (scoreSheetEntry.HomeTeam)
              {
                scoreHomeTeamPeriod++;
                scoreHomeTeamTotal++;
              }
              else
              {
                scoreAwayTeamPeriod++;
                scoreAwayTeamTotal++;
              }
            }
            #endregion

            #region create and save (or update) the home and away teams GameScore by period
            var homeGameScore = new GameScore(
                                  gtid: homeGameTeam.GameTeamId,
                                  per: period,
                                  score: scoreAwayTeamPeriod
                                );

            _contextService.SaveOrUpdateGameScore(homeGameScore);

            var awayGameScore = new GameScore(
                                    gtid: awayGameTeam.GameTeamId,
                                    per: period,
                                    score: scoreAwayTeamPeriod
                                  );

            _contextService.SaveOrUpdateGameScore(awayGameScore);
            #endregion

            #region process all score sheet entry penalties for this specific game/period
            var scoreSheetEntryPenalties = _ctx.ScoreSheetEntryPenalties.Where(s => s.GameId == gameId && s.Period == period).ToList();

            for (var s = 0; s < scoreSheetEntryPenalties.Count; s++)
            {
              var scoreSheetEntryPenalty = scoreSheetEntryPenalties[s];

              if (scoreSheetEntryPenalty.HomeTeam)
              {
                penaltyHomeTeamTotal = penaltyHomeTeamTotal + scoreSheetEntryPenalty.PenaltyMinutes;
              }
              else
              {
                penaltyAwayTeamTotal = penaltyAwayTeamTotal + scoreSheetEntryPenalty.PenaltyMinutes;
              }
            }
            #endregion
          }
          #endregion

          #region create and save (or update) the home and away teams GameScore for game
          var finalPeriod = 0;
          var homeFinalGameScore = new GameScore(
                                          gtid: homeGameTeam.GameTeamId,
                                          per: finalPeriod,
                                          score: scoreHomeTeamTotal
                                        );

          _contextService.SaveOrUpdateGameScore(homeFinalGameScore);

          var awayFinalGameScore = new GameScore(
                                          gtid: awayGameTeam.GameTeamId,
                                          per: finalPeriod,
                                          score: scoreAwayTeamTotal
                                        );

          _contextService.SaveOrUpdateGameScore(awayFinalGameScore);
          #endregion

          #region create and save (or update) the home and away teams GameOutcome for game
          // save game results for the game
          string homeResult = "T";
          string awayResult = "T";
          if (scoreHomeTeamTotal > scoreAwayTeamTotal)
          {
            homeResult = "W";
            awayResult = "L";
          }
          else if (scoreHomeTeamTotal < scoreAwayTeamTotal)
          {
            homeResult = "L";
            awayResult = "W";
          }

          var homeGameOutcome = new GameOutcome(
                                        gtid: homeGameTeam.GameTeamId,
                                        res: homeResult,
                                        gf: scoreHomeTeamTotal,
                                        ga: scoreAwayTeamTotal,
                                        pim: penaltyHomeTeamTotal,
                                        over: false
                                        );

          modifiedCount += _contextService.SaveOrUpdateGameOutcome(homeGameOutcome);

          var awayGameOutcome = new GameOutcome(
                                        gtid: awayGameTeam.GameTeamId,
                                        res: awayResult,
                                        gf: scoreAwayTeamTotal,
                                        ga: scoreHomeTeamTotal,
                                        pim: penaltyAwayTeamTotal,
                                        over: false
                                        );

          modifiedCount += _contextService.SaveOrUpdateGameOutcome(awayGameOutcome);
          #endregion
        }

        Debug.Print("ProcessScoreSheetEntriesIntoGameResults: savedGameOutcomes:" + modifiedCount);

        result.modified = modifiedCount;
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    // TODO, remove playoff input and determine it from the gameIds
    public ProcessingResult ProcessGameResultsIntoTeamStandings(int seasonId, bool playoffs, int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        // get every team just in case they do not have a game result yet
        var seasonTeams = _ctx.SeasonTeams.Where(st => st.SeasonId == seasonId).ToList();

        result.toProcess = seasonTeams.Count;

        // loop through each team and calculate their standings data
        for (var t = 0; t < seasonTeams.Count; t++)
        {
          var seasonTeam = seasonTeams[t];

          //todo find better way to identify these...field in team table?
          // first 16 teams are the place holders for position night
          if (seasonTeam.TeamId > 16)
          {
            int games = 0;
            int wins = 0;
            int losses = 0;
            int ties = 0;
            int points = 0;
            int goalsFor = 0;
            int goalsAgainst = 0;
            int penaltyMinutes = 0;

            // get game outcomes for this season team
            var gameOutcomes = _contextService.FindGameOutcomesWithGameIdsAndTeamId(startingGameId, endingGameId, seasonTeam.SeasonTeamId);

            // loop through each game
            //int seasonTypeId=-1; // TODO, make sure the games match seasonTYpe
            for (var g = 0; g < gameOutcomes.Count; g++)
            {
              var gameOutcome = gameOutcomes[g];

              if (gameOutcome.Outcome.ToLower() == "w")
              {
                wins++;
              }
              else if (gameOutcome.Outcome.ToLower() == "l")
              {
                losses++;
              }
              else
              {
                ties++;
              }

              games++;
              points = (wins * 2) + (losses * 0) + (ties * 1);

              goalsFor = goalsFor + gameOutcome.GoalsFor;
              goalsAgainst = goalsAgainst + gameOutcome.GoalsAgainst;
              penaltyMinutes = penaltyMinutes + gameOutcome.PenaltyMinutes;
            }

            var rank = -1;
            var teamStanding = new TeamStanding()
            {
              SeasonTeamId = seasonTeam.SeasonTeamId,
              Playoff = playoffs,
              Rank = rank,
              Games = games,
              Wins = wins,
              Losses = losses,
              Ties = ties,
              Points = points,
              GoalsFor = goalsFor,
              GoalsAgainst = goalsAgainst,
              PenaltyMinutes = penaltyMinutes
            };

            _contextService.SaveOrUpdateTeamStanding(teamStanding);
          }
        }

        // now process rank
        var standings = _ctx.TeamStandings.Where(ts => ts.SeasonTeam.SeasonId == seasonId)
                            .OrderByDescending(ts => ts.Points)
                            .ThenByDescending(ts => ts.Wins)
                            .ThenByDescending(ts => ts.GoalsFor - ts.GoalsAgainst)
                            .ThenByDescending(ts => ts.GoalsFor)
                            .ThenBy(ts => ts.PenaltyMinutes)
                            .ToList();

        for (var x = 0; x < standings.Count; x++)
        {
          var s = standings[x];
          var rank = x + 1;
          var teamStanding = new TeamStanding()
          {
            SeasonTeamId = s.SeasonTeamId,
            Playoff = s.Playoff,
            Rank = rank,
            Games = s.Games,
            Wins = s.Wins,
            Losses = s.Losses,
            Ties = s.Ties,
            Points = s.Points,
            GoalsFor = s.GoalsFor,
            GoalsAgainst = s.GoalsAgainst,
            PenaltyMinutes = s.PenaltyMinutes
          };

          _contextService.SaveOrUpdateTeamStanding(teamStanding);
        }

        result.modified = _ctx.SaveChanges();
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    public ProcessingResult ProcessScoreSheetEntriesIntoPlayerStats(int startingGameId, int endingGameId)
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        var gameRosters = _contextService.FindGameRostersWithGameIds(startingGameId, endingGameId);
        var gameRostersGoalies = _contextService.FindGameRostersWithGameIdsAndGoalie(startingGameId, endingGameId, goalie: true);
        var gameOutcomes = _contextService.FindGameOutcomesWithGameIds(startingGameId, endingGameId);
        var ratings = _ctx.PlayerRatings.ToList();

        var scoreSheetEntriesProcessed = _ctx.ScoreSheetEntriesProcessed.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var scoreSheetEntryPenaltiesProcessed = _ctx.ScoreSheetEntryPenaltiesProcessed.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();

        result.toProcess = scoreSheetEntriesProcessed.Count + scoreSheetEntryPenaltiesProcessed.Count;

        var playerGameStats = _lo30DataService.DerivePlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);
        var playerSeasonTeamStats = _lo30DataService.DerivePlayerSeasonTeamStats(playerGameStats);
        var playerSeasonStats = _lo30DataService.DerivePlayerSeasonStats(playerSeasonTeamStats);

        var goalieGameStats = _lo30DataService.DeriveGoalieGameStats(gameOutcomes, gameRostersGoalies);
        var goalieSeasonTeamStats = _lo30DataService.DeriveGoalieSeasonTeamStats(goalieGameStats);
        var goalieSeasonStats = _lo30DataService.DeriveGoalieSeasonStats(goalieSeasonTeamStats);

        var savedStatsGame = _contextService.SaveOrUpdatePlayerStatGame(playerGameStats);
        Debug.Print("ProcessScoreSheetEntriesIntoPlayerStats: savedStatsGame:" + savedStatsGame);

        var savedStatsSeasonTeam = _contextService.SaveOrUpdatePlayerStatSeasonTeam(playerSeasonTeamStats);
        Debug.Print("ProcessScoreSheetEntriesIntoPlayerStats: savedStatsSeasonTeam:" + savedStatsSeasonTeam);

        var savedStatsSeason = _contextService.SaveOrUpdatePlayerStatSeason(playerSeasonStats);
        Debug.Print("ProcessScoreSheetEntriesIntoPlayerStats: savedStatsSeason:" + savedStatsSeason);

        var savedStatsGameGoalie = _contextService.SaveOrUpdateGoalieStatGame(goalieGameStats);
        Debug.Print("ProcessScoreSheetEntriesIntoPlayerStats: savedStatsGameGoalie:" + savedStatsGameGoalie);

        var savedStatsSeasonTeamGoalie = _contextService.SaveOrUpdateGoalieStatSeasonTeam(goalieSeasonTeamStats);
        Debug.Print("ProcessScoreSheetEntriesIntoPlayerStats: savedStatsSeasonTeamGoalie:" + savedStatsSeasonTeamGoalie);

        var savedStatsSeasonGoalie = _contextService.SaveOrUpdateGoalieStatSeason(goalieSeasonStats);
        Debug.Print("ProcessScoreSheetEntriesIntoPlayerStats: savedStatsSeasonGoalie:" + savedStatsSeasonGoalie);

        result.modified = (savedStatsGame + savedStatsSeasonTeam + savedStatsSeason);
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }

    public ProcessingResult ProcessPlayerStatsIntoWebStats()
    {
      var result = new ProcessingResult();

      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      try
      {
        var ratings = _ctx.PlayerRatings.ToList();
        var seasonPlayerStatsForWeb = _ctx.PlayerStatsSeasonTeam
                                           .Include("season")
                                           .Include("player")
                                           .Include("seasonTeamPlayingFor")
                                           .Include("seasonTeamPlayingFor.team")
                                           .ToList();

        var seasonGoalieStatsForWeb = _ctx.GoalieStatsSeasonTeam
                                            .Include("season")
                                            .Include("player")
                                            .Include("seasonTeamPlayingFor")
                                            .Include("seasonTeamPlayingFor.team")
                                            .ToList();

        var teamStandingsForWeb = _ctx.TeamStandings
                                            .Include("seasonTeam")
                                            .ToList();

        result.toProcess = seasonPlayerStatsForWeb.Count + seasonGoalieStatsForWeb.Count;

        var playerWebStats = _lo30DataService.DeriveWebPlayerStats(seasonPlayerStatsForWeb, ratings);
        var savedWebPlayerStats = _contextService.SaveOrUpdateForWebPlayerStat(playerWebStats);
        Debug.Print("ProcessPlayerStatsIntoWebStats: savedWebPlayerStats:" + savedWebPlayerStats);

        var goalieWebStats = _lo30DataService.DeriveWebGoalieStats(seasonGoalieStatsForWeb);
        var savedGoaliePlayerStats = _contextService.SaveOrUpdateForWebGoalieStat(goalieWebStats);
        Debug.Print("DeriveWebGoalieStats: savedGoaliePlayerStats:" + savedGoaliePlayerStats);

        var teamStandings = _lo30DataService.DeriveWebTeamStandings(teamStandingsForWeb);
        var savedTeamStandings = _contextService.SaveOrUpdateForWebTeamStanding(teamStandings);
        Debug.Print("DeriveWebTeamStandings: savedTeamStandings:" + savedTeamStandings);

        result.modified = (savedWebPlayerStats + savedGoaliePlayerStats);
      }
      catch (Exception ex)
      {
        result.modified = -2;
        result.error = ex.Message;

        ErrorHandlingService.PrintFullErrorMessage(ex);
      }

      diffFromLast = DateTime.Now - last;
      result.time = diffFromLast.ToString();
      return result;
    }



  }
}