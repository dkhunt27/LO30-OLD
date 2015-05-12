using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class TeamRostersController : ApiController
  {
    private ILo30Repository _repo;
    public TeamRostersController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<TeamRoster> GetTeamRosters()
    {
      var results = _repo.GetTeamRosters();
      return results.OrderByDescending(x => x.SeasonTeamId)
                    .OrderBy(x => x.StartYYYYMMDD)
                    .OrderBy(x => x.EndYYYYMMDD)
                    .OrderBy(x => x.PlayerNumber)
                    .ToList();
    }

    public List<TeamRoster> GetTeamRostersBySeasonTeamIdAndYYYYMMDD(int seasonTeamId, int yyyymmdd)
    {
      var results = _repo.GetTeamRostersBySeasonTeamIdAndYYYYMMDD(seasonTeamId, yyyymmdd);
      return results.OrderByDescending(x => x.SeasonTeamId)
                    .OrderBy(x => x.PlayerNumber)
                    .ToList();
    }

    public TeamRoster GetTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId(int seasonTeamId, int yyyymmdd, int playerId)
    {
      var results = _repo.GetTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId(seasonTeamId, playerId, yyyymmdd);
      return results;
    }
  }
}
