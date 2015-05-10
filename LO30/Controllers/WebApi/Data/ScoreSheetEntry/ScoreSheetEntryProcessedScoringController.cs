using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ScoreSheetEntry
{
  public class ScoreSheetEntryProcessedScoringController : ApiController
  {
    private ILo30Repository _repo;
    public ScoreSheetEntryProcessedScoringController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<ScoreSheetEntryProcessed> GetScoreSheetEntriesProcessed(bool fullDetail = true)
    {
      var results = _repo.GetScoreSheetEntriesProcessed(fullDetail);
      return results.OrderBy(x => x.GameId)
                    .OrderBy(x => x.Period)
                    .OrderByDescending(x => x.TimeRemaining)
                    .ToList();
    }

    public List<ScoreSheetEntryProcessed> GetScoreSheetEntriesProcessedByGameId(int gameId, bool fullDetail = true)
    {
      var results = _repo.GetScoreSheetEntriesProcessedByGameId(gameId, fullDetail);
      return results.OrderBy(x => x.GameId)
                    .ThenBy(x => x.Period)
                    .ThenByDescending(x => x.TimeRemaining)
                    .ToList();
    }
  }
}
