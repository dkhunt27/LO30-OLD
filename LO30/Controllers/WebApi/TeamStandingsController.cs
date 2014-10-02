using LO30.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LO30.Controllers
{
  public class TeamStandingsController : ApiController
  {
    private ILo30Repository _repo;
    public TeamStandingsController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public IEnumerable<TeamStanding> Get()
    {
      IQueryable<TeamStanding> results = _repo.GetTeamStandings();

      var standings = results.OrderBy(t => t.Rank).ToList();

      return standings;
    }
  }
}
