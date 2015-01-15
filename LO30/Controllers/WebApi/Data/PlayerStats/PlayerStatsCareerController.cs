using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatsCareerController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatsCareerController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<PlayerStatCareer> GetPlayerStatsCareer()
    {
      var results = _repo.GetPlayerStatsCareer();
      return results.ToList();
    }

    public List<PlayerStatCareer> GetPlayerStatsCareerByPlayerId(int playerId)
    {
      var results = _repo.GetPlayerStatsCareerByPlayerId(playerId);
      return results;
    }
  }
}
