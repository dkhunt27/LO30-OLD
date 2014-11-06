using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class GameOutcomesController : ApiController
  {
    private ILo30Repository _repo;
    public GameOutcomesController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GameOutcome> GetGameOutcomes()
    {
      var results = _repo.GetGameOutcomes();
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }

    public List<GameOutcome> GetGameOutcomesByGameId(int gameId)
    {
      var results = _repo.GetGameOutcomesByGameId(gameId);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }

    public List<GameOutcome> GetGameOutcomesByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      var results = _repo.GetGameOutcomesByGameIdAndHomeTeam(gameId, homeTeam);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }
  }
}
