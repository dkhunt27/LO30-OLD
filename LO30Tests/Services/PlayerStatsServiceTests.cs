using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LO30.Services;
using LO30.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LO30.Services.Tests
{
  [TestClass()]
  public class PlayerStatsServiceTests
  {
    PlayerStatsService _playerStatsService;

    [TestInitialize]
    public void Before()
    {
      _playerStatsService = new PlayerStatsService();
    }

    [TestMethod()]
    public void ProcessScoreSheetEntriesIntoPlayerGameStats_NoScoreSheets_NoSubs()
    {
      List<PlayerStatType> playerStatTypes = new List<PlayerStatType>()
      {
        new PlayerStatType(pstid:801, sub:false, name:"Rostered"),
        new PlayerStatType(pstid:802, sub:true, name:"Subbed")
      };

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(gid: 1, stid: 101, pn: 1, g:false, pid: 401, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 101, pn: 2, g:false, pid: 402, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 101, pn: 3, g:false, pid: 403, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 102, pn: 1, g:false, pid: 404, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)},
        new GameRoster(gid: 1, stid: 102, pn: 2, g:false, pid: 405, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)},
        new GameRoster(gid: 1, stid: 102, pn: 3, g:false, pid: 406, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)}
      };

      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters, playerStatTypes);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, pstid: 801, sid: 201, stidpf: 101, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 402, pstid: 801, sid: 201, stidpf: 101, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, pstid: 801, sid: 201, stidpf: 101, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 404, pstid: 801, sid: 201, stidpf: 102, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, pstid: 801, sid: 201, stidpf: 102, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, pstid: 801, sid: 201, stidpf: 102, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(playerGameStats, expected);
    }

    [TestMethod()]
    public void ProcessScoreSheetEntriesIntoPlayerGameStats_NoScoreSheets_WithSubs()
    {
      List<PlayerStatType> playerStatTypes = new List<PlayerStatType>()
      {
        new PlayerStatType(pstid:801, sub:false, name:"Rostered"),
        new PlayerStatType(pstid:802, sub:true, name:"Subbed")
      };

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(gid: 1, stid: 101, pn: 1, g:false, pid: 401, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 101, pn: 2, g:false, pid: 402, sub: true, sfpid: 408){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 101, pn: 3, g:false, pid: 403, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 102, pn: 1, g:false, pid: 404, sub: true, sfpid: 407){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)},
        new GameRoster(gid: 1, stid: 102, pn: 2, g:false, pid: 405, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)},
        new GameRoster(gid: 1, stid: 102, pn: 3, g:false, pid: 406, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)}
      };
      
      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
      };
      
      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters, playerStatTypes);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, pstid: 801, sid: 201, stidpf: 101, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 408, pstid: 802, sid: 201, stidpf: 101, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, pstid: 801, sid: 201, stidpf: 101, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 407, pstid: 802, sid: 201, stidpf: 102, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, pstid: 801, sid: 201, stidpf: 102, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, pstid: 801, sid: 201, stidpf: 102, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(playerGameStats, expected);
    }

    [TestMethod()]
    public void ProcessScoreSheetEntriesIntoPlayerGameStats_OneScoreSheets_WithSubs()
    {
      List<PlayerStatType> playerStatTypes = new List<PlayerStatType>()
      {
        new PlayerStatType(pstid:801, sub:false, name:"Rostered"),
        new PlayerStatType(pstid:802, sub:true, name:"Subbed")
      };

      List<GameRoster> gameRosters = new List<GameRoster>()
      {
        new GameRoster(gid: 1, stid: 101, pn: 1, g:false, pid: 401, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 101, pn: 2, g:false, pid: 402, sub: true, sfpid: 408){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 101, pn: 3, g:false, pid: 403, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:101, sid:201, tid:301)},
        new GameRoster(gid: 1, stid: 102, pn: 1, g:false, pid: 404, sub: true, sfpid: 407){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)},
        new GameRoster(gid: 1, stid: 102, pn: 2, g:false, pid: 405, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)},
        new GameRoster(gid: 1, stid: 102, pn: 3, g:false, pid: 406, sub: false, sfpid: null){SeasonTeam = new SeasonTeam(stid:102, sid:201, tid:302)}
      };

      List<ScoreSheetEntryProcessed> scoreSheetEntriesProcessed = new List<ScoreSheetEntryProcessed>()
      {
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 1, per: 1, ht: true, time: "10.12", gpid: 401, a1pid: 408, a2pid: 403, a3pid: null, shg: true, ppg: false,  gwg: false),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 1, per: 1, ht: true, time: "6.12", gpid: 401, a1pid: 403, a2pid: null, a3pid: null, shg: true, ppg: false,  gwg: false),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 1, per: 2, ht: false, time: "12.22", gpid: 406, a1pid: null, a2pid: 405, a3pid: null, shg: false, ppg: true,  gwg: false),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 1, per: 2, ht: false, time: "11.12", gpid: 406, a1pid: 405, a2pid: null, a3pid: null, shg: false, ppg: true,  gwg: false),
        new ScoreSheetEntryProcessed(sseid: 1001, gid: 1, per: 3, ht: true, time: "3.12", gpid: 403, a1pid: 401, a2pid: 408, a3pid: null, shg: false, ppg: false,  gwg: true)
      };

      List<ScoreSheetEntryPenaltyProcessed> scoreSheetEntryPenaltiesProcessed = new List<ScoreSheetEntryPenaltyProcessed>()
      {
      };

      var playerGameStats = _playerStatsService.ProcessScoreSheetEntriesIntoPlayerGameStats(scoreSheetEntriesProcessed, scoreSheetEntryPenaltiesProcessed, gameRosters, playerStatTypes);

      var expected = new List<PlayerStatGame>() 
      {
        new PlayerStatGame(pid: 401, pstid: 801, sid: 201, stidpf: 101, gid:1, g: 2, a: 1, p: 3, ppg: 0, shg: 2, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 408, pstid: 802, sid: 201, stidpf: 101, gid:1, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 403, pstid: 801, sid: 201, stidpf: 101, gid:1, g: 1, a: 2, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 407, pstid: 802, sid: 201, stidpf: 102, gid:1, g: 0, a: 0, p: 0, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 405, pstid: 801, sid: 201, stidpf: 102, gid:1, g: 0, a: 2, p: 2, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 406, pstid: 801, sid: 201, stidpf: 102, gid:1, g: 2, a: 0, p: 2, ppg: 2, shg: 0, gwg: 0, pim: 0)
      };

      AssertAreEqualPlayerStatGameLists(playerGameStats, expected);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_OneTeam_OneGame()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid:1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2)
      };

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, pstid:1, sid:1, stidpf:1, games:1, g:1,a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(playerSeasonTeamStats, expected);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_OneTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, pstid:1, sid: 1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(playerSeasonTeamStats, expected);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_ManyTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 4, gid: 8, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 2, gid: 9, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 2, gid: 10, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 2, gid: 11, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 3, gid: 12, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 3, gid: 13, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 3, gid: 14, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, pstid:1, sid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:1, pstid:2, sid:1, stidpf:2, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:1, pstid:2, sid:1, stidpf:3, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:1, pstid:2, sid:1, stidpf:4, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(playerSeasonTeamStats, expected);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_ManyPlayer_ManyTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 1, sid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 4, gid: 8, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 2, gid: 9, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 2, gid: 10, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 2, gid: 11, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 3, gid: 12, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 3, gid: 13, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 1, pstid: 2, sid: 1, stidpf: 3, gid: 14, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),

        new PlayerStatGame(pid: 2, pstid: 1, sid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 2, pstid: 1, sid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, pstid: 1, sid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, pstid: 1, sid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 2, pstid: 1, sid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 2, pstid: 1, sid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, pstid: 1, sid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 2, pstid: 2, sid: 1, stidpf: 4, gid: 8, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 2, pstid: 2, sid: 1, stidpf: 2, gid: 9, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, pstid: 2, sid: 1, stidpf: 2, gid: 10, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, pstid: 2, sid: 1, stidpf: 2, gid: 11, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 2, pstid: 2, sid: 1, stidpf: 3, gid: 12, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 2, pstid: 2, sid: 1, stidpf: 3, gid: 13, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 2, pstid: 2, sid: 1, stidpf: 3, gid: 14, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),

        new PlayerStatGame(pid: 3, pstid: 1, sid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 3, pstid: 1, sid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, pstid: 1, sid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, pstid: 1, sid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 3, pstid: 1, sid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 3, pstid: 1, sid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, pstid: 1, sid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(pid: 3, pstid: 2, sid: 1, stidpf: 4, gid: 8, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(pid: 3, pstid: 2, sid: 1, stidpf: 2, gid: 9, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, pstid: 2, sid: 1, stidpf: 2, gid: 10, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, pstid: 2, sid: 1, stidpf: 2, gid: 11, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(pid: 3, pstid: 2, sid: 1, stidpf: 3, gid: 12, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(pid: 3, pstid: 2, sid: 1, stidpf: 3, gid: 13, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(pid: 3, pstid: 2, sid: 1, stidpf: 3, gid: 14, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonTeamStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonTeamStats(playerGameStats);

      var expected = new List<PlayerStatSeasonTeam>() 
      {
        new PlayerStatSeasonTeam(pid:1, pstid:1, sid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:1, pstid:2, sid:1, stidpf:2, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:1, pstid:2, sid:1, stidpf:3, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:1, pstid:2, sid:1, stidpf:4, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:2, pstid:1, sid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:2, pstid:2, sid:1, stidpf:2, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:2, pstid:2, sid:1, stidpf:3, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:2, pstid:2, sid:1, stidpf:4, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:3, pstid:1, sid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeasonTeam(pid:3, pstid:2, sid:1, stidpf:2, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeasonTeam(pid:3, pstid:2, sid:1, stidpf:3, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeasonTeam(pid:3, pstid:2, sid:1, stidpf:4, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqualPlayerStatSeasonTeamLists(playerSeasonTeamStats, expected);
    }

    private void AssertAreEqualPlayerStatGameLists(List<PlayerStatGame> actual, List<PlayerStatGame> expected)
    {

      Assert.AreEqual(expected.Count, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.SeasonId == expected[e].SeasonId &&
                                          x.PlayerId == expected[e].PlayerId &&
                                          x.PlayerStatTypeId == expected[e].PlayerStatTypeId &&
                                          x.SeasonTeamIdPlayingFor == expected[e].SeasonTeamIdPlayingFor &&
                                          x.GameId == expected[e].GameId)
                                .FirstOrDefault();

        var locationKey = string.Format("sid: {0}, pid: {1}, pstid: {2}, stidpf: {3}, gid: {4}",
                                    expected[e].SeasonId,
                                    expected[e].PlayerId,
                                    expected[e].PlayerStatTypeId,
                                    expected[e].SeasonTeamIdPlayingFor,
                                    expected[e].GameId);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        Assert.AreEqual(expected[e].Goals, actualMatch.Goals, "Goals key: " + locationKey);
        Assert.AreEqual(expected[e].Assists, actualMatch.Assists, "Assists key: " + locationKey);
        Assert.AreEqual(expected[e].Points, actualMatch.Points, "Points key: " + locationKey);
        Assert.AreEqual(expected[e].PowerPlayGoals, actualMatch.PowerPlayGoals, "PowerPlayGoals key: " + locationKey);
        Assert.AreEqual(expected[e].ShortHandedGoals, actualMatch.ShortHandedGoals, "ShortHandedGoals key: " + locationKey);
        Assert.AreEqual(expected[e].GameWinningGoals, actualMatch.GameWinningGoals, "GameWinningGoals key: " + locationKey);
        Assert.AreEqual(expected[e].PenaltyMinutes, actualMatch.PenaltyMinutes, "PenaltyMinutes key: " + locationKey);
      }
    }

    private void AssertAreEqualPlayerStatSeasonTeamLists(List<PlayerStatSeasonTeam> actual, List<PlayerStatSeasonTeam> expected)
    {

      Assert.AreEqual(expected.Count, actual.Count, "Count");
      for (var e = 0; e < expected.Count; e++)
      {
        var actualMatch = actual.Where(x => x.SeasonId == expected[e].SeasonId &&
                                          x.PlayerId == expected[e].PlayerId &&
                                          x.PlayerStatTypeId == expected[e].PlayerStatTypeId &&
                                          x.SeasonTeamIdPlayingFor == expected[e].SeasonTeamIdPlayingFor)
                                .FirstOrDefault();

        var locationKey = string.Format("sid: {0}, pid: {1}, pstid: {2}, stidpf: {3}",
                                    expected[e].SeasonId,
                                    expected[e].PlayerId,
                                    expected[e].PlayerStatTypeId,
                                    expected[e].SeasonTeamIdPlayingFor);

        Assert.IsNotNull(actualMatch, "actualMatch key: " + locationKey);
        Assert.AreEqual(expected[e].Games, actualMatch.Games, "Games key: " + locationKey);
        Assert.AreEqual(expected[e].Goals, actualMatch.Goals, "Goals key: " + locationKey);
        Assert.AreEqual(expected[e].Assists, actualMatch.Assists, "Assists key: " + locationKey);
        Assert.AreEqual(expected[e].Points, actualMatch.Points, "Points key: " + locationKey);
        Assert.AreEqual(expected[e].PowerPlayGoals, actualMatch.PowerPlayGoals, "PowerPlayGoals key: " + locationKey);
        Assert.AreEqual(expected[e].ShortHandedGoals, actualMatch.ShortHandedGoals, "ShortHandedGoals key: " + locationKey);
        Assert.AreEqual(expected[e].GameWinningGoals, actualMatch.GameWinningGoals, "GameWinningGoals key: " + locationKey);
        Assert.AreEqual(expected[e].PenaltyMinutes, actualMatch.PenaltyMinutes, "PenaltyMinutes key: " + locationKey);
      }
    }

  }
}
