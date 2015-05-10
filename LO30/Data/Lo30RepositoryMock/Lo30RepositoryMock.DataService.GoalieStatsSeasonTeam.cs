using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeam()
    {
      return _goalieStatsSeasonTeam.ToList();
    }

    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamByPlayerId(int playerId)
    {
      return _goalieStatsSeasonTeam.Where(x => x.PlayerId == playerId).ToList();
    }

    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamByPlayerIdSeasonId(int playerId, int seasonId)
    {
      return _goalieStatsSeasonTeam.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId).ToList();
    }

    public GoalieStatSeasonTeam GetGoalieStatSeasonTeamByPlayerIdSeasonTeamId(int playerId, int seasonTeamId, bool playoffs)
    {
      return _goalieStatsSeasonTeam.Where(x => x.PlayerId == playerId && x.SeasonTeamId == seasonTeamId && x.Playoffs == playoffs).FirstOrDefault();
    }
  }
}