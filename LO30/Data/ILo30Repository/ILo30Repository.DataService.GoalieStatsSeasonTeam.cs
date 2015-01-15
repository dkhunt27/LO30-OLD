using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeam();
    List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamByPlayerId(int playerId);
    List<GoalieStatSeasonTeam> GetGoalieStatsSeasonTeamByPlayerIdSeasonId(int playerId, int seasonId);

    GoalieStatSeasonTeam GetGoalieStatSeasonTeamByPlayerIdSeasonTeamId(int playerId, int seasonTeamId);
  }
}
