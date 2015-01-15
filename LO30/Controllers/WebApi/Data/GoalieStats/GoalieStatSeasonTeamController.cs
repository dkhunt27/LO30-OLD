using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatSeasonTeamController : ApiController
  {
    private ILo30Repository _repo;
    public GoalieStatSeasonTeamController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public GoalieStatSeasonTeam GetGoalieStatSeasonTeamByPlayerIdSeasonTeamId(int playerId, int seasonTeamId)
    {
      var results = _repo.GetGoalieStatSeasonTeamByPlayerIdSeasonTeamId(playerId, seasonTeamId);
      return results;
    }
  }
}
