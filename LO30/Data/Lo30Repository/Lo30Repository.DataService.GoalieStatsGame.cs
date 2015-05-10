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
    public List<GoalieStatGame> GetGoalieStatsGame()
    {
      Expression<Func<GoalieStatGame, bool>> whereClause = x => x.PlayerId > -1;
      return GetGoalieStatsGameBase(whereClause);
    }

    public List<GoalieStatGame> GetGoalieStatsGameByGameId(int gameId)
    {
      Expression<Func<GoalieStatGame, bool>> whereClause = x => x.GameId == gameId;
      return GetGoalieStatsGameBase(whereClause);
    }

    public List<GoalieStatGame> GetGoalieStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    {
      Expression<Func<GoalieStatGame, bool>> whereClause = x => x.PlayerId == playerId && x.SeasonId == seasonId;
      return GetGoalieStatsGameBase(whereClause);
    }

    private List<GoalieStatGame> GetGoalieStatsGameBase(Expression<Func<GoalieStatGame, bool>> whereClause)
    {
      var results = _ctx.GoalieStatsGame
                              .Include("Season")
                              .Include("Player")
                              .Include("Game")
                              //.Include("Game.Season")
                              .Include("SeasonTeam")
                              //.Include("SeasonTeam.Season")
                              .Include("SeasonTeam.Team")
                              .Include("SeasonTeam.Team.Coach")
                              .Include("SeasonTeam.Team.Sponsor")
                              .Where(whereClause)
                              .ToList();
      return results;
    }

    public GoalieStatGame GetGoalieStatGameByPlayerIdGameId(int playerId, int gameId)
    {
      return _contextService.FindGoalieStatGame(playerId, gameId, errorIfNotFound: false, errorIfMoreThanOneFound: true, populateFully: true);
    }
  }
}