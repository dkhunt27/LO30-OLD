﻿using LO30.Data.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Services
{
  public class Lo30DataService
  {
    public int TimePerPeriod = 14;

    public List<ForWebTeamStanding> DeriveWebTeamStandings(List<TeamStanding> teamStandings)
    {
      var newData = new List<ForWebTeamStanding>();

      foreach (var item in teamStandings)
      {
        float winPct = 0;
        if (item.Games > 0)
        {
          winPct = (float)item.Wins / (float)item.Games;
        }
        var stat = new ForWebTeamStanding()
        {
          STID = item.SeasonTeamId,
          PFS = item.Playoffs,
          SID = item.SeasonTeam.SeasonId,
          Div = item.Division.DivisionLongName,
          Team = item.SeasonTeam.Team.TeamLongName,
          Rank = item.Rank,
          GP = item.Games,
          W = item.Wins,
          L = item.Losses,
          T = item.Ties,
          PTS = item.Points,
          GF = item.GoalsFor,
          GA = item.GoalsAgainst,
          PIM = item.PenaltyMinutes,
          S = item.Subs,
          WPCT = winPct
        };

        if (stat.STID == 0)
        {
          Debug.Print(string.Format("DeriveWebTeamStandings: Warning ForWebTeamStanding has stid=0 Team:{0}", stat.Team));
        }
        newData.Add(stat);
      }

      return newData;
    }

    public List<PlayerStatGame> DerivePlayerGameStats(List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed, List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed, List<GameRoster> gameRosters)
    {
      var playerGameStats = new List<PlayerStatGame>();

      try
      {
        #region process each gameRoster...using game rosters because not every one will be on the score sheets for points/pims
        foreach (var gameRoster in gameRosters)
        {
          #region determine key fields (gameId, seasonTeamId, playerId, playerStatTypeId, sub, etc)
          var gameId = gameRoster.GameTeam.GameId;
          var seasonTeamId = gameRoster.GameTeam.SeasonTeamId;
          var playoffs = gameRoster.GameTeam.Game.Playoffs;
          var sub = gameRoster.Sub;
          var playerId = gameRoster.PlayerId;
          var line = gameRoster.Line;
          var position = gameRoster.Position;

          if (gameRoster.GameTeam.SeasonTeam == null || gameRoster.GameTeam.SeasonTeam.SeasonId == null)
          {
            throw new ArgumentNullException("gameRoster.GameTeam.SeasonTeam.SeasonId");
          }
          var seasonId = gameRoster.GameTeam.SeasonTeam.SeasonId;
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
                                        pid: playerId,
                                        gid: gameId,

                                        sid: seasonId,
                                        stid: seasonTeamId,
                                        pfs: playoffs,
                                        line: line,
                                        pos: position,
                                        sub: sub,

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

    public List<PlayerStatSeasonTeam> DerivePlayerSeasonTeamStats(List<PlayerStatGame> playerGameStats)
    {
      var playerSeasonTeamStats = new List<PlayerStatSeasonTeam>();

      var summedStats = playerGameStats
              .GroupBy(x => new { x.PlayerId, x.SeasonId, x.Playoffs, x.Sub, SeasonTeamIdPlayingFor = x.SeasonTeamId, x.Line, x.Position })
              .Select(grp => new
              {
                PlayerId = grp.Key.PlayerId,
                SeasonId = grp.Key.SeasonId,
                Playoffs = grp.Key.Playoffs,
                Sub = grp.Key.Sub,
                SeasonTeamIdPlayingFor = grp.Key.SeasonTeamIdPlayingFor,
                Line = grp.Key.Line,
                Position = grp.Key.Position,

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
                                    sid: stat.SeasonId,
                                    pfs: stat.Playoffs,
                                    sub: stat.Sub,
                                    stid: stat.SeasonTeamIdPlayingFor,
                                    line: stat.Line,
                                    pos: stat.Position,


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

    public List<PlayerStatSeason> DerivePlayerSeasonStats(List<PlayerStatSeasonTeam> playerSeasonTeamStats)
    {
      var playerSeasonStats = new List<PlayerStatSeason>();

      var summedStats = playerSeasonTeamStats
              .GroupBy(x => new { x.PlayerId, x.SeasonId, x.Playoffs, x.Sub })
              .Select(grp => new
              {
                PlayerId = grp.Key.PlayerId,
                SeasonId = grp.Key.SeasonId,
                Playoffs = grp.Key.Playoffs,
                Sub = grp.Key.Sub,

                Games = grp.Sum(x => x.Games),
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
                                    sid: stat.SeasonId,
                                    pfs: stat.Playoffs,
                                    sub: stat.Sub,

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

    public List<ForWebPlayerStat> DeriveWebPlayerStats(List<PlayerStatSeasonTeam> playerSeasonTeamStats, List<PlayerRating> playerRatings)
    {
      var newData = new List<ForWebPlayerStat>();

      foreach (var item in playerSeasonTeamStats)
      {
        var playerName = item.Player.FirstName + " " + item.Player.LastName;
        if (!string.IsNullOrWhiteSpace(item.Player.Suffix))
        {
          playerName = playerName + " " + item.Player.Suffix;
        }

        var stat = new ForWebPlayerStat()
        {
          PID = item.PlayerId,
          STID = item.SeasonTeamId,
          PFS = item.Playoffs,
          SID = item.SeasonId,
          Player = playerName,
          Team = item.SeasonTeam.Team.TeamLongName,
          Sub = item.Sub == true ? "Y" : "N",
          Pos = item.Position,
          Line = item.Line,
          GP = item.Games,
          G = item.Goals,
          A = item.Assists,
          P = item.Points,
          PPG = item.PowerPlayGoals,
          SHG = item.ShortHandedGoals,
          GWG = item.GameWinningGoals,
          PIM = item.PenaltyMinutes
        };

        if (stat.PID == 0 && stat.SID == 0 && stat.STID == 0)
        {
          Debug.Print(string.Format("DeriveWebPlayerStats: Warning ForWebPlayerStat has ids of 0,0,0 Player:{0}, Team:{1}, Sub:{2}", stat.Player, stat.Team, stat.Sub));
        }
        newData.Add(stat);
      }

      return newData;
    }

    public List<GoalieStatGame> DeriveGoalieGameStats(List<GameOutcome> gameOutcomes, List<GameRoster> gameRostersGoalies)
    {
      var goalieGameStats = new List<GoalieStatGame>();

      try
      {
        #region process each gameRoster...using game rosters because not every one will be on the score sheets for points/pims
        foreach (var gameRoster in gameRostersGoalies)
        {
          #region determine key fields (gameId, seasonTeamId, playerId, playerStatTypeId, sub, etc)
          var gameId = gameRoster.GameTeam.GameId;
          var seasonTeamId = gameRoster.GameTeam.SeasonTeamId;
          var playoffs = gameRoster.GameTeam.Game.Playoffs;
          var sub = gameRoster.Sub;
          var playerId = gameRoster.PlayerId;

          if (gameRoster.GameTeam.SeasonTeam == null || gameRoster.GameTeam.SeasonTeam.SeasonId == null)
          {
            throw new ArgumentNullException("gameRoster.SeasonTeam.SeasonId");
          }
          var seasonId = gameRoster.GameTeam.SeasonTeam.SeasonId;
          #endregion

          // sanity check...make sure there is only 1 goalie for each team for each game
          var check = gameRostersGoalies.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.SeasonTeamId == seasonTeamId && x.Goalie == true).ToList();

          // remove if goal "subbed" for himself because he forgot his jersey

          if (check.Count < 1 || check.Count > 1)
          {
            throw new ArgumentNullException("gameRosterGoalies", "Every GameRoster must have 1 and only 1 goalie GameId:" + gameId + " SeasonTeamId:" + seasonTeamId + " Goalie Count:" + check.Count);
          }

          // get score sheet entries for this player, for this game...if exists...if he had no points or pim, it won't exist
          int goalAgainst = 0;
          int shutOuts = 0;
          int wins = 0;

          var gameOutcome = gameOutcomes.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.SeasonTeamId == seasonTeamId).FirstOrDefault();

          if (gameOutcome != null)
          {
            //throw new ArgumentNullException("gameOutcome", "gameOutcome not found for gameId:" + gameId + " seasonTeamId:" + seasonTeamId);

            goalAgainst = gameOutcome.GoalsAgainst;
            shutOuts = gameOutcome.GoalsAgainst == 0 ? 1 : 0;
            wins = gameOutcome.Outcome == "W" ? 1 : 0;
          }

          goalieGameStats.Add(new GoalieStatGame(
                                        pid: playerId,
                                        gid: gameId,

                                        sid: seasonId,
                                        stid: seasonTeamId,
                                        pfs: playoffs,
                                        sub: sub,

                                        ga: goalAgainst,
                                        so: shutOuts,
                                        w: wins
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

    public List<GoalieStatSeasonTeam> DeriveGoalieSeasonTeamStats(List<GoalieStatGame> goalieGameStats)
    {
      var goalieSeasonTeamStats = new List<GoalieStatSeasonTeam>();

      var summedStats = goalieGameStats
              .GroupBy(x => new { x.PlayerId, x.SeasonId, x.Playoffs, x.Sub, SeasonTeamIdPlayingFor = x.SeasonTeamId })
              .Select(grp => new
              {
                PlayerId = grp.Key.PlayerId,
                SeasonId = grp.Key.SeasonId,
                Playoffs = grp.Key.Playoffs,
                Sub = grp.Key.Sub,
                SeasonTeamIdPlayingFor = grp.Key.SeasonTeamIdPlayingFor,

                Games = grp.Count(),
                GoalsAgainst = grp.Sum(x => x.GoalsAgainst),
                Shutouts = grp.Sum(x => x.Shutouts),
                Wins = grp.Sum(x => x.Wins)
              })
              .ToList();

      foreach (var stat in summedStats)
      {
        goalieSeasonTeamStats.Add(new GoalieStatSeasonTeam(
                                    pid: stat.PlayerId,
                                    sid: stat.SeasonId,
                                    pfs: stat.Playoffs,
                                    sub: stat.Sub,
                                    stid: stat.SeasonTeamIdPlayingFor,

                                    games: stat.Games,
                                    ga: stat.GoalsAgainst,
                                    so: stat.Shutouts,
                                    w: stat.Wins
                                    )
                              );
      }

      return goalieSeasonTeamStats;
    }

    public List<GoalieStatSeason> DeriveGoalieSeasonStats(List<GoalieStatSeasonTeam> goalieSeasonTeamStats)
    {
      var goalieSeasonStats = new List<GoalieStatSeason>();

      var summedStats = goalieSeasonTeamStats
              .GroupBy(x => new { x.PlayerId, x.SeasonId, x.Playoffs, x.Sub })
              .Select(grp => new
              {
                PlayerId = grp.Key.PlayerId,
                SeasonId = grp.Key.SeasonId,
                Playoffs = grp.Key.Playoffs,
                Sub = grp.Key.Sub,

                Games = grp.Sum(x => x.Games),
                GoalsAgainst = grp.Sum(x => x.GoalsAgainst),
                Shutouts = grp.Sum(x => x.Shutouts),
                Wins = grp.Sum(x => x.Wins)
              })
              .ToList();

      foreach (var stat in summedStats)
      {
        goalieSeasonStats.Add(new GoalieStatSeason(
                                    pid: stat.PlayerId,
                                    sid: stat.SeasonId,
                                    pfs: stat.Playoffs,
                                    sub: stat.Sub,

                                    games: stat.Games,
                                    ga: stat.GoalsAgainst,
                                    so: stat.Shutouts,
                                    w: stat.Wins
                                    )
                              );
      }

      return goalieSeasonStats;
    }

    public List<ForWebGoalieStat> DeriveWebGoalieStats(List<GoalieStatSeasonTeam> goalieSeasonTeamStats)
    {
      var newData = new List<ForWebGoalieStat>();

      foreach (var item in goalieSeasonTeamStats)
      {
        var playerName = item.Player.FirstName + " " + item.Player.LastName;
        if (!string.IsNullOrWhiteSpace(item.Player.Suffix))
        {
          playerName = playerName + " " + item.Player.Suffix;
        }

        newData.Add(new ForWebGoalieStat()
        {
          PID = item.PlayerId,
          STID = item.SeasonTeamId,
          PFS = item.Playoffs,
          SID = item.SeasonId,
          Player = playerName,
          Team = item.SeasonTeam.Team.TeamLongName,
          Sub = item.Sub == true ? "Y" : "N",
          GP = item.Games,
          GA = item.GoalsAgainst,
          GAA = item.GoalsAgainstAverage,
          SO = item.Shutouts,
          W = item.Wins
        });
      }

      return newData;
    }

    public List<ScoreSheetEntryProcessed> DeriveScoreSheetEntriesProcessed(List<ScoreSheetEntry> scoreSheetEntries, List<GameTeam> gameTeams, List<GameRoster> gameRosters)
    {
      var newData = new List<ScoreSheetEntryProcessed>();

      foreach (var scoreSheetEntry in scoreSheetEntries)
      {
        var gameId = scoreSheetEntry.GameId;

        // look up the home and away season team id
        // TODO..do this once per game, not per score sheet entry
        var homeGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == true).FirstOrDefault();
        var awayGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == false).FirstOrDefault();

        var homeSeasonTeamId = homeGameTeam.SeasonTeamId;
        var awaySeasonTeamId = awayGameTeam.SeasonTeamId;

        // lookup game rosters
        // TODO..do this once per game, not per score sheet entry
        var homeTeamRoster = gameRosters.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.SeasonTeamId == homeSeasonTeamId).ToList();
        var awayTeamRoster = gameRosters.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.SeasonTeamId == awaySeasonTeamId).ToList();

        var homeTeamFlag = scoreSheetEntry.HomeTeam;
        var goalPlayerNumber = scoreSheetEntry.Goal;
        var assist1PlayerNumber = scoreSheetEntry.Assist1;
        var assist2PlayerNumber = scoreSheetEntry.Assist2;
        var assist3PlayerNumber = scoreSheetEntry.Assist3;

        var gameRosterToUse = awayTeamRoster;
        var gameTeamToUse = awayGameTeam;
        if (homeTeamFlag)
        {
          gameRosterToUse = homeTeamRoster;
          gameTeamToUse = homeGameTeam;
        }

        // lookup player ids
        var goalPlayerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, goalPlayerNumber);
        var assist1PlayerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, assist1PlayerNumber);
        var assist2PlayerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, assist2PlayerNumber);
        var assist3PlayerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, assist3PlayerNumber);

        // determine type goal
        // TODO improve this logic
        bool shortHandedGoal = scoreSheetEntry.ShortHandedPowerPlay == "SH" ? true : false;
        bool powerPlayGoal = scoreSheetEntry.ShortHandedPowerPlay == "PP" ? true : false;
        bool gameWinningGoal = false;

        newData.Add(new ScoreSheetEntryProcessed(
                          sseid: scoreSheetEntry.ScoreSheetEntryId,

                          gid: scoreSheetEntry.GameId,
                          per: scoreSheetEntry.Period,
                          ht: scoreSheetEntry.HomeTeam,
                          time: scoreSheetEntry.TimeRemaining,
                          gtid: gameTeamToUse.GameTeamId,

                          gpid: Convert.ToInt32(goalPlayerId),
                          a1pid: assist1PlayerId,
                          a2pid: assist2PlayerId,
                          a3pid: assist3PlayerId,

                          shg: shortHandedGoal,
                          ppg: powerPlayGoal,
                          gwg: gameWinningGoal,

                          upd: DateTime.Now
                  ));
      }

      return newData;
    }

    public List<ScoreSheetEntryPenaltyProcessed> DeriveScoreSheetEntryPenaltiesProcessed(List<ScoreSheetEntryPenalty> scoreSheetEntryPenalties, List<GameTeam> gameTeams, List<GameRoster> gameRosters, List<Penalty> penalties)
    {
      var newData = new List<ScoreSheetEntryPenaltyProcessed>();

      foreach (var scoreSheetEntryPenalty in scoreSheetEntryPenalties)
      {
        var gameId = scoreSheetEntryPenalty.GameId;

        // look up the home and away season team id
        // TODO..do this once per game, not per score sheet entry
        var homeGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == true).FirstOrDefault();
        var awayGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == false).FirstOrDefault();
        var homeSeasonTeamId = homeGameTeam.SeasonTeamId;
        var awaySeasonTeamId = awayGameTeam.SeasonTeamId;

        // lookup game rosters
        var homeTeamRoster = gameRosters.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.SeasonTeamId == homeSeasonTeamId).ToList();
        var awayTeamRoster = gameRosters.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.SeasonTeamId == awaySeasonTeamId).ToList();

        var homeTeamFlag = scoreSheetEntryPenalty.HomeTeam;
        var playerNumber = scoreSheetEntryPenalty.Player;

        var gameRosterToUse = awayTeamRoster;
        var gameTeamToUse = awayGameTeam;
        if (homeTeamFlag)
        {
          gameRosterToUse = homeTeamRoster;
          gameTeamToUse = homeGameTeam;
        }

        // lookup player id
        var playerId = ConvertPlayerNumberIntoPlayer(gameRosterToUse, playerNumber);

        // lookup penalty
        var penaltyId = penalties.Where(x => x.PenaltyCode == scoreSheetEntryPenalty.PenaltyCode).FirstOrDefault().PenaltyId;

        newData.Add(new ScoreSheetEntryPenaltyProcessed(
                          ssepid: scoreSheetEntryPenalty.ScoreSheetEntryPenaltyId,

                          gid: scoreSheetEntryPenalty.GameId,
                          per: scoreSheetEntryPenalty.Period,
                          ht: scoreSheetEntryPenalty.HomeTeam,
                          time: scoreSheetEntryPenalty.TimeRemaining,
                          gtid: gameTeamToUse.GameTeamId,

                          playid: Convert.ToInt32(playerId),
                          penid: penaltyId,
                          pim: scoreSheetEntryPenalty.PenaltyMinutes,

                          upd: DateTime.Now
                  ));
      }

      return newData;
    }

    public List<ScoreSheetEntrySubProcessed> DeriveScoreSheetEntrySubsProcessed(List<ScoreSheetEntrySub> scoreSheetEntrySubs, List<Game> games, List<GameTeam> gameTeams, List<TeamRoster> teamRosters, List<Player> players)
    {
      var newData = new List<ScoreSheetEntrySubProcessed>();

      foreach (var scoreSheetEntrySub in scoreSheetEntrySubs)
      {
        var gameId = scoreSheetEntrySub.GameId;

        var game = games.Where(x=>x.GameId == gameId).FirstOrDefault();
        var gameDateYYYYMMDD = ConvertDateTimeIntoYYYYMMDD(game.GameDateTime, ifNullReturnMax: false);

        // look up the home and away season team id
        // TODO..do this once per game, not per score sheet entry
        var homeGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == true).FirstOrDefault();
        var awayGameTeam = gameTeams.Where(gt => gt.GameId == gameId && gt.HomeTeam == false).FirstOrDefault();
        var homeSeasonTeamId = homeGameTeam.SeasonTeamId;
        var awaySeasonTeamId = awayGameTeam.SeasonTeamId;

        // lookup team rosters
        var homeTeamRoster = teamRosters.Where(x => x.SeasonTeamId == homeSeasonTeamId && x.StartYYYYMMDD <= gameDateYYYYMMDD && x.EndYYYYMMDD >= gameDateYYYYMMDD).ToList();
        var awayTeamRoster = teamRosters.Where(x => x.SeasonTeamId == awaySeasonTeamId && x.StartYYYYMMDD <= gameDateYYYYMMDD && x.EndYYYYMMDD >= gameDateYYYYMMDD).ToList();


        var homeTeamFlag = scoreSheetEntrySub.HomeTeam;
        var jerseyNumber = scoreSheetEntrySub.JerseyNumber;
        var subPlayerId = scoreSheetEntrySub.SubPlayerId;
        var subbingForPlayerId = scoreSheetEntrySub.SubbingForPlayerId;

        var teamRosterToUse = awayTeamRoster;
        var gameTeamToUse = awayGameTeam;
        if (homeTeamFlag)
        {
          teamRosterToUse = homeTeamRoster;
          gameTeamToUse = homeGameTeam;
        }

        // lookup player ids

        // make sure the subbing for is on the team roster
        var foundSubbingForPlayer = teamRosterToUse.Where(x => x.PlayerId == subbingForPlayerId).FirstOrDefault();
        var foundSubPlayer = players.Where(x => x.PlayerId == subPlayerId).FirstOrDefault();

        if (foundSubbingForPlayer == null || foundSubPlayer == null)
        {
          // todo handle bad data
        }
        else
        {
          newData.Add(new ScoreSheetEntrySubProcessed(
                  ssesid: scoreSheetEntrySub.ScoreSheetEntrySubId,

                  gid: gameId,
                  ht: homeTeamFlag,
                  gtid: homeGameTeam.GameTeamId,

                  spid: subPlayerId,
                  sfpid: subbingForPlayerId,
                  jer: jerseyNumber,

                  upd: DateTime.Now
          ));
        }
      }

      return newData;
    }

    public List<ScoreSheetEntryEvent> DeriveScoreSheetEntryEvents(List<ScoreSheetEntryProcessed> scoreSheetEntries, List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties)
    {
      // put all the goals and penalties in order
      List<ScoreSheetEntryEvent> sseEvents = new List<ScoreSheetEntryEvent>();

      // add all the goals to the events
      foreach (var sseGoal in scoreSheetEntries)
      {
        sseEvents.Add(new ScoreSheetEntryEvent()
          {
            GameId = sseGoal.GameId,
            TimeElapsed = sseGoal.TimeElapsed,
            EventType  = ScoreSheetEntryEventType.Goal,
            ScoreSheetEntryId = sseGoal.ScoreSheetEntryId,
            ScoreSheetEntryPenaltyId = null,
            HomeTeam = sseGoal.HomeTeam,
            MatchPenalty = false
          }
        );
      }

      // add all the penalties to the events
      foreach (var ssePenalty in scoreSheetEntryPenalties)
      {
        sseEvents.Add(new ScoreSheetEntryEvent()
          {
            GameId = ssePenalty.GameId,
            TimeElapsed = ssePenalty.TimeElapsed,
            EventType = ScoreSheetEntryEventType.PenaltyStart,
            ScoreSheetEntryId = null,
            ScoreSheetEntryPenaltyId = ssePenalty.ScoreSheetEntryPenaltyId,
            HomeTeam = ssePenalty.HomeTeam,
            MatchPenalty = ssePenalty.PenaltyMinutes == 5 ? true : false
          }
        );

        sseEvents.Add(new ScoreSheetEntryEvent()
          {
            GameId = ssePenalty.GameId,
            TimeElapsed = ssePenalty.TimeElapsed.Add(new TimeSpan(0, ssePenalty.PenaltyMinutes, 0)),
            EventType = ScoreSheetEntryEventType.PenaltyEnd,
            ScoreSheetEntryId = null,
            ScoreSheetEntryPenaltyId = ssePenalty.ScoreSheetEntryPenaltyId,
            HomeTeam = ssePenalty.HomeTeam,
            MatchPenalty = ssePenalty.PenaltyMinutes == 5 ? true : false
          }
        );
      }

      // VERY IMPORTANT, order the events by time elapsed then event type
      sseEvents = sseEvents.OrderBy(x => x.TimeElapsed).ThenBy(x=>x.EventType).ToList();

      return sseEvents;
    }

    public List<ScoreSheetEntryGoalType> ProcessOneGameScoreSheetEntryEvents(List<ScoreSheetEntryEvent> scoreSheetEntryEvents)
    {
      List<ScoreSheetEntryGoalType> results = new List<ScoreSheetEntryGoalType>();

      List<ScoreSheetEntryEvent> homeTeamPenaltyBox = new List<ScoreSheetEntryEvent>();
      List<ScoreSheetEntryEvent> awayTeamPenaltyBox = new List<ScoreSheetEntryEvent>();
      ScoreSheetEntryEvent leavesPenaltyBox;
      ScoreSheetEntryEvent eventToRemove;

      // loop through the events and see if any goals where scored during the penalties
      while (scoreSheetEntryEvents.Count > 0)
      {
        var sseEvent = scoreSheetEntryEvents[0];

        var penaltyBoxToUse = awayTeamPenaltyBox;
        if (sseEvent.HomeTeam)
        {
          penaltyBoxToUse = homeTeamPenaltyBox;
        }

        switch (sseEvent.EventType)
        {
          case ScoreSheetEntryEventType.PenaltyEnd:
            // the penalty ended before any goal was scored...
            // remove person from penalty box

            leavesPenaltyBox = penaltyBoxToUse.Single(x => x.ScoreSheetEntryPenaltyId == sseEvent.ScoreSheetEntryPenaltyId);
            penaltyBoxToUse.Remove(leavesPenaltyBox);

            // remove event since it has been handled
            eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryPenaltyId == sseEvent.ScoreSheetEntryPenaltyId && x.EventType == sseEvent.EventType);
            scoreSheetEntryEvents.Remove(eventToRemove);
            break;
          case ScoreSheetEntryEventType.PenaltyStart:
            // a penalty occurred, add person to penalty box
             
            penaltyBoxToUse.Add(sseEvent);

            // remove event since it has been handled
            eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryPenaltyId == sseEvent.ScoreSheetEntryPenaltyId && x.EventType == sseEvent.EventType);
            scoreSheetEntryEvents.Remove(eventToRemove);
            break;
          case ScoreSheetEntryEventType.Goal:
            // if no one in the penalty box, then its just a regular goal
            if (homeTeamPenaltyBox.Count == 0 && awayTeamPenaltyBox.Count == 0)
            {
              // if no one in the penalty box, then its just a regular goal
              // do nothing
            }
            else if (homeTeamPenaltyBox.Count == awayTeamPenaltyBox.Count)
            {
              // there are the same number of people in the penalty box
              // so the goal was even strength
              // do nothing
            }
            else if (homeTeamPenaltyBox.Count < awayTeamPenaltyBox.Count)
            {
              // the home team is on the power play
              if (sseEvent.HomeTeam)
              {
                // home team scored...on the power play
                results.Add(new ScoreSheetEntryGoalType(){
                  ScoreSheetEntryId = Convert.ToInt32(sseEvent.ScoreSheetEntryId),
                  PowerPlayGoal = true,
                  ShortHandedGoal = false,
                  GameWinningGoal = false
                });

                //release person from away team penalty box (but not if match penalty)
                leavesPenaltyBox = awayTeamPenaltyBox.Where(x => x.MatchPenalty == false).OrderBy(x => x.TimeElapsed).FirstOrDefault();
                if (leavesPenaltyBox != null)
                {
                  awayTeamPenaltyBox.Remove(leavesPenaltyBox);

                  // also remove the penalty end event
                  eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryPenaltyId == leavesPenaltyBox.ScoreSheetEntryPenaltyId && x.EventType == ScoreSheetEntryEventType.PenaltyEnd);
                  scoreSheetEntryEvents.Remove(eventToRemove);
                }
              }
              else
              {
                // away team scored...shorthanded
                results.Add(new ScoreSheetEntryGoalType(){
                  ScoreSheetEntryId = Convert.ToInt32(sseEvent.ScoreSheetEntryId),
                  PowerPlayGoal = false,
                  ShortHandedGoal = true,
                  GameWinningGoal = false
                });
              }
            }
            else
            {
              // the away team is on the power play
              if (sseEvent.HomeTeam)
              {
                // home team scored...shorthanded
                results.Add(new ScoreSheetEntryGoalType(){
                  ScoreSheetEntryId = Convert.ToInt32(sseEvent.ScoreSheetEntryId),
                  PowerPlayGoal = false,
                  ShortHandedGoal = true,
                  GameWinningGoal = false
                });
              }
              else
              {
                // away team scored...on the power play
                results.Add(new ScoreSheetEntryGoalType(){
                  ScoreSheetEntryId = Convert.ToInt32(sseEvent.ScoreSheetEntryId),
                  PowerPlayGoal = true,
                  ShortHandedGoal = false,
                  GameWinningGoal = false
                });

                //release person from home team penalty box (but not if match penalty)
                leavesPenaltyBox = homeTeamPenaltyBox.Where(x => x.MatchPenalty == false).OrderBy(x => x.TimeElapsed).FirstOrDefault();
                if (leavesPenaltyBox != null)
                {
                  homeTeamPenaltyBox.Remove(leavesPenaltyBox);

                  // also remove the penalty end event
                  eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryPenaltyId == leavesPenaltyBox.ScoreSheetEntryPenaltyId && x.EventType == ScoreSheetEntryEventType.PenaltyEnd);
                  scoreSheetEntryEvents.Remove(eventToRemove);
                }
              }
            }

            // remove event since it has been handled
            eventToRemove = scoreSheetEntryEvents.Single(x => x.ScoreSheetEntryId == sseEvent.ScoreSheetEntryId && x.EventType == sseEvent.EventType);
            scoreSheetEntryEvents.Remove(eventToRemove);
            break;
          default:
            throw new NotImplementedException("The EventType (" + sseEvent.EventType.ToString() + ") is not implemented");
        }
      }

      return results;
    }

    public List<ScoreSheetEntryProcessed> UpdateScoreSheetEntryProcessedWithPPAndSH(List<ScoreSheetEntryProcessed> scoreSheetEntries, List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties)
    {
      List<ScoreSheetEntryGoalType> scoreSheetEntryGoalTypes = new List<ScoreSheetEntryGoalType>();

      // put all the goals and penalties in order
      List<ScoreSheetEntryEvent> scoreSheetEntryEvents = DeriveScoreSheetEntryEvents(scoreSheetEntries, scoreSheetEntryPenalties);

      int currentGameId = -1;
      int previosGameId = -1;
      List<int> penaltiesProcessing = new List<int>();

      // loop through the events and see if any goals where scored during the penalties
      foreach (var sseEvent in scoreSheetEntryEvents)
      {
        currentGameId = sseEvent.GameId;

        if (currentGameId == previosGameId)
        {
          // this gameid has already been processed; do nothing
        }
        else
        {
          previosGameId = currentGameId;

          var allCurrentGameEvents = scoreSheetEntryEvents.Where(x => x.GameId == currentGameId).OrderBy(x => x.TimeElapsed).ToList();
          var results = ProcessOneGameScoreSheetEntryEvents(allCurrentGameEvents);
          scoreSheetEntryGoalTypes.AddRange(results);
        }
      }


      // loop through goal type results and update score sheet entries
      foreach (var goalTypeResult in scoreSheetEntryGoalTypes)
      {
        var scoreSheetEntry = scoreSheetEntries.Single(x => x.ScoreSheetEntryId == goalTypeResult.ScoreSheetEntryId);
        scoreSheetEntry.PowerPlayGoal = goalTypeResult.PowerPlayGoal;
        scoreSheetEntry.ShortHandedGoal = goalTypeResult.ShortHandedGoal;
      }

      // loop through the penalties and see if any goals where scored during them
      /*foreach (var scoreSheetEntryPenalty in scoreSheetEntryPenalties)
      {
        var gameId = scoreSheetEntryPenalty.GameId;
        var penaltyHomeTeam = scoreSheetEntryPenalty.HomeTeam;

        //get start/end time of the penalty
        var start = scoreSheetEntryPenalty.TimeElapsed;
        var end = start.Add(new TimeSpan(0, scoreSheetEntryPenalty.PenaltyMinutes, 0));
        var major = scoreSheetEntryPenalty.PenaltyMinutes == 5 ? true : false;



        //check if there were any goals during the penalty
        var goalsDuringPenalty = scoreSheetEntries.Where(x => x.GameId == gameId && x.TimeElapsed >= start && x.TimeElapsed <= end).OrderBy(x=>x.TimeElapsed).ToList();

        //now see if they were power play or shorthanded
        foreach (var goalDuringPenalty in goalsDuringPenalty)
        {
          // if it was a power play goal (hometeam != penalty hometeam)
          // and it was not a major penalty
          // then the power play is over, no need to continue processing
          if (goalDuringPenalty.HomeTeam != penaltyHomeTeam)
          {
            goalDuringPenalty.PowerPlayGoal = true;

            if (!major)
            {
              // then the power play is over, no need to continue processing
              break; 
            }
          }
          else
          {
            // it was a shorthanded goal
            goalDuringPenalty.ShortHandedGoal = true;
          }
        }
      }*/

      return scoreSheetEntries;
    }

    public int? ConvertPlayerNumberIntoPlayer(ICollection<GameRoster> gameRoster, string playerNumber)
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

    public DateTime ConvertYYYYMMDDIntoDateTime(int yyyymmdd)
    {
      if (yyyymmdd.ToString().Length != 8)
      {
        throw new ArgumentOutOfRangeException("yyyymmdd", yyyymmdd, "Must be length of 8");
      }

      var year = Convert.ToInt32(yyyymmdd.ToString().Substring(0,4));
      var month = Convert.ToInt32(yyyymmdd.ToString().Substring(4, 2));
      var day = Convert.ToInt32(yyyymmdd.ToString().Substring(6,2));
      var result = new DateTime(year, month, day);

      return result;
    }

    public int ConvertDateTimeIntoYYYYMMDD(DateTime? toConvert, bool ifNullReturnMax)
    {
      int result = -1;

      if (toConvert == null)
      {
        if (ifNullReturnMax)
        {
          result = GetMaxYYYYMMDD();
        }
        else
        {
          result = GetMinYYYYMMDD();
        }
      }
      else
      {
        result = (toConvert.Value.Year * 10000) + (toConvert.Value.Month * 100) + toConvert.Value.Day;
      }

      return result;
    }

    public int GetMinYYYYMMDD()
    {
      int result = 12345678;
      return result;
    }

    public int GetMaxYYYYMMDD()
    {
      int result = 87654321;
      return result;
    }

    public void ConvertCombinedRatingToPrimarySecondary(string ratingCombined, out int ratingPrimary, out int ratingSecondary)
    {
      var ratingParts = ratingCombined.Split('.');

      if (ratingParts.Length != 2)
      {
        throw new ArgumentException("ratingCombined (" + ratingCombined + ") needs to be X.Y");
      }

      ratingPrimary = Convert.ToInt32(ratingParts[0]);
      ratingSecondary = Convert.ToInt32(ratingParts[1]);

      return;
    }

    public string ConvertRatingPrimarySecondaryToCombined(int ratingPrimary, int ratingSecondary)
    {
      string ratingCombined = "";
      ratingCombined = ratingPrimary.ToString() + '.' + ratingSecondary.ToString();
      return ratingCombined;
    }

    public TimeSpan ConvertToOverallTimeElapsed(int period, string time)
    {
      var fullPeriodsAlreadyPlayedTime = new TimeSpan(0, (period - 1) * TimePerPeriod, 0);
      var playedInThisPeriodTime = new TimeSpan(0, TimePerPeriod, 0).Subtract(ConvertTimeToTimeSpan(time));

      var overallTime = playedInThisPeriodTime.Add(fullPeriodsAlreadyPlayedTime);

      if (overallTime.CompareTo(TimeSpan.Zero) < 0)
      {
        throw new ArgumentOutOfRangeException("overallTime", overallTime, "overallTime cannot be negative. period:" + period.ToString() + " time: " + time);
      }

      return overallTime;
    }

    public TimeSpan ConvertToOverallTimeRemaining(int period, string time, int totalPeriods)
    {
      var totalTime = new TimeSpan(0, totalPeriods * TimePerPeriod, 0);
      var fullPeriodsAlreadyPlayedTime = new TimeSpan(0, (period - 1) * TimePerPeriod, 0);
      var playedInThisPeriodTime = new TimeSpan(0, TimePerPeriod, 0).Subtract(ConvertTimeToTimeSpan(time));

      var overallTime = totalTime.Subtract(fullPeriodsAlreadyPlayedTime).Subtract(playedInThisPeriodTime);

      return overallTime;
    }

    public TimeSpan ConvertTimeToTimeSpan(string time)
    {
      TimeSpan result;

      if (string.IsNullOrWhiteSpace(time))
      {
        throw new ArgumentOutOfRangeException("time", time, "Cannot be null or empty");
      }
      else if (time.IndexOf('.') > -1)
      {
        var parts = time.Split('.');
        if (parts.Length != 2)
        {
          throw new ArgumentOutOfRangeException("parts.length", parts.Length, "Should be 2");
        }

        result = ConvertTimePartsToTimeSpan(parts[0], parts[1], time);
      }
      else if (time.IndexOf(':') > -1)
      {
        var parts = time.Split(':');
        if (parts.Length != 2)
        {
          throw new ArgumentOutOfRangeException("parts.length", parts.Length, "Should be 2");
        }

        result = ConvertTimePartsToTimeSpan(parts[0], parts[1], time);
      }
      else
      {
        // assume time is just seconds
        result = ConvertTimePartsToTimeSpan("0", time, time);
      }

      return result;
    }

    private TimeSpan ConvertTimePartsToTimeSpan(string timePart0, string timePart1, string origTime)
    {
      TimeSpan result;

      int part0, part1;

      if (string.IsNullOrWhiteSpace(timePart0))
      {
        timePart0 = "0";
      }

      if (string.IsNullOrWhiteSpace(timePart1))
      {
        timePart1 = "0";
      }

      try
      {
        part0 = Convert.ToInt32(timePart0);
      }
      catch
      {
        throw new ArgumentOutOfRangeException("timePart0", timePart0, "Could not covert timePart0 to Integer from origTime:" + origTime);
      }

      try
      {
        part1 = Convert.ToInt32(timePart1);
      }
      catch
      {
        throw new ArgumentOutOfRangeException("timePart1", timePart1, "Could not covert timePart1 to Integer from origTime:" + origTime);
      }

      result = new TimeSpan(0, part0, part1);

      return result;
    }
  }
}