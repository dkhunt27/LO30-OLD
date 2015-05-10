using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<ScoreSheetEntryPenaltyProcessed> GetScoreSheetEntryPenaltiesProcessed(bool fullDetail);
    List<ScoreSheetEntryPenaltyProcessed> GetScoreSheetEntryPenaltiesProcessedByGameId(int gameId, bool fullDetail);
  }
}
