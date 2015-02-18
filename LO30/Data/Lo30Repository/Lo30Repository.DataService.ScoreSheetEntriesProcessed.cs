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
    public List<ScoreSheetEntryProcessed> GetScoreSheetEntriesProcessed(bool fullDetail)
    {
      List<ScoreSheetEntryProcessed> results = null;

      if (fullDetail)
      {
        results = _ctx.ScoreSheetEntriesProcessed
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
                    .Include("GoalPlayer")
                    .Include("Assist1Player")
                    .Include("Assist2Player")
                    .Include("Assist3Player")
                    .ToList();
      }
      else
      {
        results = _ctx.ScoreSheetEntriesProcessed.ToList();
      }

      return results;
    }

    public List<ScoreSheetEntryProcessed> GetScoreSheetEntriesProcessedByGameId(int gameId, bool fullDetail)
    {
      List<ScoreSheetEntryProcessed> results = null;

      if (fullDetail)
      {
        results = _ctx.ScoreSheetEntriesProcessed
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
                    .Include("GoalPlayer")
                    .Include("Assist1Player")
                    .Include("Assist2Player")
                    .Include("Assist3Player")
                    .Where(x => x.GameId == gameId)
                    .ToList();
      }
      else
      {
        results = _ctx.ScoreSheetEntriesProcessed
                    .Where(x => x.GameId == gameId)
                    .ToList();
      }

      return results;
    }
  }
}