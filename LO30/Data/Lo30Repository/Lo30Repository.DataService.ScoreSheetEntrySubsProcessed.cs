using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30Repository
  {
    public List<ScoreSheetEntrySubProcessed> GetScoreSheetEntrySubsProcessed(bool fullDetail)
    {
      List<ScoreSheetEntrySubProcessed> results = null;

      if (fullDetail)
      {
        results = _ctx.ScoreSheetEntrySubsProcessed
                    .Include("Game")
                    .Include("Game.Season")
                    .Include("SubPlayer")
                    .Include("SubbingForPlayer")
                    .ToList();
      }
      else
      {
        results = _ctx.ScoreSheetEntrySubsProcessed.ToList();
      }

      return results;
    }

    public List<ScoreSheetEntrySubProcessed> GetScoreSheetEntrySubsProcessedByGameId(int gameId, bool fullDetail)
    {
      List<ScoreSheetEntrySubProcessed> results = null;

      if (fullDetail)
      {
        results = _ctx.ScoreSheetEntrySubsProcessed
                    .Include("Game")
                    .Include("Game.Season")
                    .Include("SubPlayer")
                    .Include("SubbingForPlayer")
                    .Where(x => x.GameId == gameId)
                    .ToList();
      }
      else
      {
        results = _ctx.ScoreSheetEntrySubsProcessed
                    .Where(x => x.GameId == gameId)
                    .ToList();
      }

      return results;
    }
  }
}