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
    public List<ForWebTeamStanding> GetTeamStandingsForWeb()
    {
      return _ctx.ForWebTeamStandings.ToList();
    }

    public DateTime GetTeamStandingsForWebDataGoodThru()
    {
      var maxGameData = _ctx.GameOutcomes
              .GroupBy(x => new { x.GameTeam.SeasonTeam.SeasonId })
              .Select(grp => new
              {
                SeasonId = grp.Key.SeasonId,
                GameId = grp.Max(x => x.GameTeam.GameId),
                GameDateTime = grp.Max(x => x.GameTeam.Game.GameDateTime)
              })
              .ToList();

      var gameDateTime = maxGameData.Where(x => x.SeasonId == currentSeasonId).FirstOrDefault().GameDateTime;

      return gameDateTime;
    }
  }
}