using LO30.Data;
using LO30.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsCareerController : ApiController
  {
    private ILo30Repository _repo;
    public GoalieStatsCareerController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<PlayerStatCareer> GetGoalieStatsCareer()
    {
      throw new NotImplementedException();
    }

    public List<PlayerStatCareer> GetGoalieStatsCareerByPlayerId(int playerId)
    {
      throw new NotImplementedException();
    }
  }
}
