﻿using LO30.Data.Objects;
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
    public List<ForWebGoalieStat> GetGoalieStatsForWeb()
    {
      return _webGoalieStats;
    }

    public DateTime GetGoalieStatsForWebDataGoodThru()
    {
      var maxGameData = _goalieStatsGame
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