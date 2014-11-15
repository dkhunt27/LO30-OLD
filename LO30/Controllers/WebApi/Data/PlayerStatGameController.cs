using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class PlayerStatGameController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatGameController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public PlayerStatGame GetPlayerStatGameByPlayerIdGameId(int playerId, int gameId)
    {
      var results = _repo.GetPlayerStatGameByPlayerIdGameId(playerId, gameId);
      return results;
    }
  }
}
