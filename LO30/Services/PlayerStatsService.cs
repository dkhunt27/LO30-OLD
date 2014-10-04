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
      public List<PlayerStatSeason> ProcessPlayerGameStatsIntoPlayerSeasonStats(List<PlayerStatGame> playerGameStats)
      {
        var playerSeasonStats = new List<PlayerStatSeason>();

        var playerGameStatsSummedForSeason = playerGameStats
                .GroupBy(x => new { x.SeasonId, x.PlayerId, x.PlayerStatTypeId, x.SeasonTeamIdPlayingFor })
                .Select(grp => new
                {
                  SeasonId = grp.Key.SeasonId,
                  PlayerId = grp.Key.PlayerId,
                  PlayerStatTypeId = grp.Key.PlayerStatTypeId,
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

        foreach (var stat in playerGameStatsSummedForSeason)
        {
          playerSeasonStats.Add(new PlayerStatSeason(
                                      sid: stat.SeasonId, 
                                      pid: stat.PlayerId, 
                                      pstid: stat.PlayerStatTypeId, 
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

        return playerSeasonStats;
      }

      public List<PlayerStatGame> ProcessScoreSheetEntriesIntoPlayerGameStats(List<Player> players, List<Game> games, List<GameRoster> gameRosters, List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed, List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed)
      {
        var playerGameStats = new List<PlayerStatGame>();

        try
        {
          #region process each player
          foreach (var player in players)
          {
            var playerId = player.PlayerId;

            // get list of games that this player played in
            var gameRostersPlayerPlayedIn = gameRosters.Where(x => x.PlayerId == playerId).ToList();

            // get list of distinct seasons that those games cover
            var seasonsPlayerPlayedIn = gameRostersPlayerPlayedIn.Select(x => x.SeasonTeam.SeasonId).Distinct();

            #region process all seasons for that player
            foreach (var seasonId in seasonsPlayerPlayedIn)
            {
              // get list of distinct games for this season
              var gamesPlayerPlayedIn = gameRostersPlayerPlayedIn
                                          .Where(x => x.SeasonTeam.SeasonId == seasonId)
                                          .Select(x => new { GameId = x.GameId, SeasonTeamId = x.SeasonTeamId, Sub = x.Sub })
                                          .Distinct();

              #region process all games for that season for that player
              foreach (var gamePlayerPlayedIn in gamesPlayerPlayedIn)
              {
                var gameId = gamePlayerPlayedIn.GameId;
                var seasonTeamId = gamePlayerPlayedIn.SeasonTeamId;
                var sub = gamePlayerPlayedIn.Sub;

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
                                                        .Where(x => x.GameId == gameId && x.PlayerId == player.PlayerId)
                                                        .ToList();

                foreach (var scoreSheetEntryPenalty in scoreSheetEntryPenalties)
                {
                  penaltyMinutes = penaltyMinutes + scoreSheetEntryPenalty.PenaltyMinutes;
                }
                #endregion

                // TODO update so not hardcoded
                int playerStatTypeId = 1; // default is rostered
                if (sub)
                {
                  playerStatTypeId = 2;
                }

                // now save player stat game
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
            }
            #endregion
          }
          #endregion

          return playerGameStats;
        }
        catch (Exception ex)
        {
          Debug.Print("Following error occurred:" + ex.StackTrace);
          throw;
        }
      }
    }
}