using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameOutcomeController : ApiController
  {
    private ILo30Repository _repo;
    public GameOutcomeController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public GameOutcome GetGameOutcomeByGameIdAndHomeTeam(int gameId, bool homeTeam, bool fullDetail = true)
    {
      var results = _repo.GetGameOutcomeByGameIdAndHomeTeam(gameId, homeTeam, fullDetail);
      return results;
    }
  }
}
