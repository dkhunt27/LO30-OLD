using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameTeamsController : ApiController
  {
    private ILo30Repository _repo;
    public GameTeamsController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GameTeam> GetGameTeams()
    {
      var results = _repo.GetGameTeams();
      return results.OrderByDescending(x => x.GameId)
                    .ToList();
    }

    public List<GameTeam> GetGameTeamsByGameId(int gameId)
    {
      var results = _repo.GetGameTeamsByGameId(gameId);
      return results;

    }
  }
}
