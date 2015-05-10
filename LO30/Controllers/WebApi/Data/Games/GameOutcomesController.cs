using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameOutcomesController : ApiController
  {
    private ILo30Repository _repo;
    public GameOutcomesController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GameOutcome> GetGameOutcomes(bool fullDetail = true)
    {
      var results = _repo.GetGameOutcomes(fullDetail);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }

    public List<GameOutcome> GetGameOutcomesByGameId(int gameId, bool fullDetail = true)
    {
      var results = _repo.GetGameOutcomesByGameId(gameId, fullDetail);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }

  }
}
