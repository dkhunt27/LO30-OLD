using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers
{
  public class GamesController : ApiController
  {
    private ILo30Repository _repo;
    public GamesController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<Game> Get()
    {
      var results = _repo.GetGames();
      return results.OrderByDescending(x => x.GameId).ToList();
    }

    public Game Get(int id)
    {
      var result = _repo.GetGameByGameId(id);
      return result;
    }
  }
}
