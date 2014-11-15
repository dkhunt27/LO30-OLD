using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class PlayerStatsSeasonTeamController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatsSeasonTeamController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeam()
    {
      var results = _repo.GetPlayerStatsSeasonTeam();
      return results.ToList();
    }

    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamByPlayerId(int playerId)
    {
      var results = _repo.GetPlayerStatsSeasonTeamByPlayerId(playerId);
      return results;
    }

    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = _repo.GetPlayerStatsSeasonTeamByPlayerIdSeasonId(playerId, seasonId);
      return results;
    }
  }
}
