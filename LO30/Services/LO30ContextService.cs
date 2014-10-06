using LO30.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LO30.Services
{
    public class Lo30ContextService
    {
      Lo30Context _ctx;

      public Lo30ContextService(Lo30Context ctx)
      {
        _ctx = ctx;
      }

      #region SaveOrUpdate functions

      public int SaveTeamStanding(int seasonTeamId, bool playoff, int rank, int games, int wins, int losses, int ties, int points, int goalsFor, int goalsAgainst, int penaltyMinutes)
      {
        var teamStanding = new TeamStanding()
        {
          SeasonTeamId = seasonTeamId,
          Playoff = playoff,
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

        var found = _ctx.TeamStandings.Find(teamStanding.SeasonTeamId, teamStanding.Playoff);

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

      public int SaveOrUpdateGameScore(GameScore gameScore)
      {
        var found = _ctx.GameScores.Where(x => x.GameTeamId == gameScore.GameTeamId && x.Period == gameScore.Period).FirstOrDefault();

        if (found == null)
        {
          found = _ctx.GameScores.Add(gameScore);
        }
        else
        {
          // set the primary key if 0
          if (gameScore.GameScoreId == 0)
          {
            gameScore.GameScoreId = found.GameScoreId;
          }

          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(gameScore);
        }

        return ContextSaveChanges();
      }

      public int SaveOrUpdateGameOutcome(GameOutcome gameOutcome)
      {
        var found = _ctx.GameOutcomes.Where(x => x.GameTeamId == gameOutcome.GameTeamId).FirstOrDefault();

        if (found == null)
        {
          found = _ctx.GameOutcomes.Add(gameOutcome);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(gameOutcome);
        }

        return ContextSaveChanges();
      }

      public int SaveScoreSheetEntryProcessed(int scoreSheetEntryId, int gameId, int period, bool homeTeam, int goalPlayerId, int? assist1PlayerId, int? assist2PlayerId, int? assist3PlayerId, string timeRemaining, bool shortHandedGoal, bool powerPlayGoal, bool gameWinningGoal)
      {
        var scoreSheetEntryProcessed = new ScoreSheetEntryProcessed(
                                              sseid: scoreSheetEntryId,

                                              gid: gameId,
                                              per: period,
                                              ht: homeTeam,
                                              time: timeRemaining,

                                              gpid: goalPlayerId,
                                              a1pid: assist1PlayerId,
                                              a2pid: assist2PlayerId,
                                              a3pid: assist3PlayerId,
          
                                              shg: shortHandedGoal,
                                              ppg: powerPlayGoal,
                                              gwg: gameWinningGoal
                                            );

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

      public int SaveOrUpdatePlayerStatGame(List<PlayerStatGame> playerStatGame)
      {
        var saved = 0;
        foreach (var item in playerStatGame)
        {
          var results = SaveOrUpdatePlayerStatGame(item);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayerStatGame(PlayerStatGame playerStatGame)
      {
        var found = FindPlayerStatGame(errorIfNotFound: false, errorIfMoreThanOneFound: true, playerId: playerStatGame.PlayerId, gameId: playerStatGame.GameId);

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

      public int SaveOrUpdatePlayerStatSeasonTeam(List<PlayerStatSeasonTeam> playerStatSeasonTeam)
      {
        var saved = 0;
        foreach (var item in playerStatSeasonTeam)
        {
          var results = SaveOrUpdatePlayerStatSeasonTeam(item);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayerStatSeasonTeam(PlayerStatSeasonTeam playerStatSeasonTeam)
      {
        var found = FindPlayerStatSeasonTeam(errorIfNotFound: false, errorIfMoreThanOneFound: true, playerId: playerStatSeasonTeam.PlayerId, seasonTeamIdPlayingFor: playerStatSeasonTeam.SeasonTeamIdPlayingFor);

        if (found == null)
        {
          found = _ctx.PlayerStatsSeasonTeam.Add(playerStatSeasonTeam);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(playerStatSeasonTeam);
        }

        return ContextSaveChanges();
      }

      public int SaveOrUpdatePlayerStatSeason(List<PlayerStatSeason> playerStatSeason)
      {
        var saved = 0;
        foreach (var item in playerStatSeason)
        {
          var results = SaveOrUpdatePlayerStatSeason(item);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayerStatSeason(PlayerStatSeason playerStatSeason)
      {
        var found = FindPlayerStatSeason(errorIfNotFound: false, errorIfMoreThanOneFound: true, playerId: playerStatSeason.PlayerId, seasonId: playerStatSeason.SeasonId, sub: playerStatSeason.Sub);

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

      public int SaveOrUpdateGoalieStatGame(List<GoalieStatGame> goalieStatGame)
      {
        var saved = 0;
        foreach (var item in goalieStatGame)
        {
          var results = SaveOrUpdateGoalieStatGame(item);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGoalieStatGame(GoalieStatGame goalieStatGame)
      {
        var found = FindGoalieStatGame(errorIfNotFound: false, errorIfMoreThanOneFound: true, playerId: goalieStatGame.PlayerId, gameId: goalieStatGame.GameId);

        if (found == null)
        {
          _ctx.GoalieStatsGame.Add(goalieStatGame);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(goalieStatGame);
        }

        return ContextSaveChanges();
      }

      public int SaveOrUpdateGoalieStatSeasonTeam(List<GoalieStatSeasonTeam> goalieStatSeasonTeam)
      {
        var saved = 0;
        foreach (var item in goalieStatSeasonTeam)
        {
          var results = SaveOrUpdateGoalieStatSeasonTeam(item);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGoalieStatSeasonTeam(GoalieStatSeasonTeam goalieStatSeasonTeam)
      {
        var found = FindGoalieStatSeasonTeam(errorIfNotFound: false, errorIfMoreThanOneFound: true, playerId: goalieStatSeasonTeam.PlayerId, seasonTeamIdPlayingFor: goalieStatSeasonTeam.SeasonTeamIdPlayingFor);

        if (found == null)
        {
          _ctx.GoalieStatsSeasonTeam.Add(goalieStatSeasonTeam);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(goalieStatSeasonTeam);
        }

        return ContextSaveChanges();
      }

      public int SaveOrUpdateGoalieStatSeason(List<GoalieStatSeason> goalieStatSeason)
      {
        var saved = 0;
        foreach (var item in goalieStatSeason)
        {
          var results = SaveOrUpdateGoalieStatSeason(item);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGoalieStatSeason(GoalieStatSeason goalieStatSeason)
      {
        var found = FindGoalieStatSeason(errorIfNotFound: false, errorIfMoreThanOneFound: true, playerId: goalieStatSeason.PlayerId, seasonId: goalieStatSeason.SeasonId, sub: goalieStatSeason.Sub);

        if (found == null)
        {
          _ctx.GoalieStatsSeason.Add(goalieStatSeason);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(goalieStatSeason);
        }

        return ContextSaveChanges();
      }

      #endregion

      #region find functions

      #region FindGameRosters
      public List<GameRoster> FindGameRostersWithGameIdsAndGoalie(int startingGameId, int endingGameId, bool goalie)
      {
        return FindGameRostersWithGameIdsAndGoalie(errorIfNotFound: true, startingGameId: startingGameId, endingGameId: endingGameId, goalie: goalie);
      }

      public List<GameRoster> FindGameRostersWithGameIdsAndGoalie(bool errorIfNotFound, int startingGameId, int endingGameId, bool goalie)
      {
        var found = _ctx.GameRosters.Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId && x.Goalie == goalie).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameRosters for" +
                                                  " GameTeam.GameId (starting):" + startingGameId +
                                                  " GameTeam.GameId (ending):" + endingGameId +
                                                  " Goalie:" + goalie
                                          );
        }

        return found;
      }

      public List<GameRoster> FindGameRostersWithGameIds(int startingGameId, int endingGameId)
      {
        return FindGameRostersWithGameIds(errorIfNotFound: true, startingGameId: startingGameId, endingGameId: endingGameId);
      }

      public List<GameRoster> FindGameRostersWithGameIds(bool errorIfNotFound, int startingGameId, int endingGameId)
      {
        var found = _ctx.GameRosters.Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameRosters for" +
                                                  " GameTeam.GameId (starting):" + startingGameId +
                                                  " GameTeam.GameId (ending):" + endingGameId
                                          );
        }

        return found;
      }

      public GameRoster FindGameRosterWithGameRosterId(int gameRosterId)
      {
        return FindGameRosterWithGameRosterId(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameRosterId: gameRosterId);
      }

      public GameRoster FindGameRosterWithGameRosterId(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameRosterId)
      {
        var found = _ctx.GameRosters.Where(x => x.GameRosterId == gameRosterId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameRoster for" +
                                                  " GameRosterId:" + gameRosterId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameRoster was not found for" +
                                                  " GameRosterId:" + gameRosterId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region FindGameOutcome
      public List<GameOutcome> FindGameOutcomesWithGameIdsAndTeamId(int startingGameId, int endingGameId, int seasonTeamId)
      {
        return FindGameOutcomesWithGameIdsAndTeamId(errorIfNotFound: true, startingGameId: startingGameId, endingGameId: endingGameId, seasonTeamId: seasonTeamId);
      }

      public List<GameOutcome> FindGameOutcomesWithGameIdsAndTeamId(bool errorIfNotFound, int startingGameId, int endingGameId, int seasonTeamId)
      {
        var found = _ctx.GameOutcomes.Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId && x.GameTeam.SeasonTeamId == seasonTeamId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameOutcome for" +
                                                  " GameTeam.GameId (starting):" + startingGameId +
                                                  " GameTeam.GameId (ending):" + endingGameId +
                                                  " GameTeam.SeasonTeamId:" + seasonTeamId
                                          );
        }

        return found;
      }

      public List<GameOutcome> FindGameOutcomesWithGameIds(int startingGameId, int endingGameId)
      {
        return FindGameOutcomesWithGameIds(errorIfNotFound: true, startingGameId: startingGameId, endingGameId: endingGameId);
      }

      public List<GameOutcome> FindGameOutcomesWithGameIds(bool errorIfNotFound, int startingGameId, int endingGameId)
      {
        var found = _ctx.GameOutcomes.Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameOutcome for" +
                                                  " GameTeam.GameId (starting):" + startingGameId +
                                                  " GameTeam.GameId (ending):" + endingGameId
                                          );
        }

        return found;
      }

      public GameOutcome FindGameOutcomeWithGameTimeId(int gameTimeId)
      {
        return FindGameOutcomeWithGameTimeId(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameTimeId: gameTimeId);
      }

      public GameOutcome FindGameOutcomeWithGameTimeId(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameTimeId)
      {
        var found = _ctx.GameOutcomes.Where(x => x.GameTeamId == gameTimeId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameOutcome for" +
                                                  " GameTeamId:" + gameTimeId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameOutcome was not found for" +
                                                  " GameTeamId:" + gameTimeId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region FindGameTeam
      public GameTeam FindGameTeam(int gameId, bool homeTeam)
      {
        return FindGameTeam(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameId: gameId, homeTeam: homeTeam);
      }

      public GameTeam FindGameTeam(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameId, bool homeTeam)
      {
        var found = _ctx.GameTeams.Where(x => x.GameId == gameId && x.HomeTeam == homeTeam).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameTeam for" +
                                                  " GameId:" + gameId +
                                                  " HomeTeam:" + homeTeam
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameTeam was not found for" +
                                                  " GameId:" + gameId +
                                                  " HomeTeam:" + homeTeam
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region FindPlayerStatGame
      public PlayerStatGame FindPlayerStatGame(int playerId, int gameId)
      {
        return FindPlayerStatGame(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, gameId: gameId);
      }

      public PlayerStatGame FindPlayerStatGame(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int gameId)
      {
        var found = _ctx.PlayerStatsGame.Where(x => x.PlayerId == playerId && x.GameId == gameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerStatGame for" +
                                                  " PlayerId:" + playerId +
                                                  " GameId:" + gameId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerStatGame was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " GameId:" + gameId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        } 
        else
        {
          return null;
        }
      }
      #endregion

      #region FindPlayerStatSeasonTeam
      public PlayerStatSeasonTeam FindPlayerStatSeasonTeam(int playerId, int seasonTeamIdPlayingFor)
      {
        return FindPlayerStatSeasonTeam(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, seasonTeamIdPlayingFor: seasonTeamIdPlayingFor);
      }

      public PlayerStatSeasonTeam FindPlayerStatSeasonTeam(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int seasonTeamIdPlayingFor)
      {
        var found = _ctx.PlayerStatsSeasonTeam.Where(x => x.PlayerId == playerId && x.SeasonTeamIdPlayingFor == seasonTeamIdPlayingFor).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerStatsSeasonTeam for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonTeamIdPlayingFor:" + seasonTeamIdPlayingFor
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerStatsSeasonTeam was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonTeamIdPlayingFor:" + seasonTeamIdPlayingFor
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region FindPlayerStatSeason
      public PlayerStatSeason FindPlayerStatSeason(int playerId, int seasonId, bool sub)
      {
        return FindPlayerStatSeason(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, seasonId: seasonId, sub: sub);
      }

      public PlayerStatSeason FindPlayerStatSeason(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int seasonId, bool sub)
      {
        var found = _ctx.PlayerStatsSeason.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId && x.Sub == sub).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerStatsSeason for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonId:" + seasonId +
                                                  " Sub:" + sub
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerStatsSeason was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonId:" + seasonId +
                                                  " Sub:" + sub
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region FindGoalieStatGame
      public GoalieStatGame FindGoalieStatGame(int playerId, int gameId)
      {
        return FindGoalieStatGame(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, gameId: gameId);
      }

      public GoalieStatGame FindGoalieStatGame(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int gameId)
      {
        var found = _ctx.GoalieStatsGame.Where(x => x.PlayerId == playerId && x.GameId == gameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GoalieStatsGame for" +
                                                  " PlayerId:" + playerId +
                                                  " GameId:" + gameId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GoalieStatsGame was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " GameId:" + gameId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region FindGoalieStatSeasonTeam
      public GoalieStatSeasonTeam FindGoalieStatSeasonTeam(int playerId, int seasonTeamIdPlayingFor)
      {
        return FindGoalieStatSeasonTeam(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, seasonTeamIdPlayingFor: seasonTeamIdPlayingFor);
      }

      public GoalieStatSeasonTeam FindGoalieStatSeasonTeam(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int seasonTeamIdPlayingFor)
      {
        var found = _ctx.GoalieStatsSeasonTeam.Where(x => x.PlayerId == playerId && x.SeasonTeamIdPlayingFor == seasonTeamIdPlayingFor).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GoalieStatSeasonTeam for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonTeamIdPlayingFor:" + seasonTeamIdPlayingFor
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GoalieStatSeasonTeam was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonTeamIdPlayingFor:" + seasonTeamIdPlayingFor
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region FindGoalieStatSeason
      public GoalieStatSeason FindGoalieStatSeason(int playerId, int seasonId, bool sub)
      {
        return FindGoalieStatSeason(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, seasonId: seasonId, sub: sub);
      }

      public GoalieStatSeason FindGoalieStatSeason(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int seasonId, bool sub)
      {
        var found = _ctx.GoalieStatsSeason.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId && x.Sub == sub).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GoalieStatSeason for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonId:" + seasonId +
                                                  " Sub:" + sub
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GoalieStatSeason was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonId:" + seasonId +
                                                  " Sub:" + sub
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #endregion

      public int ContextSaveChanges()
      {
        try
        {
          return _ctx.SaveChanges();
        }
        catch (DbEntityValidationException e)
        {
          foreach (var eve in e.EntityValidationErrors)
          {
            Debug.Print("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
            foreach (var ve in eve.ValidationErrors)
            {
              Debug.Print("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
            }
          }
          throw;
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