using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<PlayerStatSeason> GetPlayerStatsSeason();
    List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerId(int playerId);
    List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId);

    PlayerStatSeason GetPlayerStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool playoffs, bool sub);
  }
}
