using LO30.Data;
using LO30.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatCareerController : ApiController
  {
    private ILo30Repository _repo;
    public GoalieStatCareerController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public PlayerStatCareer GetGoalieStatCareerByPlayerIdSub(int playerId, bool sub)
    {
      throw new NotImplementedException();
    }

  }
}
