using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class GamesController : ApiController
  {
    private ILo30Repository _repo;
    public GamesController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<Game> GetGames()
    {
      var results = _repo.GetGames();
      return results.OrderByDescending(x => x.GameId).ToList();
    }

    public Game GetGamesByGameId(int gameId)
    {
      var result = _repo.GetGameByGameId(gameId);
      return result;
    }
  }
}
