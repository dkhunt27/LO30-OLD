using LO30.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LO30.Controllers
{
    public class AccessStandingsController : ApiController
  {
      private IAccessO30Repository _repo;
    public AccessStandingsController(IAccessO30Repository repo)
    {
      _repo = repo;
    }

    public IEnumerable<AccessO30Standing> Get()
    {
      IQueryable<AccessO30Standing> results = _repo.GetStandings();

      var standings = results.OrderByDescending(t => t.RegSeasonPoints)
                          .Take(25)
                          .ToList();

      return standings;
    }
  }
}
