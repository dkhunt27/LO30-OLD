using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<PlayerStatus> GetPlayerStatuses();
    List<PlayerStatus> GetPlayerStatusesByPlayerId(int playerId);
    PlayerStatus GetCurrentPlayerStatusByPlayerId(int playerId);
  }
}
