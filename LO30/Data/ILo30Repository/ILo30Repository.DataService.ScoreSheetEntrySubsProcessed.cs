using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<ScoreSheetEntrySubProcessed> GetScoreSheetEntrySubsProcessed(bool fullDetail);
    List<ScoreSheetEntrySubProcessed> GetScoreSheetEntrySubsProcessedByGameId(int gameId, bool fullDetail);
  }
}
