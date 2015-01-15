using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<GoalieStatGame> GetGoalieStatsGame();
    List<GoalieStatGame> GetGoalieStatsGameByPlayerId(int playerId);
    List<GoalieStatGame> GetGoalieStatsGameByPlayerIdSeasonId(int playerId, int seasonId);

    GoalieStatGame GetGoalieStatGameByPlayerIdGameId(int playerId, int gameId);
  }
}
