using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ForWebs
{
  public class ForWebGoalieStatsController : ApiController
  {
    private ILo30Repository _repo;
    public ForWebGoalieStatsController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<ForWebGoalieStat> GetForWebGoalieStats()
    {
      var results = _repo.GetGoalieStatsForWeb();
      return results.OrderBy(x => x.GAA).ToList();
    }
  }
}
