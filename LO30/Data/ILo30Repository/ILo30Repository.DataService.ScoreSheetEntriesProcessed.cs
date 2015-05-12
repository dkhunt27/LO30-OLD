using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<ScoreSheetEntryProcessed> GetScoreSheetEntriesProcessed(bool fullDetail);
    List<ScoreSheetEntryProcessed> GetScoreSheetEntriesProcessedByGameId(int gameId, bool fullDetail);
  }
}
