using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace LO30.Data
{
  public partial class Lo30Repository
  {
    public List<GoalieStatSeason> GetGoalieStatsSeason()
    {
      Expression<Func<GoalieStatSeason, bool>> whereClause = x => x.PlayerId > -1;
      return GetGoalieStatsSeasonBase(whereClause);
    }

    public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerId(int playerId)
    {
      Expression<Func<GoalieStatSeason, bool>> whereClause = x => x.PlayerId == playerId;
      return GetGoalieStatsSeasonBase(whereClause);
    }

    public List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      Expression<Func<GoalieStatSeason, bool>> whereClause = x => x.PlayerId == playerId && x.SeasonId == seasonId;
      return GetGoalieStatsSeasonBase(whereClause);
    }

    private List<GoalieStatSeason> GetGoalieStatsSeasonBase(Expression<Func<GoalieStatSeason, bool>> whereClause)
    {
      var results = _ctx.GoalieStatsSeason
                              .Include("Player")
                              .Include("Season")
                              .Where(whereClause)
                              .ToList();
      return results;
    }

    public GoalieStatSeason GetGoalieStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool playoffs, bool sub)
    {
      return _contextService.FindGoalieStatSeason(playerId, seasonId, playoffs, sub, errorIfNotFound: false, errorIfMoreThanOneFound: true, populateFully: true);
    }
  }
}