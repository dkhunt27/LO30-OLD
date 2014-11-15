using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class PlayerController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerController(ILo30Repository repo)
    {
      _repo = repo;
    }


    public Player GetPlayerByPlayerId(int playerId)
    {
      var results = _repo.GetPlayerByPlayerId(playerId);
      return results;
    }
  }
}
