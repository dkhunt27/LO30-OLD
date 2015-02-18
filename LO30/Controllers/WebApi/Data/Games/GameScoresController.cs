using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameScoresController : ApiController
  {
    private ILo30Repository _repo;
    public GameScoresController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GameScore> GetGameScores(bool fullDetail = true)
    {
      var results = _repo.GetGameScores(fullDetail);
      return results.OrderByDescending(x => x.GameScoreId)
                    .ToList();
    }

    public List<GameScore> GetGameScoresByGameId(int gameId, bool fullDetail = true)
    {
      var results = _repo.GetGameScoresByGameId(gameId, fullDetail);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }

    public List<GameScore> GetGameScoresByGameIdAndHomeTeam(int gameId, bool homeTeam, bool fullDetail = true)
    {
      var results = _repo.GetGameScoresByGameIdAndHomeTeam(gameId, homeTeam, fullDetail);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }
  }
}
