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
    public List<GameTeam> GetGameTeams()
    {
      return _ctx.GameTeams
                    .Include("Game")
                    .Include("Game.Season")

                    .Include("SeasonTeam")
                    .Include("SeasonTeam.Season")
                    .Include("SeasonTeam.Team")
                    .Include("SeasonTeam.Team.Coach")
                    .Include("SeasonTeam.Team.Sponsor")
                    .ToList();
    }

    public GameTeam GetGameTeamByGameTeamId(int gameTeamId)
    {
      return _contextService.FindGameTeam(gameTeamId);
    }

    public GameTeam GetGameTeamByGameIdAndHomeTeam(int gameTeamId, bool homeTeam)
    {
      return _contextService.FindGameTeamByPK2(gameTeamId, homeTeam);
    }
  }
}