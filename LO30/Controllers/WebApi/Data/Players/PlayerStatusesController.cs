using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Players
{
  public class PlayerStatusesController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatusesController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<PlayerStatus> GetPlayerStatuses()
    {
      var results = _repo.GetPlayerStatuses();
      return results.ToList();
    }
  }
}
