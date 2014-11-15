using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<TeamRoster> GetTeamRosters();
    List<TeamRoster> GetTeamRostersBySeasonTeamIdAndYYYYMMDD(int seasonTeamId, int yyyymmdd);
    TeamRoster GetTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId(int seasonTeamId, int yyyymmdd, int playerId);
  }
}
