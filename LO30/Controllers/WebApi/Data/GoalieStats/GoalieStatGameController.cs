using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatGameController : ApiController
  {
    private ILo30Repository _repo;
    public GoalieStatGameController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public GoalieStatGame GetGoalieStatGameByPlayerIdGameId(int playerId, int gameId)
    {
      var results = _repo.GetGoalieStatGameByPlayerIdGameId(playerId, gameId);
      return results;
    }
  }
}
