using LO30.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LO30.Controllers
{
  public class PlayerStatsSeasonController : ApiController
  {
    private ILo30Repository _repo;
    public PlayerStatsSeasonController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public IEnumerable<PlayerStatSeason> Get()
    {
      IQueryable<PlayerStatSeason> results = _repo.GetPlayerStatsSeason();

      var playerStats = results.OrderByDescending(x => x.Points).ToList();

      return playerStats;
    }
  }
}
