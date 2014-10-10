using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers
{
  public class ForWebPlayerStatsController : ApiController
  {
    private ILo30Repository _repo;
    public ForWebPlayerStatsController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public IEnumerable<ForWebPlayerStat> Get()
    {
      IQueryable<ForWebPlayerStat> results = _repo.GetPlayerStatsForWeb();

      var playerStats = results.OrderByDescending(x => x.P).ToList();

      return playerStats;
    }
  }
}
