using LO30.Data.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Services.Tests
{
  [TestClass()]
  public class Lo30DataService_Tests
  {
    Lo30DataService _lo30DataService;
    private Lo30DataSerializationService _lo30DataSerializationService;

    [TestInitialize]
    public void Before()
    {
      _lo30DataService = new Lo30DataService();
      _lo30DataSerializationService = new Lo30DataSerializationService();
    }

    #region DerivePlayerGameStats Tests
    [TestMethod()]
    public void DerivePlayerGameStats_NoScoreSheets_NoSubs()
    {
      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:false, pid: 401, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601, div: 0)}},
        new GameRoster(grid: 1, gtid: 101, pn: 2, line:1, pos:"F", g:false, pid: 402, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:60, div: 01)}},
        new GameRoster(grid: 1, gtid: 101, pn: 3, line:1, pos:"F", g:false, pid: 403, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:301, sid:501, tid:601, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 1, line:1, pos:"F", g:false, pid: 404, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 2, line:1, pos:"F", g:false, pid: 405, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 3, line:1, pos:"F", g:false, pid: 406, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}}
      };

      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _lo30DataService.DerivePlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 402, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 404, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(expected, playerGameStats);
    }

    [TestMethod()]
    public void DerivePlayerGameStats_NoScoreSheets_WithSubs()
    {
      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:false, pid: 401, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601, div: 0)}},
        new GameRoster(grid: 1, gtid: 101, pn: 2, line:1, pos:"F", g:false, pid: 408, sub: true, sfpid: 402){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601, div: 0)}},
        new GameRoster(grid: 1, gtid: 101, pn: 3, line:1, pos:"F", g:false, pid: 403, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:301, sid:501, tid:601, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 1, line:1, pos:"F", g:false, pid: 407, sub: true, sfpid: 404){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 2, line:1, pos:"F", g:false, pid: 405, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 3, line:1, pos:"F", g:false, pid: 406, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}}
      };

      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
      };
      
      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _lo30DataService.DerivePlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 408, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 407, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(expected, playerGameStats);
    }

    [TestMethod()]
    public void DerivePlayerGameStats_OneScoreSheets_WithSubs()
    {
      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:false, pid: 401, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601, div: 0)}},
        new GameRoster(grid: 1, gtid: 101, pn: 2, line:1, pos:"F", g:false, pid: 408, sub: true, sfpid: 402){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601, div: 0)}},
        new GameRoster(grid: 1, gtid: 101, pn: 3, line:1, pos:"F", g:false, pid: 403, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201, ostid:202){SeasonTeam = new SeasonTeam(stid:301, sid:501, tid:601, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 1, line:1, pos:"F", g:false, pid: 407, sub: true, sfpid: 404){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 2, line:1, pos:"F", g:false, pid: 405, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}},
        new GameRoster(grid: 1, gtid: 102, pn: 3, line:1, pos:"F", g:false, pid: 406, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202, ostid:201){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602, div: 0)}}
      };

      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 1, ht: true, time: "10.12", gtid:1, gpid: 401, a1pid: 408, a2pid: 403, a3pid: null, shg: true, ppg: false,  gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 1, ht: true, time: "6.12", gtid:1, gpid: 401, a1pid: 403, a2pid: null, a3pid: null, shg: true, ppg: false,  gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 2, ht: false, time: "12.22", gtid:2, gpid: 406, a1pid: null, a2pid: 405, a3pid: null, shg: false, ppg: true,  gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 2, ht: false, time: "11.12", gtid:2, gpid: 406, a1pid: 405, a2pid: null, a3pid: null, shg: false, ppg: true,  gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 3, ht: true, time: "3.12", gtid:1, gpid: 403, a1pid: 401, a2pid: 408, a3pid: null, shg: false, ppg: false,  gwg: true, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _lo30DataService.DerivePlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:false, g: 2, a: 1, p: 3, ppg: 0, shg: 2, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 408, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:true, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, gid: 701, sid: 501, stid: 201, pfs: false, line:1, pos:"F", sub:false, g: 1, a: 2, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 407, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:false, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, gid: 701, sid: 501, stid: 202, pfs: false, line:1, pos:"F", sub:false, g: 2, a: 0, p: 2, ppg: 2, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(expected, playerGameStats);
    }

    [TestMethod()]
    public void DerivePlayerGameStats_SeasonId54GameId3227()
    {
      string folderPath = @"D:\git\LO30\LO30Tests\TestData\SeasonId54GameId3227\";

      List<GameRoster> gameRosters = _lo30DataSerializationService.FromJsonFromFile<List<GameRoster>>(folderPath + "GameRosters.json");
      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = _lo30DataSerializationService.FromJsonFromFile<List<ScoreSheetEntryProcessed>>(folderPath + "ScoreSheetEntriesProcessed.json");
      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = _lo30DataSerializationService.FromJsonFromFile<List<ScoreSheetEntryPenaltyProcessed>>(folderPath + "ScoreSheetEntryPenaltiesProcessed.json");

      var playerGameStats = _lo30DataService.DerivePlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var playerGameStatsPartial = playerGameStats.Where(x=>x.PlayerId == 593 || x.PlayerId == 644 || x.PlayerId == 680).ToList();

      var expected = new List<PlayerStatGame>() 
      {
          new PlayerStatGame(pid: 593, gid: 3200, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 2, p: 3, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3204, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 0, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3208, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3216, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: false, g: 4, a: 3, p: 7, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3218, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3222, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3227, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 3, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3200, sid: 54, stid: 314, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3205, sid: 54, stid: 314, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3207, sid: 54, stid: 314, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3213, sid: 54, stid: 309, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3214, sid: 54, stid: 313, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3215, sid: 54, stid: 314, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3216, sid: 54, stid: 309, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3218, sid: 54, stid: 315, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3219, sid: 54, stid: 314, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3227, sid: 54, stid: 314, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 0, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 680, gid: 3204, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 680, gid: 3208, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(expected, playerGameStatsPartial);
    }
    #endregion

    #region DerivePlayerSeasonTeamStats Tests
    [TestMethod()]
    public void DerivePlayerSeasonTeamStats_OnePlayer_OneTeam_OneGame()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, gid: 1, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2)
      };

      var playerSeasonTeamStats = _lo30DataService.DerivePlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, stid:1, pfs: false, sid:1, line:1, pos:"F", sub:false, games:1, g:1,a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStats);
    }

    [TestMethod()]
    public void DerivePlayerSeasonTeamStats_OnePlayer_OneTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, gid: 1, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 2, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 3, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 4, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 5, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 6, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 7, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonTeamStats = _lo30DataService.DerivePlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, stid:1, pfs: false, sid: 1, line:1, pos:"F", sub:false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStats);
    }

    [TestMethod()]
    public void DerivePlayerSeasonTeamStats_OnePlayer_ManyTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, gid: 1, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 2, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 3, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 4, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 5, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 6, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 7, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 1, gid: 8, sid: 1, stid: 4, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 9, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 10, sid: 1, stid: 2, pfs: false,  line:1, pos:"F", sub: true, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 11, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 12, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 13, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 14, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
      };

      var playerSeasonTeamStats = _lo30DataService.DerivePlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, stid:1, pfs: false, sid:1, line:1, pos:"F", sub:false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:1, stid:2, pfs: false, sid:1, line:1, pos:"F", sub:true, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:1, stid:3, pfs: false, sid:1, line:1, pos:"F", sub:true, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:1, stid:4, pfs: false, sid:1, line:1, pos:"F", sub:true, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStats);
    }

    [TestMethod()]
    public void DerivePlayerSeasonTeamStats_ManyPlayer_ManyTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, gid: 1, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 2, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 3, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 4, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 5, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 6, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 7, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 1, gid: 8, sid: 1, stid: 4, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 9, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 10, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 11, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 12, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 13, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 14, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),

        new PlayerStatGame(pid: 2, gid: 1, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 2, gid: 2, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 3, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 4, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 2, gid: 5, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 2, gid: 6, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 7, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 2, gid: 8, sid: 1, stid: 4, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 2, gid: 9, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 10, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 11, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 2, gid: 12, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 2, gid: 13, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 14, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),

        new PlayerStatGame(pid: 3, gid: 1, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 3, gid: 2, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 3, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 4, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 3, gid: 5, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 3, gid: 6, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 7, sid: 1, stid: 1, pfs: false, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 3, gid: 8, sid: 1, stid: 4, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 3, gid: 9, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 10, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 11, sid: 1, stid: 2, pfs: false, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 3, gid: 12, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 3, gid: 13, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 14, sid: 1, stid: 3, pfs: false, line:1, pos:"F", sub: true, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonTeamStats = _lo30DataService.DerivePlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, sid:1, stid:1, pfs: false, line:1, pos:"F", sub: false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:1, sid:1, stid:2, pfs: false, line:1, pos:"F", sub: true, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:1, sid:1, stid:3, pfs: false, line:1, pos:"F", sub: true, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:1, sid:1, stid:4, pfs: false, line:1, pos:"F", sub: true, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:2, sid:1, stid:1, pfs: false, line:1, pos:"F", sub: false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:2, sid:1, stid:2, pfs: false, line:1, pos:"F", sub: true, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:2, sid:1, stid:3, pfs: false, line:1, pos:"F", sub: true, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:2, sid:1, stid:4, pfs: false, line:1, pos:"F", sub: true, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:3, sid:1, stid:1, pfs: false, line:1, pos:"F", sub: false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:3, sid:1, stid:2, pfs: false, line:1, pos:"F", sub: true, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:3, sid:1, stid:3, pfs: false, line:1, pos:"F", sub: true, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:3, sid:1, stid:4, pfs: false, line:1, pos:"F", sub: true, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStats);
    }

    [TestMethod()]
    public void DerivePlayerSeasonTeamStats_SeasonId54GameId3227()
    {
      string folderPath = @"D:\git\LO30\LO30Tests\TestData\SeasonId54GameId3227\";

      List<GameRoster> gameRosters = _lo30DataSerializationService.FromJsonFromFile<List<GameRoster>>(folderPath + "GameRosters.json");
      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = _lo30DataSerializationService.FromJsonFromFile<List<ScoreSheetEntryProcessed>>(folderPath + "ScoreSheetEntriesProcessed.json");
      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = _lo30DataSerializationService.FromJsonFromFile<List<ScoreSheetEntryPenaltyProcessed>>(folderPath + "ScoreSheetEntryPenaltiesProcessed.json");

      var playerGameStats = _lo30DataService.DerivePlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var playerSeasonTeamStats = _lo30DataService.DerivePlayerSeasonTeamStats(playerGameStats);

      var playerSeasonTeamStatsPartial = playerSeasonTeamStats.Where(x => x.PlayerId == 593 || x.PlayerId == 644 || x.PlayerId == 680).ToList();

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid: 593, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: false, games: 7, g: 8, a: 11, p: 19, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 644, sid: 54, stid: 309, pfs: false, line:1, pos:"F", sub: true, games: 2, g: 1, a: 1, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 644, sid: 54, stid: 313, pfs: false, line:1, pos:"F", sub: true, games: 1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 644, sid: 54, stid: 314, pfs: false, line:1, pos:"F", sub: false, games: 6, g: 1, a: 3, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 644, sid: 54, stid: 315, pfs: false, line:1, pos:"F", sub: true, games: 1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 680, sid: 54, stid: 308, pfs: false, line:1, pos:"F", sub: true, games: 2, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };


      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStatsPartial);
    }
    #endregion

    #region DeriveGoalieGameStats Tests
    [TestMethod()]
    public void DeriveGoalieGameStats_NoGameOutcomes_NoSubs()
    {
      GameTeam gt101 = new GameTeam(gid: 701, ht: true, stid: 201, ostid: 202) { SeasonTeam = new SeasonTeam(stid: 201, sid: 501, tid: 601, div: 0) };
      GameTeam gt102 = new GameTeam(gid: 701, ht: false, stid: 202, ostid: 201) { SeasonTeam = new SeasonTeam(stid: 202, sid: 501, tid: 601, div: 0) };

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:true, pid: 401, sub: false, sfpid: null){GameTeam = gt101},
        new GameRoster(grid: 1, gtid: 102, pn: 30, line:1, pos:"F", g:true, pid: 402, sub: false, sfpid: null){GameTeam = gt102},
      };

      List<GameOutcome> gameOutcomes = new List<GameOutcome>()
      {
      };

      var goalieGameStats = _lo30DataService.DeriveGoalieGameStats(gameOutcomes, gameRosters);

      var expected = new List<GoalieStatGame>() 
      {
        new GoalieStatGame(pid: 401, gid: 701, sid: 501, stid: 201, pfs: false, sub: false, ga: 0, so: 0, w: 0),
        new GoalieStatGame(pid: 402, gid: 701, sid: 501, stid: 202, pfs: false, sub: false, ga: 0, so: 0, w: 0)
      };

      AssertAreEqualGoalieStatGameLists(expected, goalieGameStats);
    }

    [TestMethod()]
    public void DeriveGoalieGameStats_NoGameOutcomes_WithSubs()
    {
      GameTeam gt101 = new GameTeam(gid: 701, ht: true, stid: 201, ostid: 202) { SeasonTeam = new SeasonTeam(stid: 201, sid: 501, tid: 601, div: 0) };
      GameTeam gt102 = new GameTeam(gid: 701, ht: false, stid: 202, ostid: 201) { SeasonTeam = new SeasonTeam(stid: 202, sid: 501, tid: 601, div: 0) };

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:true, pid: 401, sub: false, sfpid: null){GameTeam = gt101},
        new GameRoster(grid: 1, gtid: 102, pn: 30, line:1, pos:"F", g:true, pid: 408, sub: true, sfpid: 402){GameTeam = gt102},
      };

      List<GameOutcome> gameOutcomes = new List<GameOutcome>()
      {
      };


      var goalieGameStats = _lo30DataService.DeriveGoalieGameStats(gameOutcomes, gameRosters);

      var expected = new List<GoalieStatGame>() 
      {
        new GoalieStatGame(pid: 401, gid: 701, sid: 501, stid: 201, pfs: false, sub: false, ga: 0, so: 0, w: 0),
        new GoalieStatGame(pid: 408, gid: 701, sid: 501, stid: 202, pfs: false, sub: true, ga: 0, so: 0, w: 0)
      };

      AssertAreEqualGoalieStatGameLists(expected, goalieGameStats);
    }

    [TestMethod()]
    public void DeriveGoalieGameStats_OneScoreSheets_WithSubs()
    {
      GameTeam gt101 = new GameTeam(gid: 701, ht: true, stid: 201, ostid: 202) { SeasonTeam = new SeasonTeam(stid: 201, sid: 501, tid: 601, div: 0) };
      GameTeam gt102 = new GameTeam(gid: 701, ht: false, stid: 202, ostid: 201) { SeasonTeam = new SeasonTeam(stid: 202, sid: 501, tid: 601, div: 0) };

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:true, pid: 401, sub: false, sfpid: null){GameTeam = gt101},
        new GameRoster(grid: 1, gtid: 102, pn: 30, line:1, pos:"F", g:true, pid: 408, sub: true, sfpid: 402){GameTeam = gt102},
      };

      List<GameOutcome> gameOutcomes = new List<GameOutcome>()
      {
        new GameOutcome(gtid: 101, res: "W", gf: 4, ga: 2, pim: 2, over: false, ogtid:102){GameTeam = gt101, OpponentGameTeam = gt102},
        new GameOutcome(gtid: 102, res: "L", gf: 2, ga: 4, pim: 2, over: false, ogtid:101){GameTeam = gt102, OpponentGameTeam = gt101}
      };

      var goalieGameStats = _lo30DataService.DeriveGoalieGameStats(gameOutcomes, gameRosters);

      var expected = new List<GoalieStatGame>() 
      {
        new GoalieStatGame(pid: 401, gid: 701, sid: 501, stid: 201, pfs: false, sub: false, ga: 2, so: 0, w: 1),
        new GoalieStatGame(pid: 408, gid: 701, sid: 501, stid: 202, pfs: false, sub: true, ga: 4, so: 0, w: 0)
      };

      AssertAreEqualGoalieStatGameLists(expected, goalieGameStats);
    }

    [TestMethod()]
    public void DeriveGoalieGameStats_SeasonId54GameId3227()
    {
      string folderPath = @"D:\git\LO30\LO30Tests\TestData\SeasonId54GameId3227\";

      List<GameRoster> gameRosters = _lo30DataSerializationService.FromJsonFromFile<List<GameRoster>>(folderPath + "GameRosters.json");
      List<GameOutcome> gameOutcomes = _lo30DataSerializationService.FromJsonFromFile<List<GameOutcome>>(folderPath + "GameOutcomes.json");

      var goalieGameStats = _lo30DataService.DeriveGoalieGameStats(gameOutcomes, gameRosters);

      var goalieGameStatsPartial = goalieGameStats.Where(x => x.PlayerId == 634 || x.PlayerId == 619 || x.PlayerId == 81).ToList();

      var expected = new List<GoalieStatGame>() 
      {
        new GoalieStatGame(pid: 81, gid: 3201, sid: 54, stid: 311, pfs: false, sub: true, ga: 4, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3202, sid: 54, stid: 313, pfs: false, sub: false, ga: 3, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3206, sid: 54, stid: 313, pfs: false, sub: false, ga: 1, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3208, sid: 54, stid: 313, pfs: false, sub: false, ga: 2, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3214, sid: 54, stid: 313, pfs: false, sub: false, ga: 4, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3221, sid: 54, stid: 313, pfs: false, sub: false, ga: 2, so: 0, w: 1),
        new GoalieStatGame(pid: 619, gid: 3225, sid: 54, stid: 313, pfs: false, sub: false, ga: 4, so: 0, w: 0),
        new GoalieStatGame(pid: 634, gid: 3203, sid: 54, stid: 315, pfs: false, sub: false, ga: 0, so: 1, w: 1),
        new GoalieStatGame(pid: 634, gid: 3209, sid: 54, stid: 315, pfs: false, sub: false, ga: 1, so: 0, w: 0),
        new GoalieStatGame(pid: 634, gid: 3211, sid: 54, stid: 315, pfs: false, sub: false, ga: 1, so: 0, w: 1),
        new GoalieStatGame(pid: 634, gid: 3214, sid: 54, stid: 315, pfs: false, sub: false, ga: 0, so: 1, w: 1),
        new GoalieStatGame(pid: 634, gid: 3218, sid: 54, stid: 315, pfs: false, sub: false, ga: 6, so: 0, w: 0),
        new GoalieStatGame(pid: 634, gid: 3224, sid: 54, stid: 315, pfs: false, sub: false, ga: 2, so: 0, w: 1),
        new GoalieStatGame(pid: 634, gid: 3226, sid: 54, stid: 315, pfs: false, sub: false, ga: 0, so: 1, w: 1)
      };

      AssertAreEqualGoalieStatGameLists(expected, goalieGameStatsPartial);
    }
    #endregion

    #region UpdateScoreSheetEntryProcessedWithPPAndSH Tests
    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_NoGoalsNoPenalties()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = scoreSheetEntries;

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_GoalsNoPenalties()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:2, ht: true, time: "13.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:3, ht: true, time: "2.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = scoreSheetEntries;

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_GoalsPenaltiesNoPPNoSH()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:2, ht: true, time: "13.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:3, ht: true, time: "2.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"9.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:2, gid:101, per:2, ht:false, time:"3.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:3, gid:101, per:3, ht:false, time:"4.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = scoreSheetEntries;

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_GoalsPenalties1PPNoSH()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:2, ht: true, time: "13.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:3, ht: true, time: "2.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"10.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:2, gid:101, per:2, ht:false, time:"3.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:3, gid:101, per:3, ht:false, time:"4.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:2, ht: true, time: "13.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:3, ht: true, time: "2.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_GoalsPenalties1PP1SH()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:2, ht: true, time: "13.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:3, ht: true, time: "2.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"10.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:2, gid:101, per:2, ht:true, time:"13.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:3, gid:101, per:3, ht:false, time:"4.27", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:2, ht: true, time: "13.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: true, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:3, ht: true, time: "2.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_GoalsBeforeAndAfterPenalty()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:2, ht: true, time: "8.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:3, ht: true, time: "2.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"12.12", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:2, gid:101, per:1, ht:true, time:"10.10", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:2, ht: true, time: "8.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:3, ht: true, time: "2.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_MultiplePPGoalsDuringPenalty()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:1, ht: true, time: "9.23", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:1, ht: true, time: "8.45", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"10.12", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:2, gid:101, per:2, ht:true, time:"10.10", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:1, ht: true, time: "9.23", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:1, ht: true, time: "8.45", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_MultipleSHPPGoalsDuringPenalty()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: false, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:1, ht: false, time: "9.55", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:1, ht: true, time: "9.23", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:4, gid:101, per:1, ht: false, time: "8.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"10.12", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now),
        new ScoreSheetEntryPenaltyProcessed(ssepid:2, gid:101, per:2, ht:true, time:"10.10", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: false, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: true, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:1, ht: false, time: "9.55", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: true, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:1, ht: true, time: "9.23", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:4, gid:101, per:1, ht: false, time: "8.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);

    }
    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_PPGoalAtStartOfPenalty()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"10.11", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_PPGoalAtEndOfPenalty()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "8.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"10.11", gtid:201, playid: 302, penid: 401, pim:2, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "8.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }

    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_MultiplePPGoalsDuringMatchPenalty()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.13", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:1, ht: true, time: "9.23", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:1, ht: true, time: "7.43", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:4, gid:101, per:1, ht: true, time: "6.33", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:5, gid:101, per:1, ht: true, time: "5.56", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:6, gid:101, per:1, ht: true, time: "5.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"10.12", gtid:201, playid: 302, penid: 401, pim:5, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.13", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:1, ht: true, time: "9.23", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:1, ht: true, time: "7.43", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:4, gid:101, per:1, ht: true, time: "6.33", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:5, gid:101, per:1, ht: true, time: "5.56", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:6, gid:101, per:1, ht: true, time: "5.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }
    
    [TestMethod()]
    public void UpdateScoreSheetEntryProcessedWithPPAndSH_MultipleSHPPGoalsDuringMatchPenalty()
    {
      List<ScoreSheetEntryProcessed> scoreSheetEntries = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.13", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:1, ht: false, time: "9.23", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:1, ht: true, time: "7.43", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:4, gid:101, per:1, ht: true, time: "6.33", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:5, gid:101, per:1, ht: false, time: "5.56", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:6, gid:101, per:1, ht: true, time: "5.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenalties = new List<ScoreSheetEntryPenaltyProcessed>()
      {
        new ScoreSheetEntryPenaltyProcessed(ssepid:1, gid:101, per:1, ht:false, time:"10.12", gtid:201, playid: 302, penid: 401, pim:5, upd: DateTime.Now)
      };

      var actual = _lo30DataService.UpdateScoreSheetEntryProcessedWithPPAndSH(scoreSheetEntries, scoreSheetEntryPenalties);

      var expected = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid:1, gid:101, per:1, ht: true, time: "10.13", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:2, gid:101, per:1, ht: false, time: "9.23", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: true, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:3, gid:101, per:1, ht: true, time: "7.43", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:4, gid:101, per:1, ht: true, time: "6.33", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: true, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:5, gid:101, per:1, ht: false, time: "5.56", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: true, ppg: false, gwg: false, upd: DateTime.Now),
        new ScoreSheetEntryProcessed(sseid:6, gid:101, per:1, ht: true, time: "5.11", gtid: 201, gpid: 301, a1pid: null, a2pid: null, a3pid: null, shg: false, ppg: false, gwg: false, upd: DateTime.Now)
      };

      AssertAreEqualScoreSheetEntryProcessedLists(expected, actual);
    }
    #endregion

    #region ConvertYYYYMMDDIntoDateTime
    [TestMethod()]
    public void ConvertYYYYMMDDIntoDateTime_TooSmall()
    {
      try
      {
        int yyyymmdd = 2012;
        var result = _lo30DataService.ConvertYYYYMMDDIntoDateTime(yyyymmdd);
        Assert.Fail("Expecting exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType(), "Error type");
        Assert.IsTrue(ex.Message.Contains("Must be length of 8"), "Error message");
      }
    }
    public void ConvertYYYYMMDDIntoDateTime_TooLong()
    {
      try
      {
        int yyyymmdd = 2012101112;
        var result = _lo30DataService.ConvertYYYYMMDDIntoDateTime(yyyymmdd);
        Assert.Fail("Expecting exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType(), "Error type");
        Assert.IsTrue(ex.Message.Contains("Must be length of 8"), "Error message");
      }
    }
    public void ConvertYYYYMMDDIntoDateTime_Correct()
    {
        int yyyymmdd = 19770223;
        var result = _lo30DataService.ConvertYYYYMMDDIntoDateTime(yyyymmdd);
        Assert.AreEqual(new DateTime(1977, 2, 23), result, "Result");
    }
    #endregion

    #region ConvertDateTimeIntoYYYYMMDD
    [TestMethod()]
    public void ConvertDateTimeIntoYYYYMMDD_Null_Min()
    {
      DateTime? input = null;
      var result = _lo30DataService.ConvertDateTimeIntoYYYYMMDD(input, ifNullReturnMax: false);
      Assert.AreEqual(12345678, result, "Result");
    }
    [TestMethod()]
    public void ConvertDateTimeIntoYYYYMMDD_Null_Max()
    {
      DateTime? input = null;
      var result = _lo30DataService.ConvertDateTimeIntoYYYYMMDD(input, ifNullReturnMax: true);
      Assert.AreEqual(87654321, result, "Result");
    }
    public void ConvertDateTimeIntoYYYYMMDD_Correct()
    {
      DateTime? input = new DateTime(1977, 2, 23);
      var result = _lo30DataService.ConvertDateTimeIntoYYYYMMDD(input, ifNullReturnMax: false);
      Assert.AreEqual(19770223, result, "Result");
    }
    #endregion

    #region ConvertTimeToTimeSpan
    [TestMethod()]
    public void ConvertTimeToTimeSpan_ErrorIf_TimeIsNull()
    {
      string expectedExMsg = "Cannot be null or empty";
      try
      {
        string time = null;
        var result = _lo30DataService.ConvertTimeToTimeSpan(time);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }
    [TestMethod()]
    public void ConvertTimeToTimeSpan_TimeWithPeriod()
    {
      string time = "10.11";
      var result = _lo30DataService.ConvertTimeToTimeSpan(time);
      Assert.AreEqual(new TimeSpan(0, 10, 11), result, "Result");
    }
    [TestMethod()]
    public void ConvertTimeToTimeSpan_TimeStartsWithPeriod()
    {
      string time = ".11";
      var result = _lo30DataService.ConvertTimeToTimeSpan(time);
      Assert.AreEqual(new TimeSpan(0, 0, 11), result, "Result");
    }
    [TestMethod()]
    public void ConvertTimeToTimeSpan_TimeWithColon()
    {
      string time = "10:11";
      var result = _lo30DataService.ConvertTimeToTimeSpan(time);
      Assert.AreEqual(new TimeSpan(0, 10, 11), result, "Result");
    }
    #endregion

    #region ConvertToOverallTimeRemaining
    [TestMethod()]
    public void ConvertToOverallTimeRemaining_ErrorIf_TimeIsNull()
    {
      string expectedExMsg = "Cannot be null or empty";
      try
      {
        int totalPeriods=3;
        int period =1;
        string time = null;
        var result = _lo30DataService.ConvertToOverallTimeRemaining(period, time, totalPeriods);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }
    [TestMethod()]
    public void ConvertToOverallTimeRemaining_1SecondIntoTheGame()
    {
      int totalPeriods = 3;
      int period = 1;
      string time = "13.59";
      var result = _lo30DataService.ConvertToOverallTimeRemaining(period, time, totalPeriods);
      // 3 periods, 14 mins each = total time of 42 mins;
      // 14:00-13:59 = 0:01
      // 42:00-0:01 = 41:59
      Assert.AreEqual(new TimeSpan(0, 41, 59), result, "Result");
    }
    [TestMethod()]
    public void ConvertToOverallTimeRemaining_1SecondLeftInTheGame()
    {
      int totalPeriods = 3;
      int period = 3;
      string time = "0.01";
      var result = _lo30DataService.ConvertToOverallTimeRemaining(period, time, totalPeriods);
      // 3 periods, 14 mins each = total time of 42 mins;
      // 14:00+14:00+(14:00-13:59) = 41:59
      // 42:00-41:59 = 0:01
      Assert.AreEqual(new TimeSpan(0, 0, 1), result, "Result");
    }
    [TestMethod()]
    public void ConvertToOverallTimeRemaining_TimeInPeriod1()
    {
      int totalPeriods = 3;
      int period = 1;
      string time = "10.11";
      var result = _lo30DataService.ConvertToOverallTimeRemaining(period, time, totalPeriods);
      // 3 periods, 14 mins each = total time of 42 mins;
      // 14:00-10:11 = 3:49
      // 42:00-3:49 = 38:11
      Assert.AreEqual(new TimeSpan(0, 38, 11), result, "Result");
    }
    [TestMethod()]
    public void ConvertToOverallTimeRemaining_TimeInPeriod2()
    {
      int totalPeriods = 3;
      int period = 2;
      string time = "5.23";
      var result = _lo30DataService.ConvertToOverallTimeRemaining(period, time, totalPeriods);
      // 3 periods, 14 mins each = total time of 42 mins;
      // 14:00+(14:00-5:23) = 22:37
      // 42:00-22:37 = 19:23
      Assert.AreEqual(new TimeSpan(0, 19, 23), result, "Result");
    }
    [TestMethod()]
    public void ConvertToOverallTimeRemaining_TimeInPeriod3()
    {
      int totalPeriods = 3;
      int period = 3;
      string time = "2.57";
      var result = _lo30DataService.ConvertToOverallTimeRemaining(period, time, totalPeriods);
      // 3 periods, 14 mins each = total time of 42 mins;
      // 14:00+14:00+(14:00-2:57) = 39:03
      // 42:00-39:03 = 2:57
      Assert.AreEqual(new TimeSpan(0, 2, 57), result, "Result");
    }

    #endregion

    #region ConvertToOverallTimeElapsed
    [TestMethod()]
    public void ConvertToOverallTimeElapsed_ErrorIf_TimeIsNull()
    {
      string expectedExMsg = "Cannot be null or empty";
      try
      {
        int period = 1;
        string time = null;
        var result = _lo30DataService.ConvertToOverallTimeElapsed(period, time);
        Assert.Fail("Expected exception to be thrown");
      }
      catch (Exception ex)
      {
        Assert.AreEqual(expectedExMsg, ex.Message.Substring(0, expectedExMsg.Length), "Not expected exception");
      }
    }
    [TestMethod()]
    public void ConvertToOverallTimeElapsed_1SecondIntoTheGame()
    {
      int period = 1;
      string time = "13.59";
      var result = _lo30DataService.ConvertToOverallTimeElapsed(period, time);
      // 14:00-13:59 = 0:01
      Assert.AreEqual(new TimeSpan(0, 0, 1), result, "Result");
    }
    [TestMethod()]
    public void ConvertToOverallTimeElapsed_1SecondLeftInTheGame()
    {
      int period = 3;
      string time = "0.01";
      var result = _lo30DataService.ConvertToOverallTimeElapsed(period, time);
      // 14:00+14:00+(14:00-13:59) = 41:59
      Assert.AreEqual(new TimeSpan(0, 41, 59), result, "Result");
    }
    [TestMethod()]
    public void ConvertToOverallTimeElapsed_TimeInPeriod1()
    {
      int period = 1;
      string time = "10.11";
      var result = _lo30DataService.ConvertToOverallTimeElapsed(period, time);
      // 14:00-10:11 = 3:49
      Assert.AreEqual(new TimeSpan(0, 3, 49), result, "Result");
    }
    [TestMethod()]
    public void ConvertToOverallTimeElapsed_TimeInPeriod2()
    {
      int period = 2;
      string time = "5.23";
      var result = _lo30DataService.ConvertToOverallTimeElapsed(period, time);
      // 14:00+(14:00-5:23) = 22:37
      Assert.AreEqual(new TimeSpan(0, 22, 37), result, "Result");
    }
    [TestMethod()]
    public void ConvertToOverallTimeElapsed_TimeInPeriod3()
    {
      int period = 3;
      string time = "2.57";
      var result = _lo30DataService.ConvertToOverallTimeElapsed(period, time);
      // 14:00+14:00+(14:00-2:57) = 39:03
      Assert.AreEqual(new TimeSpan(0, 39, 03), result, "Result");
    }

    #endregion


    #region Asserts
    private void AssertAreEqualPlayerStatGameLists(List<PlayerStatGame> expected, List<PlayerStatGame> actual)
    {

      Assert.AreEqual(expected.Count, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.PlayerId == expected[e].PlayerId &&
                                          x.GameId == expected[e].GameId)
                                .FirstOrDefault();

        var locationKey = string.Format("pid: {0}, gid: {1}",
                                    expected[e].PlayerId,
                                    expected[e].GameId);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        AssertAreEqualPlayerStatGameItem(expected[e], actualMatch, locationKey);
      }
    }

    private void AssertAreEqualPlayerStatGameListsPartial(List<PlayerStatGame> expected, List<PlayerStatGame> actual, int expectedCount)
    {

      Assert.AreEqual(expectedCount, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.PlayerId == expected[e].PlayerId &&
                                          x.GameId == expected[e].GameId)
                                .FirstOrDefault();

        var locationKey = string.Format("pid: {0}, gid: {1}",
                                    expected[e].PlayerId,
                                    expected[e].GameId);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        AssertAreEqualPlayerStatGameItem(expected[e], actualMatch, locationKey);
      }
    }

    private void AssertAreEqualPlayerStatGameItem(PlayerStatGame expected, PlayerStatGame actual, string locationKey)
    {
      Assert.AreEqual(expected.SeasonId, actual.SeasonId, "SeasonId key: " + locationKey);
      Assert.AreEqual(expected.SeasonTeamId, actual.SeasonTeamId, "SeasonTeamId key: " + locationKey);
      Assert.AreEqual(expected.Sub, actual.Sub, "Sub key: " + locationKey);
      Assert.AreEqual(expected.Goals, actual.Goals, "Goals key: " + locationKey);
      Assert.AreEqual(expected.Assists, actual.Assists, "Assists key: " + locationKey);
      Assert.AreEqual(expected.Points, actual.Points, "Points key: " + locationKey);
      Assert.AreEqual(expected.PowerPlayGoals, actual.PowerPlayGoals, "PowerPlayGoals key: " + locationKey);
      Assert.AreEqual(expected.ShortHandedGoals, actual.ShortHandedGoals, "ShortHandedGoals key: " + locationKey);
      Assert.AreEqual(expected.GameWinningGoals, actual.GameWinningGoals, "GameWinningGoals key: " + locationKey);
      Assert.AreEqual(expected.PenaltyMinutes, actual.PenaltyMinutes, "PenaltyMinutes key: " + locationKey);
    }

    private void AssertAreEqualPlayerStatSeasonTeamLists(List<PlayerStatSeasonTeam> expected, List<PlayerStatSeasonTeam> actual)
    {

      Assert.AreEqual(expected.Count, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.PlayerId == expected[e].PlayerId &&
                                          x.SeasonTeamId == expected[e].SeasonTeamId)
                                .FirstOrDefault();

        var locationKey = string.Format("pid: {0}, stid: {1}",
                                    expected[e].PlayerId,
                                    expected[e].SeasonTeamId);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        AssertAreEqualPlayerStatSeasonTeamItem(expected[e], actualMatch, locationKey);
      }
    }

    private void AssertAreEqualPlayerStatSeasonTeamListsPartial(List<PlayerStatSeasonTeam> expected, List<PlayerStatSeasonTeam> actual, int expectedCount)
    {

      Assert.AreEqual(expectedCount, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.PlayerId == expected[e].PlayerId &&
                                          x.SeasonTeamId == expected[e].SeasonTeamId)
                                .FirstOrDefault();

        var locationKey = string.Format("pid: {0}, stid: {1}",
                                    expected[e].PlayerId,
                                    expected[e].SeasonTeamId);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        AssertAreEqualPlayerStatSeasonTeamItem(expected[e], actualMatch, locationKey);
      }
    }

    private void AssertAreEqualPlayerStatSeasonTeamItem(PlayerStatSeasonTeam expected, PlayerStatSeasonTeam actual, string locationKey)
    {
      Assert.AreEqual(expected.SeasonId, actual.SeasonId, "SeasonId key: " + locationKey);
      Assert.AreEqual(expected.Sub, actual.Sub, "Sub key: " + locationKey);
      Assert.AreEqual(expected.Games, actual.Games, "Games key: " + locationKey);
      Assert.AreEqual(expected.Goals, actual.Goals, "Goals key: " + locationKey);
      Assert.AreEqual(expected.Assists, actual.Assists, "Assists key: " + locationKey);
      Assert.AreEqual(expected.Points, actual.Points, "Points key: " + locationKey);
      Assert.AreEqual(expected.PowerPlayGoals, actual.PowerPlayGoals, "PowerPlayGoals key: " + locationKey);
      Assert.AreEqual(expected.ShortHandedGoals, actual.ShortHandedGoals, "ShortHandedGoals key: " + locationKey);
      Assert.AreEqual(expected.GameWinningGoals, actual.GameWinningGoals, "GameWinningGoals key: " + locationKey);
      Assert.AreEqual(expected.PenaltyMinutes, actual.PenaltyMinutes, "PenaltyMinutes key: " + locationKey);
      
    }

    private void AssertAreEqualGoalieStatGameLists(List<GoalieStatGame> expected, List<GoalieStatGame> actual)
    {

      Assert.AreEqual(expected.Count, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.PlayerId == expected[e].PlayerId &&
                                          x.GameId == expected[e].GameId)
                                .FirstOrDefault();

        var locationKey = string.Format("pid: {0}, gid: {1}",
                                    expected[e].PlayerId,
                                    expected[e].GameId);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        AssertAreEqualGoalieStatGameItem(expected[e], actualMatch, locationKey);
      }
    }

    private void AssertAreEqualGoalieStatGameListsPartial(List<GoalieStatGame> expected, List<GoalieStatGame> actual, int expectedCount)
    {

      Assert.AreEqual(expectedCount, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.PlayerId == expected[e].PlayerId &&
                                          x.GameId == expected[e].GameId)
                                .FirstOrDefault();

        var locationKey = string.Format("pid: {0}, gid: {1}",
                                    expected[e].PlayerId,
                                    expected[e].GameId);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        AssertAreEqualGoalieStatGameItem(expected[e], actualMatch, locationKey);
      }
    }

    private void AssertAreEqualGoalieStatGameItem(GoalieStatGame expected, GoalieStatGame actual, string locationKey)
    {
      Assert.AreEqual(expected.SeasonId, actual.SeasonId, "SeasonId key: " + locationKey);
      Assert.AreEqual(expected.SeasonTeamId, actual.SeasonTeamId, "SeasonTeamId key: " + locationKey);
      Assert.AreEqual(expected.Sub, actual.Sub, "Sub key: " + locationKey);
      Assert.AreEqual(expected.GoalsAgainst, actual.GoalsAgainst, "GoalsAgainst key: " + locationKey);
      Assert.AreEqual(expected.Shutouts, actual.Shutouts, "Shutouts key: " + locationKey);
      Assert.AreEqual(expected.Wins, actual.Wins, "Wins key: " + locationKey);
    }

    private void AssertAreEqualScoreSheetEntryProcessedLists(List<ScoreSheetEntryProcessed> expected, List<ScoreSheetEntryProcessed> actual)
    {

      Assert.AreEqual(expected.Count, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.ScoreSheetEntryId == expected[e].ScoreSheetEntryId)
                                .FirstOrDefault();

        var locationKey = string.Format("sseid: {0}",
                                    expected[e].ScoreSheetEntryId);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        AssertAreEqualScoreSheetEntryProcessedItem(expected[e], actualMatch, locationKey);
      }
    }

    private void AssertAreEqualScoreSheetEntryProcessedItem(ScoreSheetEntryProcessed expected, ScoreSheetEntryProcessed actual, string locationKey)
    {
      Assert.AreEqual(expected.Assist1PlayerId, actual.Assist1PlayerId, "Assist1PlayerId key: " + locationKey);
      Assert.AreEqual(expected.Assist2PlayerId, actual.Assist2PlayerId, "Assist2PlayerId key: " + locationKey);
      Assert.AreEqual(expected.Assist3PlayerId, actual.Assist3PlayerId, "Assist3PlayerId key: " + locationKey);
      Assert.AreEqual(expected.GameId, actual.GameId, "GameId key: " + locationKey);
      Assert.AreEqual(expected.GameTeamId, actual.GameTeamId, "GameTeamId key: " + locationKey);
      Assert.AreEqual(expected.GameWinningGoal, actual.GameWinningGoal, "GameWinningGoal key: " + locationKey);
      Assert.AreEqual(expected.GoalPlayerId, actual.GoalPlayerId, "GoalPlayerId key: " + locationKey);
      Assert.AreEqual(expected.HomeTeam, actual.HomeTeam, "HomeTeam key: " + locationKey);
      Assert.AreEqual(expected.Period, actual.Period, "Period key: " + locationKey);
      Assert.AreEqual(expected.PowerPlayGoal, actual.PowerPlayGoal, "PowerPlayGoal key: " + locationKey);
      Assert.AreEqual(expected.ShortHandedGoal, actual.ShortHandedGoal, "ShortHandedGoal key: " + locationKey);
      Assert.AreEqual(expected.TimeElapsed, actual.TimeElapsed, "TimeElapsed key: " + locationKey);
      Assert.AreEqual(expected.TimeRemaining, actual.TimeRemaining, "TimeRemaining key: " + locationKey);
    }

    #endregion
  }
}
