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
    public Lo30Repository(Lo30Context ctx)
    {
      _ctx = ctx;
    }

    public IQueryable<Article> GetArticles()
    {
      return _ctx.Articles;
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
            saveGameScore(gameId, period, homeSeasonTeamId, scoreHomeTeamPeriod, awaySeasonTeamId, scoreAwayTeamPeriod);

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
          saveGameScore(gameId, finalPeriod, homeSeasonTeamId, scoreHomeTeamTotal, awaySeasonTeamId, scoreAwayTeamTotal);

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
          saveGameResults(gameId, homeSeasonTeamId, scoreHomeTeamTotal, penaltyHomeTeamTotal, homeResult, awaySeasonTeamId, scoreAwayTeamTotal, penaltyAwayTeamTotal, awayResult);
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
            saveTeamStanding(seasonTeam.SeasonTeamId, seasonTypeId, rank, games, wins, losses, ties, points, goalsFor, goalsAgainst, penaltyMinutes);
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
          saveTeamStanding(s.SeasonTeamId, s.SeasonTypeId, rank, s.Games, s.Wins, s.Losses, s.Ties, s.Points, s.GoalsFor, s.GoalsAgainst, s.PenaltyMinutes);
        }
        return _ctx.SaveChanges() > 0;
      }
      catch (Exception ex)
      {
        Debug.Print("Following error occurred:" + ex.StackTrace);
        return false;
      }
    }

    public void saveTeamStanding(int seasonTeamId, int seasonTypeId, int rank, int games, int wins, int losses, int ties, int points, int goalsFor, int goalsAgainst, int penaltyMinutes)
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

      try
      {
        _ctx.SaveChanges();
      }
      catch (Exception ex)
      {
        Debug.Print("Following error occurred:" + ex.StackTrace);
        throw ex;
      }
    }

    public void saveGameScore(int gameId, int period, int homeSeasonTeamId, int scoreHomeTeamPeriod, int awaySeasonTeamId, int scoreAwayTeamPeriod)
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
      _ctx.SaveChanges();
    }

    public void saveGameResults(int gameId, int homeSeasonTeamId, int homeTeamScore, int homeTeamPenalties, string homeResult, int awaySeasonTeamId, int awayTeamScore, int awayTeamPenalties, string awayResult)
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
    }
  }
}