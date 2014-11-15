using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class PlayerStatsSeasonController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatsSeasonController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<PlayerStatSeason> GetPlayerStatsSeason()
    {
      var results = _repo.GetPlayerStatsSeason();
      return results.ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerId(int playerId)
    {
      var results = _repo.GetPlayerStatsSeasonByPlayerId(playerId);
      return results;
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = _repo.GetPlayerStatsSeasonByPlayerIdSeasonId(playerId, seasonId);
      return results;
    }
  }
}
