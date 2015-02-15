using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<GameOutcome> GetGameOutcomes(bool fullDetail);
    List<GameOutcome> GetGameOutcomesByGameId(int gameId, bool fullDetail);
    GameOutcome GetGameOutcomeByGameIdAndHomeTeam(int gameId, bool homeTeam, bool fullDetail);
    List<GameOutcome> GetGameOutcomesBySeasonTeamId(int seasonId, bool playoffs, int seasonTeamId, bool fullDetail);
  }
}
