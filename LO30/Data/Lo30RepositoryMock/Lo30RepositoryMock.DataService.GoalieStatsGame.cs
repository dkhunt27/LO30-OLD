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
    public List<GoalieStatGame> GetGoalieStatsGame()
    {
      return _goalieStatsGame.ToList();
    }

    public List<GoalieStatGame> GetGoalieStatsGameByPlayerId(int playerId)
    {
      return _goalieStatsGame.Where(x => x.PlayerId == playerId).ToList();
    }

    public List<GoalieStatGame> GetGoalieStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    {
      return _goalieStatsGame.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId).ToList();
    }

    public GoalieStatGame GetGoalieStatGameByPlayerIdGameId(int playerId, int gameId)
    {
      return _goalieStatsGame.Where(x => x.PlayerId == playerId && x.GameId == gameId).FirstOrDefault();
    }
  }
}