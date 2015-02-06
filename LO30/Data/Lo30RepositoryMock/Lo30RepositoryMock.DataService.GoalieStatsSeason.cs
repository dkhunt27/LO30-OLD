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
    public List<GoalieStatSeason> GetGoalieStatsSeason()
    {
      return _goalieStatsSeason.ToList();
    }

    public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerId(int playerId)
    {
      return _goalieStatsSeason.Where(x => x.PlayerId == playerId).ToList();
    }

    public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      return _goalieStatsSeason.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId).ToList();
    }

    public GoalieStatSeason GetGoalieStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool playoffs, bool sub)
    {
      return _goalieStatsSeason.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId && x.Playoffs == playoffs && x.Sub == sub).FirstOrDefault();
    }
  }
}