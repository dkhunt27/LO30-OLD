using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<GameRoster> GetGameRosters();
    List<GameRoster> GetGameRostersByGameId(int gameId);
    List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam);
    GameRoster GetGameRosterByGameRosterId(int gameRosterId);
    GameRoster GetGameRosterByGameTeamIdAndPlayerNumber(int gameTeamId, string playerNumber);
  }
}
