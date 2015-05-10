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
    public List<ForWebPlayerStat> GetPlayerStatsForWeb(int seasonId, bool playoffs)
    {
      return _ctx.ForWebPlayerStats.Where(x => x.SID == seasonId && x.PFS == playoffs).ToList();
    }

    public DateTime GetPlayerStatsForWebDataGoodThru(int seasonId)
    {
      var maxGameData = _ctx.PlayerStatsGame
              .GroupBy(x => new { x.SeasonId, x.Playoffs })
              .Select(grp => new
              {
                SeasonId = grp.Key.SeasonId,
                Playoffs = grp.Key.Playoffs,
                GameId = grp.Max(x => x.GameId),
                GameDateTime = grp.Max(x => x.Game.GameDateTime)
              })
              .Where(x => x.SeasonId == seasonId)
              .OrderByDescending(x=>x.GameDateTime)
              .ToList();

      var gameDateTime = maxGameData.FirstOrDefault().GameDateTime;

      return gameDateTime;
    }
  }
}