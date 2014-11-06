using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class GameScoresController : ApiController
  {
    private ILo30Repository _repo;
    public GameScoresController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GameScore> GetGameScores()
    {
      var results = _repo.GetGameScores();
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }

    public List<GameScore> GetGameScoresByGameId(int gameId)
    {
      var results = _repo.GetGameScoresByGameId(gameId);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }

    public List<GameScore> GetGameScoresByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      var results = _repo.GetGameScoresByGameIdAndHomeTeam(gameId, homeTeam);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }
  }
}
