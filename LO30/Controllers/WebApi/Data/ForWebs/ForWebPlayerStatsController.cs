using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ForWebs
{
  public class ForWebPlayerStatsController : ApiController
  {
    private ILo30Repository _repo;
    public ForWebPlayerStatsController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<ForWebPlayerStat> GetForWebPlayerStats(int seasonId, bool playoffs)
    {
      var results = _repo.GetPlayerStatsForWeb(seasonId, playoffs);
      return results.OrderByDescending(x => x.P).ToList();
    }
  }
}
