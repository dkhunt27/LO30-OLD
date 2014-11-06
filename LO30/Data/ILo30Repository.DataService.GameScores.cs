using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<GameScore> GetGameScores();
    List<GameScore> GetGameScoresByGameId(int gameId);
    List<GameScore> GetGameScoresByGameIdAndHomeTeam(int gameId, bool homeTeam);
  }
}
