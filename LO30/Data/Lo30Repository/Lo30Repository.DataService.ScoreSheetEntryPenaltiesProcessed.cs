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
    public List<ScoreSheetEntryPenaltyProcessed> GetScoreSheetEntryPenaltiesProcessed(bool fullDetail)
    {
      List<ScoreSheetEntryPenaltyProcessed> results = null;

      if (fullDetail)
      {
        results = _ctx.ScoreSheetEntryPenaltiesProcessed
                    .Include("Game")
                    .Include("Game.Season")
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .Include("Player")
                    .Include("Penalty")
                    .ToList();
      }
      else
      {
        results = _ctx.ScoreSheetEntryPenaltiesProcessed.ToList();
      }

      return results;
    }

    public List<ScoreSheetEntryPenaltyProcessed> GetScoreSheetEntryPenaltiesProcessedByGameId(int gameId, bool fullDetail)
    {
      List<ScoreSheetEntryPenaltyProcessed> results = null;

      if (fullDetail)
      {
        results = _ctx.ScoreSheetEntryPenaltiesProcessed
                    .Include("Game")
                    .Include("Game.Season")
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .Include("Player")
                    .Include("Penalty")
                    .Where(x => x.GameId == gameId)
                    .ToList();
      }
      else
      {
        results = _ctx.ScoreSheetEntryPenaltiesProcessed
                    .Where(x => x.GameId == gameId)
                    .ToList();
      }

      return results;
    }
  }
}