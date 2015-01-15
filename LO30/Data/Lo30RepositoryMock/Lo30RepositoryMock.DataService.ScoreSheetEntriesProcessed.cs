using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    public List<ScoreSheetEntryProcessed> GetScoreSheetEntriesProcessed(bool fullDetail)
    {
      return _scoreSheetEntriesProcessed;
    }

    public List<ScoreSheetEntryProcessed> GetScoreSheetEntriesProcessedByGameId(int gameId, bool fullDetail)
    {
      return _scoreSheetEntriesProcessed.Where(x => x.GameId == gameId).ToList();
    }

  }
}