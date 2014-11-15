using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class PlayerStatSeasonController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatSeasonController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public PlayerStatSeason GetPlayerStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool sub)
    {
      var results = _repo.GetPlayerStatSeasonByPlayerIdSeasonIdSub(playerId, seasonId, sub);
      return results;
    }
  }
}
