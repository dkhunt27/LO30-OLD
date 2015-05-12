using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<PlayerStatGame> GetPlayerStatsGame();
    List<PlayerStatGame> GetPlayerStatsGameByPlayerId(int playerId);
    List<PlayerStatGame> GetPlayerStatsGameByPlayerIdSeasonId(int playerId, int seasonId);

    PlayerStatGame GetPlayerStatGameByPlayerIdGameId(int playerId, int gameId);
  }
}
