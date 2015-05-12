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
    public List<TeamRoster> GetTeamRosters()
    {
      return _teamRosters;
    }

    public List<TeamRoster> GetTeamRostersBySeasonTeamIdAndYYYYMMDD(int seasonTeamId, int yyyymmdd)
    {
      return _teamRosters.Where(x => x.SeasonTeamId == seasonTeamId &&
                                    x.StartYYYYMMDD <= yyyymmdd &&
                                    x.EndYYYYMMDD >= yyyymmdd
                                ).ToList();
    }

    public TeamRoster GetTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId(int seasonTeamId, int yyyymmdd, int playerId)
    {
      return _teamRosters.Where(x => x.SeasonTeamId == seasonTeamId && 
                                    x.PlayerId == playerId &&
                                    x.StartYYYYMMDD <= yyyymmdd &&
                                    x.EndYYYYMMDD >= yyyymmdd
                                ).FirstOrDefault();
    }

  }
}