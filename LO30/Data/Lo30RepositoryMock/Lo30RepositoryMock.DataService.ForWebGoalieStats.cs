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
    public List<ForWebGoalieStat> GetGoalieStatsForWeb(int seasonId, bool playoffs)
    {
      return _webGoalieStats.Where(x => x.SID == seasonId && x.PFS == playoffs).ToList();
    }

    public DateTime GetGoalieStatsForWebDataGoodThru(int seasonId)
    {
      var maxGameData = _goalieStatsGame
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