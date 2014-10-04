using System;
using LO30;
using LO30.Services;
using LO30.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LO30.UnitTests.Services
{
  [TestClass]
  public class PlayerStatsServiceUnitTests
  {
    [TestMethod]
    public void ProcessPlayerGameStatsIntoPlayerSeasonStats()
    {
      var playerStatsService = new PlayerStatsService();

      List<PlayerStatsGame> playerGameStats = new List<PlayerStatsGame>();

    }
  }
}
