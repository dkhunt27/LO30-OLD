using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsSeasonController : ApiController
  {
    private ILo30Repository _repo;
    public GoalieStatsSeasonController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GoalieStatSeason> GetGoalieStatsSeason()
    {
      var results = _repo.GetGoalieStatsSeason();
      return results.ToList();
    }

    public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerId(int playerId)
    {
      var results = _repo.GetGoalieStatsSeasonByPlayerId(playerId);
      return results;
    }

    public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = _repo.GetGoalieStatsSeasonByPlayerIdSeasonId(playerId, seasonId);
      return results;
    }
  }
}
