using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatSeasonController : ApiController
  {
    private ILo30Repository _repo;
    public GoalieStatSeasonController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public GoalieStatSeason GetGoalieStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool playoffs, bool sub)
    {
      var results = _repo.GetGoalieStatSeasonByPlayerIdSeasonIdSub(playerId, seasonId, playoffs, sub);
      return results;
    }
  }
}
