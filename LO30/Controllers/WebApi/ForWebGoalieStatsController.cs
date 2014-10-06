﻿using LO30.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LO30.Controllers
{
  public class ForWebGoalieStatsController : ApiController
  {
    private ILo30Repository _repo;
    public ForWebGoalieStatsController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public IEnumerable<ForWebGoalieStat> Get()
    {
      List<ForWebGoalieStat> results = _repo.GetGoalieStatsForWeb();

      var goalieStats = results.OrderBy(x => x.GAA).ToList();

      return goalieStats;
    }
  }
}
