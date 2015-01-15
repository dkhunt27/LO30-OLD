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
    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeam()
    {
      Expression<Func<GoalieStatSeasonTeam, bool>> whereClause = x => x.PlayerId > -1;
      return GetGoalieStatsSeasonTeamBase(whereClause);
    }

    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamByPlayerId(int playerId)
    {
      Expression<Func<GoalieStatSeasonTeam, bool>> whereClause = x => x.PlayerId == playerId;
      return GetGoalieStatsSeasonTeamBase(whereClause);
    }

    public List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamByPlayerIdSeasonId(int playerId, int seasonId)
    {
      Expression<Func<GoalieStatSeasonTeam, bool>> whereClause = x => x.PlayerId == playerId && x.SeasonId == seasonId;
      return GetGoalieStatsSeasonTeamBase(whereClause);
    }

    private List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamBase(Expression<Func<GoalieStatSeasonTeam, bool>> whereClause)
    {
      var results = _ctx.GoalieStatsSeasonTeam
                              .Include("Season")
                              .Include("Player")
                              .Include("SeasonTeam")
                              //.Include("SeasonTeam.Season")
                              .Include("SeasonTeam.Team")
                              .Include("SeasonTeam.Team.Coach")
                              .Include("SeasonTeam.Team.Sponsor")
                              .Where(whereClause)
                              .ToList();
      return results;
    }

    public GoalieStatSeasonTeam GetGoalieStatSeasonTeamByPlayerIdSeasonTeamId(int playerId, int seasonTeamId)
    {
      return _contextService.FindGoalieStatSeasonTeam(playerId, seasonTeamId, errorIfNotFound: false, errorIfMoreThanOneFound: true, populateFully: true);
    }
  }
}