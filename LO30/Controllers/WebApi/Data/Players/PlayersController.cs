using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Players
{
  public class PlayersController : ApiController
  {
    private ILo30Repository _repo;
    public PlayersController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<Player> GetPlayers()
    {
      var results = _repo.GetPlayers();
      return results.OrderBy(x => x.LastName)
                    .OrderBy(x => x.FirstName)
                    .ToList();
    }
  }
}
