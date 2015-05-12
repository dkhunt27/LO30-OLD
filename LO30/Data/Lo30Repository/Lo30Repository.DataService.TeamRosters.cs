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
    public List<TeamRoster> GetTeamRosters()
    {
      return _ctx.TeamRosters
                    .Include("SeasonTeam")
                    .Include("SeasonTeam.Season")
                    .Include("SeasonTeam.Team")
                    .Include("SeasonTeam.Team.Coach")
                    .Include("SeasonTeam.Team.Sponsor")

                    .Include("Player")
                    .ToList();
    }

    public List<TeamRoster> GetTeamRostersBySeasonTeamIdAndYYYYMMDD(int seasonTeamId, int yyyymmdd)
    {
      return GetTeamRosters().Where(x => x.SeasonTeamId == seasonTeamId &&
                                        x.StartYYYYMMDD <= yyyymmdd &&
                                        x.EndYYYYMMDD >= yyyymmdd
                                    ).ToList();
    }

    public TeamRoster GetTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId(int seasonTeamId, int yyyymmdd, int playerId)
    {
      return _contextService.FindTeamRosterWithYYYYMMDD(seasonTeamId, playerId, yyyymmdd);
    }
  }
}