﻿using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30Repository
  {
    public List<ForWebPlayerStat> GetPlayerStatsForWeb()
    {
      return _ctx.ForWebPlayerStats.ToList();
    }

    public DateTime GetPlayerStatsForWebDataGoodThru()
    {
      var maxGameData = _ctx.PlayerStatsGame
              .GroupBy(x => new { x.SeasonId })
              .Select(grp => new
              {
                SeasonId = grp.Key.SeasonId,
                GameId = grp.Max(x => x.GameId),
                GameDateTime = grp.Max(x => x.Game.GameDateTime)
              })
              .ToList();

      var gameDateTime = maxGameData.Where(x => x.SeasonId == currentSeasonId).FirstOrDefault().GameDateTime;

      return gameDateTime;
    }
  }
}