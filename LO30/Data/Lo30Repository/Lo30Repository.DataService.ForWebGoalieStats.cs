using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30Repository
  {
    public List<ForWebGoalieStat> GetGoalieStatsForWeb(int seasonId, bool playoffs)
    {
      return _ctx.ForWebGoalieStats.Where(x => x.SID == seasonId && x.PFS == playoffs).ToList();
    }

    public DateTime GetGoalieStatsForWebDataGoodThru(int seasonId)
    {
      var maxGameData = _ctx.GoalieStatsGame
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