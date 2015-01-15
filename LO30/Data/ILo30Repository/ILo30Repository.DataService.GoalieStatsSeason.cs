using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<GoalieStatSeason> GetGoalieStatsSeason();
    List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerId(int playerId);
    List<GoalieStatSeason> GetGoalieStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId);

    GoalieStatSeason GetGoalieStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool sub);
  }
}
