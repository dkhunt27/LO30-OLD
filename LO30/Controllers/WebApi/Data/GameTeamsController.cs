using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
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
