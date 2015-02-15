using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    public List<ForWebPlayerStat> GetPlayerStatsForWeb(int seasonId, bool playoffs)
    {
      return _webPlayerStats.Where(x => x.SID == seasonId && x.PFS == playoffs).ToList();
    }

    public DateTime GetPlayerStatsForWebDataGoodThru(int seasonId)
    {
      var maxGameData = _playerStatsGame
              .GroupBy(x => new { x.SeasonId, x.Playoffs })
              .Select(grp => new
              {
                SeasonId = grp.Key.SeasonId,
                Playoffs = grp.Key.Playoffs,
                GameId = grp.Max(x => x.GameId),
                GameDateTime = grp.Max(x => x.Game.GameDateTime)
              })
              .Where(x => x.SeasonId == seasonId)
              .ToList();

      var gameDateTime = maxGameData.FirstOrDefault().GameDateTime;

      return gameDateTime;
    }
  }
}