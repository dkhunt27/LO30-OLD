using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data
{
  public class PlayersSubSearchController : ApiController
  {
    private ILo30Repository _repo;
    public PlayersSubSearchController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<PlayerSubSearch> GetPlayersSubSearch(string position, string ratingMin, string ratingMax)
    {
      var results = _repo.GetPlayersSubSearch(position, ratingMin, ratingMax);
      return results.OrderBy(x => x.RatingPrimary)
                    .OrderBy(x => x.RatingSecondary)
                    .ToList();
    }

  }
}
