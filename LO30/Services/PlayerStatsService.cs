using LO30.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LO30.Services
{
  public class PlayerStatsService
  {
    public List<PlayerStatGame> ProcessScoreSheetEntriesIntoPlayerGameStats(List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed, List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed, List<GameRoster> gameRosters, List<PlayerStatType> playerStatTypes)
    {
      var playerGameStats = new List<PlayerStatGame>();

      try
      {
        #region process each gameRoster...using game rosters because not every one will be on the score sheets for points/pims
        foreach (var gameRoster in gameRosters)
        {
          #region determine key fields (gameId, seasonTeamId, playerId, playerStatTypeId, sub, etc)
          var gameId = gameRoster.GameId;
          var seasonTeamId = gameRoster.SeasonTeamId;
          var sub = gameRoster.Sub;

          int playerId;
          bool subLookup;
          if (gameRoster.SubbingForPlayerId == null)
          {
            playerId = gameRoster.PlayerId;
            subLookup = false;
          }
          else
          {
            // if there is a sub, use him
            playerId = Convert.ToInt32(gameRoster.SubbingForPlayerId);
            subLookup = true;
          }

          var playerStatType = playerStatTypes.Where(x => x.Sub == subLookup).FirstOrDefault();
          if (playerStatType == null)
          {
            throw new ArgumentNullException("playerStatType", "playerStatType was not found");
          }
          var playerStatTypeId = playerStatType.PlayerStatTypeId;

          if (gameRoster.SeasonTeam == null || gameRoster.SeasonTeam.SeasonId == null)
          {
            throw new ArgumentNullException("gameRoster.SeasonTeam.SeasonId");
          }
          var seasonId = gameRoster.SeasonTeam.SeasonId;
          #endregion

          // get score sheet entries for this player, for this game...if exists...if he had no points or pim, it won't exist
          int goals = 0;
          int assists = 0;
          int penaltyMinutes = 0;
          int powerPlayGoals = 0;
          int shortHandedGoals = 0;
          int gameWinningGoals = 0;

          #region process all score sheet entries for this specific game/player
          var scoreSheetEntries = scoreSheetEntriesProcessed
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
              goals++;

              if (scoreSheetEntry.ShortHandedGoal)
              {
                shortHandedGoals++;
              }
              else if (scoreSheetEntry.PowerPlayGoal)
              {
                powerPlayGoals++;
              }

              // can be (shorthanded or powerplay) and a game winner...so not else if here, just if
              if (scoreSheetEntry.GameWinningGoal)
              {
                gameWinningGoals++;
              }

            }
            else
            {
              // the score sheet entry must match this player on a goal or assist
              assists++;
            }

          }
          #endregion

          #region process all score sheet entry penalties for this specific game/player
          var scoreSheetEntryPenalties = scoreSheetEntryPenaltiesProcessed
                                                  .Where(x => x.GameId == gameId && x.PlayerId == playerId)
                                                  .ToList();

          foreach (var scoreSheetEntryPenalty in scoreSheetEntryPenalties)
          {
            penaltyMinutes = penaltyMinutes + scoreSheetEntryPenalty.PenaltyMinutes;
          }
          #endregion

          playerGameStats.Add(new PlayerStatGame(
                                        sid: seasonId,
                                        pid: playerId,
                                        pstid: playerStatTypeId,
                                        stidpf: seasonTeamId,
                                        gid: gameId,
                                        g: goals,
                                        a: assists,
                                        p: goals + assists,
                                        ppg: powerPlayGoals,
                                        shg: shortHandedGoals,
                                        gwg: gameWinningGoals,
                                        pim: penaltyMinutes
                                        )
                                );
        }
        #endregion

        return playerGameStats;
      }
      catch (Exception ex)
      {
        ErrorHandlingService.PrintFullErrorMessage(ex);
        throw ex;
      }
    }

    public List<PlayerStatSeasonTeam> ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(List<PlayerStatGame> playerGameStats)
    {
      var playerSeasonTeamStats = new List<PlayerStatSeasonTeam>();

      var summedStats = playerGameStats
              .GroupBy(x => new { x.PlayerId, x.PlayerStatTypeId, x.SeasonId, x.SeasonTeamIdPlayingFor })
              .Select(grp => new
              {
                PlayerId = grp.Key.PlayerId,
                PlayerStatTypeId = grp.Key.PlayerStatTypeId,
                SeasonId = grp.Key.SeasonId,
                SeasonTeamIdPlayingFor = grp.Key.SeasonTeamIdPlayingFor,

                Games = grp.Count(),
                Goals = grp.Sum(x => x.Goals),
                Assists = grp.Sum(x => x.Assists),
                Points = grp.Sum(x => x.Points),
                PenaltyMinutes = grp.Sum(x => x.PenaltyMinutes),
                ShortHandedGoals = grp.Sum(x => x.ShortHandedGoals),
                PowerPlayGoals = grp.Sum(x => x.PowerPlayGoals),
                GameWinningGoals = grp.Sum(x => x.GameWinningGoals)
              })
              .ToList();

      foreach (var stat in summedStats)
      {
        playerSeasonTeamStats.Add(new PlayerStatSeasonTeam(
                                    pid: stat.PlayerId,
                                    pstid: stat.PlayerStatTypeId,
                                    sid: stat.SeasonId,
                                    stidpf: stat.SeasonTeamIdPlayingFor,

                                    games: stat.Games,
                                    g: stat.Goals,
                                    a: stat.Assists,
                                    p: stat.Points,
                                    ppg: stat.PowerPlayGoals,
                                    shg: stat.ShortHandedGoals,
                                    gwg: stat.GameWinningGoals,
                                    pim: stat.PenaltyMinutes
                                    )
                              );
      }

      return playerSeasonTeamStats;
    }

    public List<PlayerStatSeason> ProcessPlayerSeasonTeamStatsIntoPlayerSeasonStats(List<PlayerStatSeasonTeam> playerSeasonTeamStats)
    {
      var playerSeasonStats = new List<PlayerStatSeason>();

      var summedStats = playerSeasonTeamStats
              .GroupBy(x => new { x.PlayerId, x.PlayerStatTypeId, x.SeasonId })
              .Select(grp => new
              {
                PlayerId = grp.Key.PlayerId,
                PlayerStatTypeId = grp.Key.PlayerStatTypeId,
                SeasonId = grp.Key.SeasonId,

                Games = grp.Count(),
                Goals = grp.Sum(x => x.Goals),
                Assists = grp.Sum(x => x.Assists),
                Points = grp.Sum(x => x.Points),
                PenaltyMinutes = grp.Sum(x => x.PenaltyMinutes),
                ShortHandedGoals = grp.Sum(x => x.ShortHandedGoals),
                PowerPlayGoals = grp.Sum(x => x.PowerPlayGoals),
                GameWinningGoals = grp.Sum(x => x.GameWinningGoals)
              })
              .ToList();

      foreach (var stat in summedStats)
      {
        playerSeasonStats.Add(new PlayerStatSeason(
                                    pid: stat.PlayerId,
                                    pstid: stat.PlayerStatTypeId,
                                    sid: stat.SeasonId,

                                    games: stat.Games,
                                    g: stat.Goals,
                                    a: stat.Assists,
                                    p: stat.Points,
                                    ppg: stat.PowerPlayGoals,
                                    shg: stat.ShortHandedGoals,
                                    gwg: stat.GameWinningGoals,
                                    pim: stat.PenaltyMinutes
                                    )
                              );
      }

      return playerSeasonStats;
    }

    public List<GoalieStatGame> ProcessScoreSheetEntriesIntoGoalieGameStats(List<GameResult> gameResults, List<GameRoster> gameRostersGoalies, List<PlayerStatType> playerStatTypes)
    {
      var goalieGameStats = new List<GoalieStatGame>();

      try
      {
        #region process each gameRoster...using game rosters because not every one will be on the score sheets for points/pims
        foreach (var gameRoster in gameRostersGoalies)
        {
          #region determine key fields (gameId, seasonTeamId, playerId, playerStatTypeId, sub, etc)
          var gameId = gameRoster.GameId;
          var seasonTeamId = gameRoster.SeasonTeamId;
          var sub = gameRoster.Sub;

          int playerId;
          bool subLookup;
          if (gameRoster.SubbingForPlayerId == null)
          {
            playerId = gameRoster.PlayerId;
            subLookup = false;
          }
          else
          {
            // if there is a sub, use him
            playerId = Convert.ToInt32(gameRoster.SubbingForPlayerId);
            subLookup = true;
          }

          var playerStatType = playerStatTypes.Where(x => x.Sub == subLookup).FirstOrDefault();
          if (playerStatType == null)
          {
            throw new ArgumentNullException("playerStatType", "playerStatType was not found");
          }
          var playerStatTypeId = playerStatType.PlayerStatTypeId;

          if (gameRoster.SeasonTeam == null || gameRoster.SeasonTeam.SeasonId == null)
          {
            throw new ArgumentNullException("gameRoster.SeasonTeam.SeasonId");
          }
          var seasonId = gameRoster.SeasonTeam.SeasonId;
          #endregion

          // sanity check...make sure there is only 1 goalie for each team for each game
          var check = gameRostersGoalies.Where(x => x.GameId == gameId && x.SeasonTeamId == seasonTeamId && x.Goalie == true).ToList();

          if (check.Count < 1 || check.Count > 1)
          {
            throw new ArgumentNullException("gameRosterGoalies", "Every GameRoster must have 1 and only 1 goalie GameId:" + gameId + " SeasonTeamId:" + seasonTeamId + " Goalie Count:" + check.Count);
          }

          var gameResult = gameResults.Where(x => x.GameId == gameId && x.SeasonTeamId == seasonTeamId).FirstOrDefault();

          if (gameResult == null)
          {
            throw new ArgumentNullException("gameResult", "gameResult not found for gameId:" + gameId + " seasonTeamId:" + seasonTeamId);
          }

          goalieGameStats.Add(new GoalieStatGame(
                                        sid: seasonId,
                                        pid: playerId,
                                        pstid: playerStatTypeId,
                                        stidpf: seasonTeamId,
                                        gid: gameId,
                                        ga: gameResult.GoalsAgainst,
                                        so: gameResult.GoalsAgainst == 0 ? 1 : 0,
                                        w: gameResult.Result == "W" ? 1 : 0
                                        )
                                );
        }
        #endregion

        return goalieGameStats;
      }
      catch (Exception ex)
      {
        ErrorHandlingService.PrintFullErrorMessage(ex);
        throw ex;
      }
    }
  }
}