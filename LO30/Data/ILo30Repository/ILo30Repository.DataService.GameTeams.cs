using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<GameTeam> GetGameTeams();
    List<GameTeam> GetGameTeamsByGameId(int gameId);
    GameTeam GetGameTeamByGameTeamId(int gameTeamId);
    GameTeam GetGameTeamByGameIdAndHomeTeam(int gameId, bool homeTeam);
  }
}
