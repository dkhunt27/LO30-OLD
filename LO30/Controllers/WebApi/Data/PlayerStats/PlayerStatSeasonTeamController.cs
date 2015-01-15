using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatSeasonTeamController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatSeasonTeamController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public PlayerStatSeasonTeam GetPlayerStatSeasonTeamByPlayerIdSeasonTeamId(int playerId, int seasonTeamId)
    {
      var results = _repo.GetPlayerStatSeasonTeamByPlayerIdSeasonTeamId(playerId, seasonTeamId);
      return results;
    }
  }
}
