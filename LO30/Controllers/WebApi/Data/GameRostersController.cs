using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class GameRostersController : ApiController
  {
    private ILo30Repository _repo;
    public GameRostersController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GameRoster> GetGameRosters()
    {
      var results = _repo.GetGameRosters();
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .OrderBy(x => x.Line)
                    .OrderByDescending(x => x.Position)
                    .OrderBy(x => x.RatingPrimary)
                    .OrderBy(x => x.RatingSecondary)
                    .ToList();
    }

    public List<GameRoster> GetGameRostersByGameId(int gameId)
    {
      var results = _repo.GetGameRostersByGameId(gameId);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .OrderBy(x=>x.Line)
                    .OrderByDescending(x=>x.Position)
                    .OrderBy(x=>x.RatingPrimary)
                    .OrderBy(x=>x.RatingSecondary)
                    .ToList();
    }

    public List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      var results = _repo.GetGameRostersByGameIdAndHomeTeam(gameId, homeTeam);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .OrderBy(x => x.Line)
                    .OrderByDescending(x => x.Position)
                    .OrderBy(x => x.RatingPrimary)
                    .OrderBy(x => x.RatingSecondary)
                    .ToList();
    }
  }
}
