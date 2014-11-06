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
    public List<GameOutcome> GetGameOutcomes()
    {
      return _ctx.GameOutcomes
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .ToList();
    }

    public List<GameOutcome> GetGameOutcomesByGameId(int gameId)
    {
      return GetGameOutcomes().Where(x => x.GameTeam.GameId == gameId).ToList();
    }

    public List<GameOutcome> GetGameOutcomesByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return GetGameOutcomes().Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam).ToList();
    }

  }
}