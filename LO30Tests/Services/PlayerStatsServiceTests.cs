using LO30.Data.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Services.Tests
{
  [TestClass()]
  public class PlayerStatsServiceTests
  {
    PlayerStatsService _playerStatsService;
    private Lo30DataService _lo30DataService;

    [TestInitialize]
    public void Before()
    {
      _playerStatsService = new PlayerStatsService();
      _lo30DataService = new Lo30DataService();
    }

    #region ProcessScoreSheetEntriesIntoPlayerGameStats Tests
    [TestMethod()]
    public void ProcessScoreSheetEntriesIntoPlayerGameStats_NoScoreSheets_NoSubs()
    {
      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:false, pid: 401, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 101, pn: 2, line:1, pos:"F", g:false, pid: 402, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 101, pn: 3, line:1, pos:"F", g:false, pid: 403, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:301, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 102, pn: 1, line:1, pos:"F", g:false, pid: 404, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}},
        new GameRoster(grid: 1, gtid: 102, pn: 2, line:1, pos:"F", g:false, pid: 405, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}},
        new GameRoster(grid: 1, gtid: 102, pn: 3, line:1, pos:"F", g:false, pid: 406, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}}
      };

      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 402, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 404, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(expected, playerGameStats);
    }

    [TestMethod()]
    public void ProcessScoreSheetEntriesIntoPlayerGameStats_NoScoreSheets_WithSubs()
    {
      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:false, pid: 401, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 101, pn: 2, line:1, pos:"F", g:false, pid: 408, sub: true, sfpid: 402){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 101, pn: 3, line:1, pos:"F", g:false, pid: 403, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:301, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 102, pn: 1, line:1, pos:"F", g:false, pid: 407, sub: true, sfpid: 404){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}},
        new GameRoster(grid: 1, gtid: 102, pn: 2, line:1, pos:"F", g:false, pid: 405, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}},
        new GameRoster(grid: 1, gtid: 102, pn: 3, line:1, pos:"F", g:false, pid: 406, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}}
      };

      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
      };
      
      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 408, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 407, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(expected, playerGameStats);
    }

    [TestMethod()]
    public void ProcessScoreSheetEntriesIntoPlayerGameStats_OneScoreSheets_WithSubs()
    {
      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:false, pid: 401, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 101, pn: 2, line:1, pos:"F", g:false, pid: 408, sub: true, sfpid: 402){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 101, pn: 3, line:1, pos:"F", g:false, pid: 403, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:301, sid:501, tid:601)}},
        new GameRoster(grid: 1, gtid: 102, pn: 1, line:1, pos:"F", g:false, pid: 407, sub: true, sfpid: 404){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}},
        new GameRoster(grid: 1, gtid: 102, pn: 2, line:1, pos:"F", g:false, pid: 405, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}},
        new GameRoster(grid: 1, gtid: 102, pn: 3, line:1, pos:"F", g:false, pid: 406, sub: false, sfpid: null){GameTeam = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:302, sid:501, tid:602)}}
      };

      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 1, ht: true, time: "10.12", gpid: 401, a1pid: 408, a2pid: 403, a3pid: null, shg: true, ppg: false,  gwg: false),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 1, ht: true, time: "6.12", gpid: 401, a1pid: 403, a2pid: null, a3pid: null, shg: true, ppg: false,  gwg: false),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 2, ht: false, time: "12.22", gpid: 406, a1pid: null, a2pid: 405, a3pid: null, shg: false, ppg: true,  gwg: false),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 2, ht: false, time: "11.12", gpid: 406, a1pid: 405, a2pid: null, a3pid: null, shg: false, ppg: true,  gwg: false),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 701, per: 3, ht: true, time: "3.12", gpid: 403, a1pid: 401, a2pid: 408, a3pid: null, shg: false, ppg: false,  gwg: true)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:false, g: 2, a: 1, p: 3, ppg: 0, shg: 2, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 408, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:true, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, gid: 701, sid: 501, stidpf: 201, line:1, pos:"F", sub:false, g: 1, a: 2, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 407, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:false, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, gid: 701, sid: 501, stidpf: 202, line:1, pos:"F", sub:false, g: 2, a: 0, p: 2, ppg: 2, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(expected, playerGameStats);
    }

    [TestMethod()]
    public void ProcessScoreSheetEntriesIntoPlayerGameStats_SeasonId54GameId3227()
    {
      string folderPath = @"C:\git\LO30\LO30Tests\Data\SeasonId54GameId3227\";

      List<GameRoster> gameRosters = _lo30DataService.FromJsonFromFile<List<GameRoster>>(folderPath + "GameRosters.json");
      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = _lo30DataService.FromJsonFromFile<List<ScoreSheetEntryProcessed>>(folderPath + "ScoreSheetEntriesProcessed.json");
      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = _lo30DataService.FromJsonFromFile<List<ScoreSheetEntryPenaltyProcessed>>(folderPath + "ScoreSheetEntryPenaltiesProcessed.json");

      var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var playerGameStatsPartial = playerGameStats.Where(x=>x.PlayerId == 593 || x.PlayerId == 644 || x.PlayerId == 680).ToList();

      var expected = new List<PlayerStatGame>() 
      {
          new PlayerStatGame(pid: 593, gid: 3200, sid: 54, stidpf: 308, line:1, pos:"F", sub: false, g: 1, a: 2, p: 3, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3204, sid: 54, stidpf: 308, line:1, pos:"F", sub: false, g: 1, a: 0, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3208, sid: 54, stidpf: 308, line:1, pos:"F", sub: false, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3216, sid: 54, stidpf: 308, line:1, pos:"F", sub: false, g: 4, a: 3, p: 7, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3218, sid: 54, stidpf: 308, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3222, sid: 54, stidpf: 308, line:1, pos:"F", sub: false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 593, gid: 3227, sid: 54, stidpf: 308, line:1, pos:"F", sub: false, g: 1, a: 3, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3200, sid: 54, stidpf: 314, line:1, pos:"F", sub: false, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3205, sid: 54, stidpf: 314, line:1, pos:"F", sub: false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3207, sid: 54, stidpf: 314, line:1, pos:"F", sub: false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3213, sid: 54, stidpf: 309, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3214, sid: 54, stidpf: 313, line:1, pos:"F", sub: true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3215, sid: 54, stidpf: 314, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3216, sid: 54, stidpf: 309, line:1, pos:"F", sub: true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3218, sid: 54, stidpf: 315, line:1, pos:"F", sub: true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3219, sid: 54, stidpf: 314, line:1, pos:"F", sub: false, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 644, gid: 3227, sid: 54, stidpf: 314, line:1, pos:"F", sub: false, g: 1, a: 0, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 680, gid: 3204, sid: 54, stidpf: 308, line:1, pos:"F", sub: true, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
          new PlayerStatGame(pid: 680, gid: 3208, sid: 54, stidpf: 308, line:1, pos:"F", sub: true, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(expected, playerGameStatsPartial);
    }
    #endregion

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_OneTeam_OneGame()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, gid: 1, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2)
      };

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, stidpf:1, sid:1, line:1, pos:"F", sub:false, games:1, g:1,a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStats);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_OneTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, gid: 1, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 2, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 3, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 4, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 5, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 6, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 7, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, stidpf:1, sid: 1, line:1, pos:"F", sub:false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStats);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_ManyTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, gid: 1, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 2, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 3, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 4, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 5, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 6, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 7, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 1, gid: 8, sid: 1, stidpf: 4, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 9, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 10, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 11, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 12, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 13, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 14, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
      };

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, stidpf:1, sid:1, line:1, pos:"F", sub:false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:1, stidpf:2, sid:1, line:1, pos:"F", sub:true, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:1, stidpf:3, sid:1, line:1, pos:"F", sub:true, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:1, stidpf:4, sid:1, line:1, pos:"F", sub:true, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStats);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_ManyPlayer_ManyTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, gid: 1, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 2, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 3, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 4, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 5, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 6, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 7, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 1, gid: 8, sid: 1, stidpf: 4, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, gid: 9, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 10, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 11, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, gid: 12, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, gid: 13, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, gid: 14, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),

        new PlayerStatGame(pid: 2, gid: 1, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 2, gid: 2, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 3, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 4, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 2, gid: 5, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 2, gid: 6, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 7, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 2, gid: 8, sid: 1, stidpf: 4, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 2, gid: 9, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 10, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 11, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 2, gid: 12, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 2, gid: 13, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, gid: 14, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),

        new PlayerStatGame(pid: 3, gid: 1, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 3, gid: 2, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 3, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 4, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 3, gid: 5, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 3, gid: 6, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 7, sid: 1, stidpf: 1, line:1, pos:"F", sub: false, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 3, gid: 8, sid: 1, stidpf: 4, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 3, gid: 9, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 10, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 11, sid: 1, stidpf: 2, line:1, pos:"F", sub: true, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 3, gid: 12, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 3, gid: 13, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, gid: 14, sid: 1, stidpf: 3, line:1, pos:"F", sub: true, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, sid:1, stidpf:1, line:1, pos:"F", sub: false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:1, sid:1, stidpf:2, line:1, pos:"F", sub: true, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:1, sid:1, stidpf:3, line:1, pos:"F", sub: true, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:1, sid:1, stidpf:4, line:1, pos:"F", sub: true, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:2, sid:1, stidpf:1, line:1, pos:"F", sub: false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:2, sid:1, stidpf:2, line:1, pos:"F", sub: true, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:2, sid:1, stidpf:3, line:1, pos:"F", sub: true, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:2, sid:1, stidpf:4, line:1, pos:"F", sub: true, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:3, sid:1, stidpf:1, line:1, pos:"F", sub: false, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:3, sid:1, stidpf:2, line:1, pos:"F", sub: true, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:3, sid:1, stidpf:3, line:1, pos:"F", sub: true, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:3, sid:1, stidpf:4, line:1, pos:"F", sub: true, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStats);
    }


    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonTeamStats_SeasonId54GameId3227()
    {
      string folderPath = @"C:\git\LO30\LO30Tests\Data\SeasonId54GameId3227\";

      List<GameRoster> gameRosters = _lo30DataService.FromJsonFromFile<List<GameRoster>>(folderPath + "GameRosters.json");
      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = _lo30DataService.FromJsonFromFile<List<ScoreSheetEntryProcessed>>(folderPath + "ScoreSheetEntriesProcessed.json");
      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = _lo30DataService.FromJsonFromFile<List<ScoreSheetEntryPenaltyProcessed>>(folderPath + "ScoreSheetEntryPenaltiesProcessed.json");

      var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters);

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var playerSeasonTeamStatsPartial = playerSeasonTeamStats.Where(x => x.PlayerId == 593 || x.PlayerId == 644 || x.PlayerId == 680).ToList();

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid: 593, sid: 54, stidpf: 308, line:1, pos:"F", sub: false, games: 7, g: 8, a: 11, p: 19, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 644, sid: 54, stidpf: 309, line:1, pos:"F", sub: true, games: 2, g: 1, a: 1, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 644, sid: 54, stidpf: 313, line:1, pos:"F", sub: true, games: 1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 644, sid: 54, stidpf: 314, line:1, pos:"F", sub: false, games: 6, g: 1, a: 3, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 644, sid: 54, stidpf: 315, line:1, pos:"F", sub: true, games: 1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatSeasonTeam(pid: 680, sid: 54, stidpf: 308, line:1, pos:"F", sub: true, games: 2, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };


      AssertAreEqualPlayerStatSeasonTeamLists(expected, playerSeasonTeamStatsPartial);
    }

    [TestMethod()]
    public void ProcessGameOutcomesIntoGoalieGameStats_NoGameOutcomes_NoSubs()
    {
      GameTeam gt101 = new GameTeam(gid: 701, ht: true, stid: 201) { SeasonTeam = new SeasonTeam(stid: 201, sid: 501, tid: 601) };
      GameTeam gt102 = new GameTeam(gid: 701, ht: false, stid: 202) { SeasonTeam = new SeasonTeam(stid: 202, sid: 501, tid: 601) };

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:true, pid: 401, sub: false, sfpid: null){GameTeam = gt101},
        new GameRoster(grid: 1, gtid: 102, pn: 30, line:1, pos:"F", g:true, pid: 402, sub: false, sfpid: null){GameTeam = gt102},
      };

      List<GameOutcome> gameOutcomes = new List<GameOutcome>()
      {
      };

      var goalieGameStats = _playerStatsService.ProcessGameOutcomesIntoGoalieGameStats(gameOutcomes, gameRosters);

      var expected = new List<GoalieStatGame>() 
      {
        new GoalieStatGame(pid: 401, gid: 701, sid: 501, stidpf: 201, sub: false, ga: 0, so: 0, w: 0),
        new GoalieStatGame(pid: 402, gid: 701, sid: 501, stidpf: 202, sub: false, ga: 0, so: 0, w: 0)
      };

      AssertAreEqualGoalieStatGameLists(expected, goalieGameStats);
    }

    [TestMethod()]
    public void ProcessGameOutcomesIntoGoalieGameStats_NoGameOutcomes_WithSubs()
    {
      GameTeam gt101 = new GameTeam(gid: 701, ht: true, stid: 201) { SeasonTeam = new SeasonTeam(stid: 201, sid: 501, tid: 601) };
      GameTeam gt102 = new GameTeam(gid: 701, ht: false, stid: 202) { SeasonTeam = new SeasonTeam(stid: 202, sid: 501, tid: 601) };

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:true, pid: 401, sub: false, sfpid: null){GameTeam = gt101},
        new GameRoster(grid: 1, gtid: 102, pn: 30, line:1, pos:"F", g:true, pid: 408, sub: true, sfpid: 402){GameTeam = gt102},
      };

      List<GameOutcome> gameOutcomes = new List<GameOutcome>()
      {
      };


      var goalieGameStats = _playerStatsService.ProcessGameOutcomesIntoGoalieGameStats(gameOutcomes, gameRosters);

      var expected = new List<GoalieStatGame>() 
      {
        new GoalieStatGame(pid: 401, gid: 701, sid: 501, stidpf: 201, sub: false, ga: 0, so: 0, w: 0),
        new GoalieStatGame(pid: 408, gid: 701, sid: 501, stidpf: 202, sub: true, ga: 0, so: 0, w: 0)
      };

      AssertAreEqualGoalieStatGameLists(expected, goalieGameStats);
    }

    [TestMethod()]
    public void ProcessGameOutcomesIntoGoalieGameStats_OneScoreSheets_WithSubs()
    {
      GameTeam gt101 = new GameTeam(gid:701, ht: true, stid:201){SeasonTeam = new SeasonTeam(stid:201, sid:501, tid:601)};
      GameTeam gt102 = new GameTeam(gid:701, ht: false, stid:202){SeasonTeam = new SeasonTeam(stid:202, sid:501, tid:601)};

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(grid: 1, gtid: 101, pn: 1, line:1, pos:"F", g:true, pid: 401, sub: false, sfpid: null){GameTeam = gt101},
        new GameRoster(grid: 1, gtid: 102, pn: 30, line:1, pos:"F", g:true, pid: 408, sub: true, sfpid: 402){GameTeam = gt102},
      };

      List<GameOutcome> gameOutcomes = new List<GameOutcome>()
      {
        new GameOutcome(gtid: 101, res: "W", gf: 4, ga: 2, pim: 2, over: false){GameTeam = gt101},
        new GameOutcome(gtid: 102, res: "L", gf: 2, ga: 4, pim: 2, over: false){GameTeam = gt102}
      };

      var goalieGameStats = _playerStatsService.ProcessGameOutcomesIntoGoalieGameStats(gameOutcomes, gameRosters);

      var expected = new List<GoalieStatGame>() 
      {
        new GoalieStatGame(pid: 401, gid: 701, sid: 501, stidpf: 201, sub: false, ga: 2, so: 0, w: 1),
        new GoalieStatGame(pid: 408, gid: 701, sid: 501, stidpf: 202, sub: true, ga: 4, so: 0, w: 0)
      };

      AssertAreEqualGoalieStatGameLists(expected, goalieGameStats);
    }

    [TestMethod()]
    public void ProcessGameOutcomesIntoGoalieGameStats_SeasonId54GameId3227()
    {
      string folderPath = @"C:\git\LO30\LO30Tests\Data\SeasonId54GameId3227\";

      List<GameRoster> gameRosters = _lo30DataService.FromJsonFromFile<List<GameRoster>>(folderPath + "GameRosters.json");
      List<GameOutcome> gameOutcomes = _lo30DataService.FromJsonFromFile<List<GameOutcome>>(folderPath + "GameOutcomes.json");

      var goalieGameStats = _playerStatsService.ProcessGameOutcomesIntoGoalieGameStats(gameOutcomes, gameRosters);

      var goalieGameStatsPartial = goalieGameStats.Where(x => x.PlayerId == 634 || x.PlayerId == 619 || x.PlayerId == 81).ToList();

      var expected = new List<GoalieStatGame>() 
      {
        new GoalieStatGame(pid: 81, gid: 3201, sid: 54, stidpf: 311, sub: true, ga: 4, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3202, sid: 54, stidpf: 313, sub: false, ga: 3, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3206, sid: 54, stidpf: 313, sub: false, ga: 1, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3208, sid: 54, stidpf: 313, sub: false, ga: 2, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3214, sid: 54, stidpf: 313, sub: false, ga: 4, so: 0, w: 0),
        new GoalieStatGame(pid: 619, gid: 3221, sid: 54, stidpf: 313, sub: false, ga: 2, so: 0, w: 1),
        new GoalieStatGame(pid: 619, gid: 3225, sid: 54, stidpf: 313, sub: false, ga: 4, so: 0, w: 0),
        new GoalieStatGame(pid: 634, gid: 3203, sid: 54, stidpf: 315, sub: false, ga: 0, so: 1, w: 1),
        new GoalieStatGame(pid: 634, gid: 3209, sid: 54, stidpf: 315, sub: false, ga: 1, so: 0, w: 0),
        new GoalieStatGame(pid: 634, gid: 3211, sid: 54, stidpf: 315, sub: false, ga: 1, so: 0, w: 1),
        new GoalieStatGame(pid: 634, gid: 3214, sid: 54, stidpf: 315, sub: false, ga: 0, so: 1, w: 1),
        new GoalieStatGame(pid: 634, gid: 3218, sid: 54, stidpf: 315, sub: false, ga: 6, so: 0, w: 0),
        new GoalieStatGame(pid: 634, gid: 3224, sid: 54, stidpf: 315, sub: false, ga: 2, so: 0, w: 1),
        new GoalieStatGame(pid: 634, gid: 3226, sid: 54, stidpf: 315, sub: false, ga: 0, so: 1, w: 1)
      };

      AssertAreEqualGoalieStatGameLists(expected, goalieGameStatsPartial);
    }

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
      Assert.AreEqual(expected.SeasonTeamIdPlayingFor, actual.SeasonTeamIdPlayingFor, "SeasonTeamIdPlayingFor key: " + locationKey);
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
                                          x.SeasonTeamIdPlayingFor == expected[e].SeasonTeamIdPlayingFor)
                                .FirstOrDefault();

        var locationKey = string.Format("pid: {0}, stidpf: {1}",
                                    expected[e].PlayerId,
                                    expected[e].SeasonTeamIdPlayingFor);

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
                                          x.SeasonTeamIdPlayingFor == expected[e].SeasonTeamIdPlayingFor)
                                .FirstOrDefault();

        var locationKey = string.Format("pid: {0}, stidpf: {1}",
                                    expected[e].PlayerId,
                                    expected[e].SeasonTeamIdPlayingFor);

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
      Assert.AreEqual(expected.SeasonTeamIdPlayingFor, actual.SeasonTeamIdPlayingFor, "SeasonTeamIdPlayingFor key: " + locationKey);
      Assert.AreEqual(expected.Sub, actual.Sub, "Sub key: " + locationKey);
      Assert.AreEqual(expected.GoalsAgainst, actual.GoalsAgainst, "GoalsAgainst key: " + locationKey);
      Assert.AreEqual(expected.Shutouts, actual.Shutouts, "Shutouts key: " + locationKey);
      Assert.AreEqual(expected.Wins, actual.Wins, "Wins key: " + locationKey);
    }
    #endregion
  }
}
