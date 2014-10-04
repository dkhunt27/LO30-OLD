using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Lo30Repository : ILo30Repository
  {
    Lo30Context _ctx;
    PlayerStatsService _playerStatsService;
    LO30ContextService _lo30ContextService;

    Player _unknownPlayer;

    public Lo30Repository(Lo30Context ctx)
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

      _playerStatsService = new PlayerStatsService();
      _lo30ContextService = new LO30ContextService(_ctx);

      // force the context to populate the data...just for improved error messaging

      try
      {
        var seasons = _ctx.Seasons.ToList();
      } 
      catch (Exception ex)
      {
        Debug.Print(ex.Message);
        var innerEx = ex.InnerException;

        while (innerEx != null)
        {
          Debug.Print("With inner exception of:");
          Debug.Print(innerEx.Message);

          innerEx = innerEx.InnerException;
        }

        Debug.Print(ex.StackTrace);

        throw ex;
      }
    }

    public IQueryable<Article> GetArticles()
    {
      return _ctx.Articles;
    }

    public IQueryable<TeamStanding> GetTeamStandings()
    {
      return _ctx.TeamStandings.Include("seasonTeam").Include("seasonTeam.team");
    }

    public IQueryable<PlayerStatSeason> GetPlayerStatsSeason()
    {
      return _ctx.PlayerStatsSeason.Include("season").Include("player").Include("seasonTeamPlayingFor").Include("seasonTeamPlayingFor.team");
    }

    public List<ForWebPlayerStat> GetPlayerStatsForWeb()
    {
      var data = _ctx.PlayerStatsSeason.Include("season").Include("player").Include("seasonTeamPlayingFor").Include("seasonTeamPlayingFor.team").ToList();

      var newData = new List<ForWebPlayerStat>();

      foreach (var item in data)
      {
        var ratings = _ctx.PlayerRatings.Where(x=> x.SeasonId == item.SeasonId && x.PlayerId == item.PlayerId).FirstOrDefault();

        var line = 0;
        if (ratings != null)
        {
          line = ratings.Line;
        }

        var playerName = item.Player.FirstName + " " + item.Player.LastName;
        if (!string.IsNullOrWhiteSpace(item.Player.Suffix))
        {
          playerName = playerName + " " + item.Player.Suffix;
        }

        newData.Add(new ForWebPlayerStat()
        {
          Player = playerName,
          Team = item.SeasonTeamPlayingFor.Team.TeamLongName,
          Sub = item.PlayerStatTypeId == 2 ? "Y" : "N",
          Pos = item.Player.PreferredPosition,
          Line = line,
          GP = item.Games,
          G = item.Goals,
          A = item.Assists,
          P = item.Points,
          PPG = item.PowerPlayGoals,
          SHG = item.ShortHandedGoals,
          GWG = item.GameWinningGoals,
          PIM = item.PenaltyMinutes
        });
      }
      return newData;
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

    public bool ProcessScoreSheetEntryPenalties(int startingGameId, int endingGameId)
    {
      return false;
    }

    public bool ProcessScoreSheetEntries(int startingGameId, int endingGameId)
    {
      try
      {
        var scoreSheetEntries = _ctx.ScoreSheetEntries
                                      .Include("Game")
                                      .Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId)
                                      .ToList();

        // loop through each game
        var savedScoreSheetEntries = 0;
        foreach (var scoreSheetEntry in scoreSheetEntries)
        {
          var gameId = scoreSheetEntry.GameId;

          // look up the home and away season team id
          // TODO..do this once per game, not per score sheet entry
          var gameTeams = _ctx.GameTeams.Where(gt => gt.GameId == gameId).ToList();
          var homeSeasonTeamId = gameTeams.Where(gt => gt.HomeTeam == true).FirstOrDefault().SeasonTeamId;
          var awaySeasonTeamId = gameTeams.Where(gt => gt.HomeTeam == false).FirstOrDefault().SeasonTeamId;

          // lookup team rosters
          var homeTeamRoster = _ctx.GameRosters
                                      .Include("Game")
                                      .Include("Player")
                                      .Where(x => x.GameId == gameId && x.SeasonTeamId == homeSeasonTeamId)
                                      .ToList();

          var awayTeamRoster = _ctx.GameRosters
                            .Include("Game")
                            .Include("Player")
                            .Where(x => x.GameId == gameId && x.SeasonTeamId == awaySeasonTeamId)
                            .ToList();

          var homeTeam = scoreSheetEntry.HomeTeam;
          var goalPlayerNumber = scoreSheetEntry.Goal;
          var assist1PlayerNumber = scoreSheetEntry.Assist1;
          var assist2PlayerNumber = scoreSheetEntry.Assist2;
          var assist3PlayerNumber = scoreSheetEntry.Assist3;

          ICollection<GameRoster> gameRosterToUse;
          if (homeTeam)
          {
            gameRosterToUse = homeTeamRoster;
          }
          else 
          {
            gameRosterToUse = awayTeamRoster;
          }

          // lookup player ids
          var goalPlayerId = convertPlayerNumberIntoPlayer(gameRosterToUse, goalPlayerNumber);
          var assist1PlayerId = convertPlayerNumberIntoPlayer(gameRosterToUse, assist1PlayerNumber);
          var assist2PlayerId = convertPlayerNumberIntoPlayer(gameRosterToUse, assist2PlayerNumber);
          var assist3PlayerId = convertPlayerNumberIntoPlayer(gameRosterToUse, assist3PlayerNumber);

          // determine type goal
          // TODO improve this logic
          bool shortHandedGoal = scoreSheetEntry.ShortHandedPowerPlay == "SH" ? true : false;
          bool powerPlayGoal = scoreSheetEntry.ShortHandedPowerPlay == "PP" ? true : false;
          bool gameWinningGoal = false;

          var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed()
          {
            ScoreSheetEntryId = scoreSheetEntry.ScoreSheetEntryId,
            GameId = scoreSheetEntry.GameId,
            Period = scoreSheetEntry.Period,
            HomeTeam = scoreSheetEntry.HomeTeam,
            GoalPlayerId = Convert.ToInt32(goalPlayerId),
            Assist1PlayerId = assist1PlayerId,
            Assist2PlayerId = assist2PlayerId,
            Assist3PlayerId = assist3PlayerId,
            TimeRemaining = scoreSheetEntry.TimeRemaining,
            ShortHandedGoal = shortHandedGoal,
            PowerPlayGoal = powerPlayGoal,
            GameWinningGoal = gameWinningGoal
          };

          var results = _lo30ContextService.SaveScoreSheetEntryProcessed(scoreSheetEntryProcessed);
          savedScoreSheetEntries = savedScoreSheetEntries + results;
        };

        return savedScoreSheetEntries > 0;
      }
      catch (Exception ex)
      {
        Debug.Print("Following error occurred:" + ex.StackTrace);
        return false;
      }
    }

    public bool ProcessScoreSheetEntriesIntoGameResults(int startingGameId, int endingGameId)
    {
      try
      {
        // get list of game entries for these games (use game just in case there was no score sheet entries...0-0 game with no penalty minutes)
        var games = _ctx.Games.Where(s => s.GameId >= startingGameId && s.GameId <= endingGameId).ToList();

        // get a list of periods
        var periods = new int[] { 1, 2, 3, 4 };

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

            // save game score for the period
            _lo30ContextService.SaveGameScore(gameId, period, homeSeasonTeamId, scoreHomeTeamPeriod, awaySeasonTeamId, scoreAwayTeamPeriod);

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

          // save game score for the game
          var finalPeriod = 0;
          _lo30ContextService.SaveGameScore(gameId, finalPeriod, homeSeasonTeamId, scoreHomeTeamTotal, awaySeasonTeamId, scoreAwayTeamTotal);

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
          _lo30ContextService.SaveGameResults(gameId, homeSeasonTeamId, scoreHomeTeamTotal, penaltyHomeTeamTotal, homeResult, awaySeasonTeamId, scoreAwayTeamTotal, penaltyAwayTeamTotal, awayResult);
        }

        return _ctx.SaveChanges() > 0;
      }
      catch (Exception ex)
      {
        Debug.Print("Following error occurred:" + ex.StackTrace);
        return false;
      }
    }

    public bool ProcessGameResultsIntoTeamStandings(int seasonId, int seasonTypeId, int startingGameId, int endingGameId)
    {
      try
      {
        // get every team just in case they do not have a game result yet
        var seasonTeams = _ctx.SeasonTeams.Where(st => st.SeasonId == seasonId).ToList();

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

            // get game results for this season team
            var gameResults = _ctx.GameResults.Where(gr => gr.GameId >= startingGameId && gr.GameId <= endingGameId && gr.SeasonTeamId == seasonTeam.SeasonTeamId).ToList();

            // loop through each game
            //int seasonTypeId=-1; // TODO, make sure teh games match seasonTYpe
            for (var g = 0; g < gameResults.Count; g++)
            {
              var gameResult = gameResults[g];

              // look up type of season for game to be applied to standings
              var game = _ctx.Games.Where(gg => gg.GameId == gameResult.GameId).FirstOrDefault();
              //seasonTypeId = game.SeasonTypeId;

              if (gameResult.Result.ToLower() == "w")
              {
                wins++;
              }
              else if (gameResult.Result.ToLower() == "l")
              {
                losses++;
              }
              else
              {
                ties++;
              }

              games++;
              points = (wins * 2) + (losses * 0) + (ties * 1);

              goalsFor = goalsFor + gameResult.GoalsFor;
              goalsAgainst = goalsAgainst + gameResult.GoalsAgainst;
              penaltyMinutes = penaltyMinutes + gameResult.PenaltyMinutes;
            }

            var rank = -1;
            _lo30ContextService.SaveTeamStanding(seasonTeam.SeasonTeamId, seasonTypeId, rank, games, wins, losses, ties, points, goalsFor, goalsAgainst, penaltyMinutes);
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
          _lo30ContextService.SaveTeamStanding(s.SeasonTeamId, s.SeasonTypeId, rank, s.Games, s.Wins, s.Losses, s.Ties, s.Points, s.GoalsFor, s.GoalsAgainst, s.PenaltyMinutes);
        }
        return _ctx.SaveChanges() > 0;
      }
      catch (Exception ex)
      {
        Debug.Print("Following error occurred:" + ex.StackTrace);
        return false;
      }
    }

    public bool ProcessScoreSheetEntriesIntoPlayerStats(int startingGameId, int endingGameId)
    {
      try
      {
        // TODO...maybe filter this to only "available" players
        var players = _ctx.Players.ToList();
        var games = _ctx.Games.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var gameRosters = _ctx.GameRosters.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var scoreSheetEntriesProcessed = _ctx.ScoreSheetEntriesProcessed.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();
        var scoreSheetEntryPenaltiesProcessed = _ctx.ScoreSheetEntryPenaltiesProcessed.Where(x => x.GameId >= startingGameId && x.GameId <= endingGameId).ToList();


        var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(players, games, gameRosters, scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed);
        var playerSeasonStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonStats(playerGameStats);

        var savedStatsGame = _lo30ContextService.SavePlayerStatGame(playerGameStats);
        Debug.Print("ProcessScoreSheetEntriesIntoPlayerStats: savedStatsGame:" + savedStatsGame);

        var savedStatsSeason = _lo30ContextService.SavePlayerStatSeason(playerSeasonStats);
        Debug.Print("ProcessScoreSheetEntriesIntoPlayerStats: savedStatsSeason:" + savedStatsSeason);

        return (savedStatsSeason + savedStatsGame) > 0;
      }
      catch (Exception ex)
      {
        Debug.Print(ex.Message);
        var innerEx = ex.InnerException;

        while (innerEx != null)
        {
          Debug.Print("With inner exception of:");
          Debug.Print(innerEx.Message);

          innerEx = innerEx.InnerException;
        }

        Debug.Print(ex.StackTrace);

        throw ex;
      }
    }

    public int? convertPlayerNumberIntoPlayer(ICollection<GameRoster> gameRoster, int? playerNumber)
    {
      int? playerId = null;

      if (playerNumber != null)
      {
        var gameRosterMatch = gameRoster.Where(x => x.PlayerNumber == playerNumber).FirstOrDefault();
        if (gameRosterMatch == null)
        {
          playerId = 0; // the unknown player
        }
        else
        {
          playerId = gameRosterMatch.PlayerId;
        }
      }

      return playerId;
    }
  }
}