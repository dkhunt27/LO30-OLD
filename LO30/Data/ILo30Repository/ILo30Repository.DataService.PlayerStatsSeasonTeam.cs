using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeam();
    List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamByPlayerId(int playerId);
    List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamByPlayerIdSeasonId(int playerId, int seasonId);

    PlayerStatSeasonTeam GetPlayerStatSeasonTeamByPlayerIdSeasonTeamId(int playerId, int seasonTeamId, bool playoffs);
  }
}
