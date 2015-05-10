using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ForWebs
{
  public class ForWebTeamStandingsController : ApiController
  {
    private ILo30Repository _repo;
    public ForWebTeamStandingsController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<ForWebTeamStanding> GetForWebTeamStandings(int seasonId, bool playoffs)
    {
      var results = _repo.GetTeamStandingsForWeb(seasonId, playoffs);
      return results.OrderByDescending(x => x.PTS).ToList();
    }
  }
}
