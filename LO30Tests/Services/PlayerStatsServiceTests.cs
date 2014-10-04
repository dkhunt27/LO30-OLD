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
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_OneTeam_OneGame()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid:1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2)
      };

      var playerSeasonStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonStats(playerGameStats);

      var expected = new List<PlayerStatSeason>() 
      {
        new PlayerStatSeason(sid:1, pid:1, pstid:1, stidpf:1, games:1, g:1,a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqual(playerSeasonStats, expected);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_OneTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonStats(playerGameStats);

      var expected = new List<PlayerStatSeason>() 
      {
        new PlayerStatSeason(sid:1,pid:1, pstid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10)
      };

      AssertAreEqual(playerSeasonStats, expected);
    }

    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_OnePlayer_ManyTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 4, gid: 8, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 2, gid: 9, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 2, gid: 10, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 2, gid: 11, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 3, gid: 12, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 3, gid: 13, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 3, gid: 14, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonStats(playerGameStats);

      var expected = new List<PlayerStatSeason>() 
      {
        new PlayerStatSeason(sid:1, pid:1, pstid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeason(sid:1, pid:1, pstid:2, stidpf:2, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeason(sid:1, pid:1, pstid:2, stidpf:3, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeason(sid:1, pid:1, pstid:2, stidpf:4, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqual(playerSeasonStats, expected);
    }


    [TestMethod()]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStatsTest_ManyPlayer_ManyTeam_ManyGames()
    {
      var playerGameStats = new List<PlayerStatGame>()
      {
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 4, gid: 8, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 2, gid: 9, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 2, gid: 10, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 2, gid: 11, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 3, gid: 12, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 3, gid: 13, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 1, pstid: 2, stidpf: 3, gid: 14, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),

        new PlayerStatGame(sid: 1, pid: 2, pstid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 2, stidpf: 4, gid: 8, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 2, stidpf: 2, gid: 9, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 2, stidpf: 2, gid: 10, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 2, stidpf: 2, gid: 11, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 2, stidpf: 3, gid: 12, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 2, stidpf: 3, gid: 13, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 2, pstid: 2, stidpf: 3, gid: 14, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),

        new PlayerStatGame(sid: 1, pid: 3, pstid: 1, stidpf: 1, gid: 1, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 1, stidpf: 1, gid: 2, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 1, stidpf: 1, gid: 3, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 1, stidpf: 1, gid: 4, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 1, stidpf: 1, gid: 5, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 1, stidpf: 1, gid: 6, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 1, stidpf: 1, gid: 7, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 2, stidpf: 4, gid: 8, g: 1, a: 1, p: 2, ppg: 1, shg: 0, gwg: 1, pim: 2),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 2, stidpf: 2, gid: 9, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 2, stidpf: 2, gid: 10, g: 3, a: 1, p: 4, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 2, stidpf: 2, gid: 11, g: 1, a: 1, p: 2, ppg: 0, shg: 1, gwg: 0, pim: 6),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 2, stidpf: 3, gid: 12, g: 2, a: 1, p: 3, ppg: 0, shg: 0, gwg: 1, pim: 0),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 2, stidpf: 3, gid: 13, g: 0, a: 1, p: 1, ppg: 0, shg: 0, gwg: 0, pim: 0),
        new PlayerStatGame(sid: 1, pid: 3, pstid: 2, stidpf: 3, gid: 14, g: 4, a: 1, p: 5, ppg: 2, shg: 0, gwg: 0, pim: 2)
      };

      var playerSeasonStats = _playerStatsService.ProcessPlayerGameStatsIntoPlayerSeasonStats(playerGameStats);

      var expected = new List<PlayerStatSeason>() 
      {
        new PlayerStatSeason(sid:1, pid:1, pstid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeason(sid:1, pid:1, pstid:2, stidpf:2, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeason(sid:1, pid:1, pstid:2, stidpf:3, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeason(sid:1, pid:1, pstid:2, stidpf:4, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2),
        new PlayerStatSeason(sid:1, pid:2, pstid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeason(sid:1, pid:2, pstid:2, stidpf:2, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeason(sid:1, pid:2, pstid:2, stidpf:3, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeason(sid:1, pid:2, pstid:2, stidpf:4, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2),
        new PlayerStatSeason(sid:1, pid:3, pstid:1, stidpf:1, games:7, g:11, a:7, p:18, ppg:3, shg:1, gwg:2, pim:10),
        new PlayerStatSeason(sid:1, pid:3, pstid:2, stidpf:2, games:3, g:4, a:3, p:7, ppg:0, shg:1, gwg:0, pim:6),
        new PlayerStatSeason(sid:1, pid:3, pstid:2, stidpf:3, games:3, g:6, a:3, p:9, ppg:2, shg:0, gwg:1, pim:2),
        new PlayerStatSeason(sid:1, pid:3, pstid:2, stidpf:4, games:1, g:1, a:1, p:2, ppg:1, shg:0, gwg:1, pim:2)
      };

      AssertAreEqual(playerSeasonStats, expected);
    }

    public void AssertAreEqual(List<PlayerStatSeason> actual, List<PlayerStatSeason> expected)
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
