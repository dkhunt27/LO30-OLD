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
    public List<GameScore> GetGameScores(bool fullDetail)
    {
      List<GameScore> gameScores = null;

      if (fullDetail)
      {
        gameScores = _ctx.GameScores
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .ToList();
      }
      else
      {
        gameScores = _ctx.GameScores.ToList();
      }

      return gameScores;
    }

    public List<GameScore> GetGameScoresByGameId(int gameId, bool fullDetail)
    {
      List<GameScore> gameScores = null;

      if (fullDetail)
      {
        gameScores = _ctx.GameScores
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .Where(x => x.GameTeam.GameId == gameId)
                    .ToList();
      }
      else
      {
        gameScores = _ctx.GameScores
                    .Include("GameTeam")
                    .Where(x => x.GameTeam.GameId == gameId)          
                    .ToList();
      }

      return gameScores;
    }

    public List<GameScore> GetGameScoresByGameIdAndHomeTeam(int gameId, bool homeTeam, bool fullDetail)
    {
      List<GameScore> gameScores = null;

      if (fullDetail)
      {
        gameScores = _ctx.GameScores
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam)
                    .ToList();
      }
      else
      {
        gameScores = _ctx.GameScores
                    .Include("GameTeam")
                    .Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam)
                    .ToList();
      }

      return gameScores;
    }

  }
}