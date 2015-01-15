using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.PlayerStats
{
  public class PlayerStatCareerController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatCareerController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public PlayerStatCareer GetPlayerStatCareerByPlayerIdSub(int playerId, bool sub)
    {
      var results = _repo.GetPlayerStatCareerByPlayerIdSub(playerId, sub);
      return results;
    }

  }
}
