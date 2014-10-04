using LO30.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LO30.Services
{
    public class LO30ContextService
    {
      Lo30Context _ctx;

      public LO30ContextService(Lo30Context ctx)
      {
        _ctx = ctx;
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

      public int SavePlayerStatGame(List<PlayerStatGame> playerStatGame)
      {
        var saved = 0;
        foreach (var item in playerStatGame)
        {
          var results = SavePlayerStatGame(item);
          saved = saved + results;
        }

        return saved;
      }

      public int SavePlayerStatGame(PlayerStatGame playerStatGame)
      {
        var found = _ctx.PlayerStatsGame.Find(playerStatGame.SeasonId, playerStatGame.PlayerId, playerStatGame.PlayerStatTypeId, playerStatGame.SeasonTeamIdPlayingFor, playerStatGame.GameId);

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

      public int SavePlayerStatSeason(List<PlayerStatSeason> playerStatSeason)
      {
        var saved = 0;
        foreach (var item in playerStatSeason)
        {
          var results = SavePlayerStatSeason(item);
          saved = saved + results;
        }

        return saved;
      }

      public int SavePlayerStatSeason(PlayerStatSeason playerStatSeason)
      {
        var found = _ctx.PlayerStatsSeason.Find(playerStatSeason.SeasonId, playerStatSeason.PlayerId, playerStatSeason.PlayerStatTypeId, playerStatSeason.SeasonTeamIdPlayingFor);

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
    }
}