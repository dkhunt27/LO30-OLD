using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class GameTeamController : ApiController
  {
    private ILo30Repository _repo;
    public GameTeamController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public GameTeam GetGameTeamByGameTeamId(int gameTeamId)
    {
      var results = _repo.GetGameTeamByGameTeamId(gameTeamId);
      return results;
    }

    public GameTeam GetGameTeamByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      var results = _repo.GetGameTeamByGameIdAndHomeTeam(gameId, homeTeam);
      return results;
    }
  }
}
