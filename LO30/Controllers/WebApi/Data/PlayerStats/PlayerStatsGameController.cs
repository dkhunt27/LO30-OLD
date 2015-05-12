using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsGameController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatsGameController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<PlayerStatGame> GetPlayerStatsGame()
    {
      var results = _repo.GetPlayerStatsGame();
      return results.ToList();
    }

    public List<PlayerStatGame> GetPlayerStatsGameByPlayerId(int playerId)
    {
      var results = _repo.GetPlayerStatsGameByPlayerId(playerId);
      return results;
    }

    public List<PlayerStatGame> GetPlayerStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = _repo.GetPlayerStatsGameByPlayerIdSeasonId(playerId, seasonId);
      return results;
    }
  }
}
