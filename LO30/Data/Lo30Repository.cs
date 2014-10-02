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
    }

    public IQueryable<Article> GetArticles()
    {
      return _ctx.Articles;
    }

    public IQueryable<TeamStanding> GetTeamStandings()
    {
      return _ctx.TeamStandings.Include("seasonTeam").Include("seasonTeam.team");
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

          var results = SaveScoreSheetEntryProcessed(scoreSheetEntryProcessed);
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
            SaveGameScore(gameId, period, homeSeasonTeamId, scoreHomeTeamPeriod, awaySeasonTeamId, scoreAwayTeamPeriod);

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
          SaveGameScore(gameId, finalPeriod, homeSeasonTeamId, scoreHomeTeamTotal, awaySeasonTeamId, scoreAwayTeamTotal);

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
          SaveGameResults(gameId, homeSeasonTeamId, scoreHomeTeamTotal, penaltyHomeTeamTotal, homeResult, awaySeasonTeamId, scoreAwayTeamTotal, penaltyAwayTeamTotal, awayResult);
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
            SaveTeamStanding(seasonTeam.SeasonTeamId, seasonTypeId, rank, games, wins, losses, ties, points, goalsFor, goalsAgainst, penaltyMinutes);
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
          SaveTeamStanding(s.SeasonTeamId, s.SeasonTypeId, rank, s.Games, s.Wins, s.Losses, s.Ties, s.Points, s.GoalsFor, s.GoalsAgainst, s.PenaltyMinutes);
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
        // get list of players
        // TODO...maybe filter this to only "available" players
        var players = _ctx.Players.ToList();

        // get list of game entries for these games (use game just in case there was no score sheet entries...0-0 game with no penalty minutes)
        var games = _ctx.Games.Where(s => s.GameId >= startingGameId && s.GameId <= endingGameId).ToList();

        int savedStatsCareer = 0;
        int savedStatsSeason = 0;
        int savedStatsGame = 0;
        int saved = 0;

        // loop through each player
        foreach (var player in players)
        {
          var playerId = player.PlayerId;
          int gamesCareer = 0;
          int goalsCareer = 0;
          int assistsCareer = 0;
          int penaltyMinutesCareer = 0;
          int powerPlayGoalsCareer = 0;
          int shortHandedGoalsCareer = 0;
          int gameWinningGoalsCareer = 0;
          saved = 0;

          // get list of games that this player played in
          var gameRostersPlayerPlayedIn = _ctx.GameRosters.Where(x => x.PlayerId == playerId).ToList();

          // get list of distinct seasons that those games cover
          var seasonsPlayerPlayedIn = gameRostersPlayerPlayedIn.Select(x => x.SeasonTeam.SeasonId).Distinct();

          #region process all seasons for that player
          foreach (var seasonId in seasonsPlayerPlayedIn)
          {
            int gamesSeason = 0;
            int goalsSeason = 0;
            int assistsSeason = 0;
            int penaltyMinutesSeason = 0;
            int powerPlayGoalsSeason = 0;
            int shortHandedGoalsSeason = 0;
            int gameWinningGoalsSeason = 0;
            saved = 0;

            // get list of distinct games for this season
            var gamesPlayerPlayedIn = gameRostersPlayerPlayedIn
                                        .Where(x => x.SeasonTeam.SeasonId == seasonId)
                                        .Select(x => x.GameId)
                                        .Distinct();

            #region process all games for that season for that player
            foreach (var gameId in gamesPlayerPlayedIn)
            {
              gamesSeason++;

              int goalsGame = 0;
              int assistsGame = 0;
              int penaltyMinutesGame = 0;
              int powerPlayGoalsGame = 0;
              int shortHandedGoalsGame = 0;
              int gameWinningGoalsGame = 0;
              saved = 0;

              #region process all score sheet entries for this specific game/player
              var scoreSheetEntries = _ctx.ScoreSheetEntriesProcessed
                                          .Where(x =>
                                            x.GameId == gameId &&
                                            (
                                              x.GoalPlayerId == playerId ||
                                              x.Assist1PlayerId == playerId ||
                                              x.Assist2PlayerId == playerId ||
                                              x.Assist3PlayerId == playerId
                                            )
                                          )
                                          .ToList();

              foreach (var scoreSheetEntry in scoreSheetEntries)
              {
                if (scoreSheetEntry.GoalPlayerId == playerId)
                {
                  goalsGame++;
                  goalsSeason++;

                  if (scoreSheetEntry.ShortHandedGoal)
                  {
                    shortHandedGoalsGame++;
                    shortHandedGoalsSeason++;
                  }
                  else if (scoreSheetEntry.PowerPlayGoal)
                  {
                    powerPlayGoalsGame++;
                    powerPlayGoalsSeason++;
                  }

                  // can be (shorthanded or powerplay) and a game winner...so not else if here, just if
                  if (scoreSheetEntry.GameWinningGoal)
                  {
                    gameWinningGoalsGame++;
                    gameWinningGoalsSeason++;
                  }

                }
                else
                {
                  // the score sheet entry must match this player on a goal or assist
                  assistsGame++;
                  assistsSeason++;
                }

              }
              #endregion

              #region process all score sheet entry penalties for this specific game/player
              var scoreSheetEntryPenalties = _ctx.ScoreSheetEntryPenaltiesProcessed
                  .Where(x => x.GameId == gameId && x.PlayerId == player.PlayerId)
                  .ToList();

              foreach (var scoreSheetEntryPenalty in scoreSheetEntryPenalties)
              {
                penaltyMinutesGame = penaltyMinutesGame + scoreSheetEntryPenalty.PenaltyMinutes;
                penaltyMinutesSeason = penaltyMinutesSeason + scoreSheetEntryPenalty.PenaltyMinutes;
              }
              #endregion


              // now save player stat game
              var playerStatGame = new PlayerStatGame()
              {
                GameId = gameId,
                PlayerId = playerId,
                PlayerStatTypeId = 1,
                Goals = goalsGame,
                Assists = assistsGame,
                Points = goalsGame + assistsGame,
                PenaltyMinutes = penaltyMinutesGame,
                ShortHandedGoals = shortHandedGoalsGame,
                PowerPlayGoals = powerPlayGoalsGame,
                GameWinningGoals = gameWinningGoalsGame
              };

              // save game score for the period
              saved = SavePlayerStatGame(playerStatGame);
              savedStatsGame = savedStatsGame + saved;
            }
            #endregion

            // now save player stat season
            var playerStatSeason = new PlayerStatSeason()
            {
              SeasonId = seasonId,
              PlayerId = playerId,
              PlayerStatTypeId = 1,
              Games = gamesSeason,
              Goals = goalsSeason,
              Assists = assistsSeason,
              Points = goalsSeason + assistsSeason,
              PenaltyMinutes = penaltyMinutesSeason,
              ShortHandedGoals = shortHandedGoalsSeason,
              PowerPlayGoals = powerPlayGoalsSeason,
              GameWinningGoals = gameWinningGoalsSeason
            };

            // save game score for the period
            saved = SavePlayerStatSeason(playerStatSeason);
            savedStatsSeason = savedStatsSeason + saved;
          }
          #endregion
        }

        return (savedStatsSeason + savedStatsGame) > 0;
      }
      catch (Exception ex)
      {
        Debug.Print("Following error occurred:" + ex.StackTrace);
        return false;
      }
    }

    public int SaveTeamStanding(int seasonTeamId, int seasonTypeId, int rank, int games, int wins, int losses, int ties, int points, int goalsFor, int goalsAgainst, int penaltyMinutes)
    {
      var teamStanding = new TeamStanding()
      {
        SeasonTeamId = seasonTeamId,
        SeasonTypeId = seasonTypeId,
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

      var found = _ctx.TeamStandings.Find(teamStanding.SeasonTeamId, teamStanding.SeasonTypeId);

      if (found == null)
      {
        found = _ctx.TeamStandings.Add(teamStanding);
      }
      else
      {
        var entry = _ctx.Entry(found);
        entry.OriginalValues.SetValues(found);
        entry.CurrentValues.SetValues(teamStanding);
      }

      return ContextSaveChanges();
    }

    public int SaveGameScore(int gameId, int period, int homeSeasonTeamId, int scoreHomeTeamPeriod, int awaySeasonTeamId, int scoreAwayTeamPeriod)
    {
      var gameScore = new GameScore()
      {
        GameId = gameId,
        Period = period,
        SeasonTeamId = homeSeasonTeamId,
        Score = scoreHomeTeamPeriod
      };

      var found = _ctx.GameScores.Find(gameScore.GameId, gameScore.SeasonTeamId, gameScore.Period);

      if (found == null)
      {
        found = _ctx.GameScores.Add(gameScore);
      }
      else
      {
        var entry = _ctx.Entry(found);
        entry.OriginalValues.SetValues(found);
        entry.CurrentValues.SetValues(gameScore);
      }

      gameScore = new GameScore()
      {
        GameId = gameId,
        Period = period,
        SeasonTeamId = awaySeasonTeamId,
        Score = scoreAwayTeamPeriod
      };

      found = _ctx.GameScores.Find(gameScore.GameId, gameScore.SeasonTeamId, gameScore.Period);

      if (found == null)
      {
        found = _ctx.GameScores.Add(gameScore);
      }
      else
      {
        var entry = _ctx.Entry(found);
        entry.OriginalValues.SetValues(found);
        entry.CurrentValues.SetValues(gameScore);
      }

      return ContextSaveChanges();
    }

    public int SaveGameResults(int gameId, int homeSeasonTeamId, int homeTeamScore, int homeTeamPenalties, string homeResult, int awaySeasonTeamId, int awayTeamScore, int awayTeamPenalties, string awayResult)
    {
      var gameResult = new GameResult()
      {
        GameId = gameId,
        SeasonTeamId = homeSeasonTeamId,
        Result = homeResult,
        GoalsFor = homeTeamScore,
        GoalsAgainst = awayTeamScore,
        PenaltyMinutes = homeTeamPenalties,
        Override = false
      };

      var found = _ctx.GameResults.Find(gameResult.GameId, gameResult.SeasonTeamId);

      if (found == null)
      {
        found = _ctx.GameResults.Add(gameResult);
      }
      else
      {
        var entry = _ctx.Entry(found);
        entry.OriginalValues.SetValues(found);
        entry.CurrentValues.SetValues(gameResult);
      }

      gameResult = new GameResult()
      {
        GameId = gameId,
        SeasonTeamId = awaySeasonTeamId,
        Result = awayResult,
        GoalsFor = awayTeamScore,
        GoalsAgainst = homeTeamScore,
        PenaltyMinutes = awayTeamPenalties,
        Override = false
      };

      found = _ctx.GameResults.Find(gameResult.GameId, gameResult.SeasonTeamId);

      if (found == null)
      {
        found = _ctx.GameResults.Add(gameResult);
      }
      else
      {
        var entry = _ctx.Entry(found);
        entry.OriginalValues.SetValues(found);
        entry.CurrentValues.SetValues(gameResult);
      }

      return ContextSaveChanges();
    }

    public int SaveScoreSheetEntryProcessed(int scoreSheetEntryId, int gameId, int period, bool homeTeam, int goalPlayerId, int? assist1PlayerId, int? assist2PlayerId, int? assist3PlayerId, string timeRemaining, bool shortHandedGoal, bool powerPlayGoal, bool gameWinningGoal)
    {
      var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed()
      {
        ScoreSheetEntryId = scoreSheetEntryId,
        GameId = gameId,
        Period = period,
        HomeTeam = homeTeam,
        GoalPlayerId = goalPlayerId,
        Assist1PlayerId = assist1PlayerId,
        Assist2PlayerId = assist2PlayerId,
        Assist3PlayerId = assist3PlayerId,
        TimeRemaining = timeRemaining,
        ShortHandedGoal = shortHandedGoal,
        PowerPlayGoal = powerPlayGoal,
        GameWinningGoal = gameWinningGoal
      };

      return SaveScoreSheetEntryProcessed(scoreSheetEntryProcessed);
    }

    public int SaveScoreSheetEntryProcessed(ScoreSheetEntryProcessed scoreSheetEntryProcessed)
    {
      var found = _ctx.ScoreSheetEntriesProcessed.Find(scoreSheetEntryProcessed.ScoreSheetEntryId);

      if (found == null)
      {
        found = _ctx.ScoreSheetEntriesProcessed.Add(scoreSheetEntryProcessed);
      }
      else
      {
        var entry = _ctx.Entry(found);
        entry.OriginalValues.SetValues(found);
        entry.CurrentValues.SetValues(scoreSheetEntryProcessed);
      }

      return ContextSaveChanges();
    }

    public int SavePlayerStatGame(PlayerStatGame playerStatGame)
    {
      var found = _ctx.PlayerStatsGame.Find(playerStatGame.GameId, playerStatGame.PlayerId, playerStatGame.PlayerStatTypeId);

      if (found == null)
      {
        found = _ctx.PlayerStatsGame.Add(playerStatGame);
      }
      else
      {
        var entry = _ctx.Entry(found);
        entry.OriginalValues.SetValues(found);
        entry.CurrentValues.SetValues(playerStatGame);
      }

      return ContextSaveChanges();
    }

    public int SavePlayerStatSeason(PlayerStatSeason playerStatSeason)
    {
      var found = _ctx.PlayerStatsSeason.Find(playerStatSeason.SeasonId, playerStatSeason.PlayerId, playerStatSeason.PlayerStatTypeId);

      if (found == null)
      {
        found = _ctx.PlayerStatsSeason.Add(playerStatSeason);
      }
      else
      {
        var entry = _ctx.Entry(found);
        entry.OriginalValues.SetValues(found);
        entry.CurrentValues.SetValues(playerStatSeason);
      }

      return ContextSaveChanges();
    }

    private int ContextSaveChanges()
    {
      try
      {
        return _ctx.SaveChanges();
      }
      catch (Exception ex)
      {
        Debug.Print("Following error occurred:" + ex.StackTrace);
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