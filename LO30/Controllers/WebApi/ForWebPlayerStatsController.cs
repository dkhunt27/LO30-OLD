using LO30.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LO30.Controllers
{
  public class ForWebPlayerStatsController : ApiController
  {
    private ILo30Repository _repo;
    public ForWebPlayerStatsController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public IEnumerable<ForWebPlayerStat> Get()
    {
      List<ForWebPlayerStat> results = _repo.GetPlayerStatsForWeb();

      var playerStats = results.OrderByDescending(x => x.P).ToList();

      return playerStats;
    }
  }
}
