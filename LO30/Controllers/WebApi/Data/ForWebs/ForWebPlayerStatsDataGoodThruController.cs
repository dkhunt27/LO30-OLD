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

    public string GetDataGoodThru()
    {
      var results = _repo.GetPlayerStatsForWebDataGoodThru();
      return results.ToShortDateString();
    }
  }
}
