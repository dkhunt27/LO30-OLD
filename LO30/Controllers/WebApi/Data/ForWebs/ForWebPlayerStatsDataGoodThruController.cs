using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ForWebs
{
  public class ForWebPlayerStatsDataGoodThruController : ApiController
  {
    private ILo30Repository _repo;
    public ForWebPlayerStatsDataGoodThruController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public ForWebGoodThru GetDataGoodThru(int seasonId)
    {
      var results = _repo.GetPlayerStatsForWebDataGoodThru(seasonId);
      var goodThru = new ForWebGoodThru() { GT = results.ToShortDateString() };
      return goodThru;
    }
  }
}
