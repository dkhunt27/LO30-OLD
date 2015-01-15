using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.GoalieStats
{
  public class GoalieStatsSeasonTeamController : ApiController
  {
    private ILo30Repository _repo;
    public GoalieStatsSeasonTeamController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeam()
    {
      var results = _repo.GetGoalieStatsSeasonTeam();
      return results.ToList();
    }

    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamByPlayerId(int playerId)
    {
      var results = _repo.GetGoalieStatsSeasonTeamByPlayerId(playerId);
      return results;
    }

    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamByPlayerIdSeasonId(int playerId, int seasonId)
    {
      var results = _repo.GetGoalieStatsSeasonTeamByPlayerIdSeasonId(playerId, seasonId);
      return results;
    }
  }
}
