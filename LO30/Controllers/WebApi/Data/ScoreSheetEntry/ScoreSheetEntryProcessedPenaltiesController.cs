using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.ScoreSheetEntry
{
  public class ScoreSheetEntryProcessedPenaltiesController : ApiController
  {
    private ILo30Repository _repo;
    public ScoreSheetEntryProcessedPenaltiesController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<ScoreSheetEntryPenaltyProcessed> GetScoreSheetEntryPenaltiesProcessed(bool fullDetail = true)
    {
      var results = _repo.GetScoreSheetEntryPenaltiesProcessed(fullDetail);
      return results.OrderBy(x => x.GameId)
                    .OrderBy(x => x.Period)
                    .OrderByDescending(x => x.TimeRemaining)
                    .ToList();
    }

    public List<ScoreSheetEntryPenaltyProcessed> GetScoreSheetEntryPenaltiesProcessedByGameId(int gameId, bool fullDetail = true)
    {
      var results = _repo.GetScoreSheetEntryPenaltiesProcessedByGameId(gameId, fullDetail);
      return results.OrderBy(x => x.GameId)
                    .ThenBy(x => x.Period)
                    .ThenByDescending(x => x.TimeRemaining)
                    .ToList();
    }
  }
}
