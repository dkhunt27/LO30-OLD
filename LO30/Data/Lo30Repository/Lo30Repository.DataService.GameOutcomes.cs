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
    public List<GameOutcome> GetGameOutcomes(bool fullDetail)
    {
      List<GameOutcome> gameOutcomes = null;
 
      if (fullDetail)
      {
        gameOutcomes = _ctx.GameOutcomes
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .Include("OpponentGameTeam")
                    .Include("OpponentGameTeam.Game")
                    .Include("OpponentGameTeam.Game.Season")
                    .Include("OpponentGameTeam.SeasonTeam")
                    .Include("OpponentGameTeam.SeasonTeam.Season")
                    .Include("OpponentGameTeam.SeasonTeam.Team")
                    .Include("OpponentGameTeam.SeasonTeam.Team.Coach")
                    .Include("OpponentGameTeam.SeasonTeam.Team.Sponsor")
                    .ToList();
      } 
      else
      {
        gameOutcomes = _ctx.GameOutcomes.ToList();
      }

      return gameOutcomes;
    }

    public List<GameOutcome> GetGameOutcomesByGameId(int gameId, bool fullDetail)
    {
      List<GameOutcome> gameOutcomes = null;

      if (fullDetail)
      {
        gameOutcomes = _ctx.GameOutcomes
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .Include("OpponentGameTeam")
                    .Include("OpponentGameTeam.Game")
                    .Include("OpponentGameTeam.Game.Season")
                    .Include("OpponentGameTeam.SeasonTeam")
                    .Include("OpponentGameTeam.SeasonTeam.Season")
                    .Include("OpponentGameTeam.SeasonTeam.Team")
                    .Include("OpponentGameTeam.SeasonTeam.Team.Coach")
                    .Include("OpponentGameTeam.SeasonTeam.Team.Sponsor")
                    .Where(x => x.GameTeam.GameId == gameId)
                    .ToList();
      }
      else
      {
        gameOutcomes = _ctx.GameOutcomes
                    .Include("GameTeam")
                    .Where(x => x.GameTeam.GameId == gameId)
                    .ToList();
      }

      return gameOutcomes;
    }

    public GameOutcome GetGameOutcomeByGameIdAndHomeTeam(int gameId, bool homeTeam, bool fullDetail)
    {
      GameOutcome gameOutcome = null;

      if (fullDetail)
      {
        gameOutcome = _ctx.GameOutcomes
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .Include("OpponentGameTeam")
                    .Include("OpponentGameTeam.Game")
                    .Include("OpponentGameTeam.Game.Season")
                    .Include("OpponentGameTeam.SeasonTeam")
                    .Include("OpponentGameTeam.SeasonTeam.Season")
                    .Include("OpponentGameTeam.SeasonTeam.Team")
                    .Include("OpponentGameTeam.SeasonTeam.Team.Coach")
                    .Include("OpponentGameTeam.SeasonTeam.Team.Sponsor")
                    .Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam)
                    .FirstOrDefault();
      }
      else
      {
        gameOutcome = _ctx.GameOutcomes
                    .Include("GameTeam")
                    .Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam)
                    .FirstOrDefault();
      }

      return gameOutcome;
    }

    public List<GameOutcome> GetGameOutcomesBySeasonTeamId(int seasonId, bool playoffs, int seasonTeamId, bool fullDetail)
    {
      List<GameOutcome> gameOutcomes = null;

      if (fullDetail)
      {
        gameOutcomes = _ctx.GameOutcomes
                    .Include("GameTeam")
                    .Include("GameTeam.Game")
                    .Include("GameTeam.Game.Season")
                    .Include("GameTeam.SeasonTeam")
                    .Include("GameTeam.SeasonTeam.Season")
                    .Include("GameTeam.SeasonTeam.Team")
                    .Include("GameTeam.SeasonTeam.Team.Coach")
                    .Include("GameTeam.SeasonTeam.Team.Sponsor")
                    .Include("OpponentGameTeam")
                    .Include("OpponentGameTeam.Game")
                    .Include("OpponentGameTeam.Game.Season")
                    .Include("OpponentGameTeam.SeasonTeam")
                    .Include("OpponentGameTeam.SeasonTeam.Season")
                    .Include("OpponentGameTeam.SeasonTeam.Team")
                    .Include("OpponentGameTeam.SeasonTeam.Team.Coach")
                    .Include("OpponentGameTeam.SeasonTeam.Team.Sponsor")
                    .Where(x => x.GameTeam.SeasonTeam.SeasonTeamId == seasonTeamId && x.GameTeam.Game.Playoffs == playoffs)
                    .ToList();
      }
      else
      {
        gameOutcomes = _ctx.GameOutcomes
                    .Include("GameTeam")
                    .Include("GameTeam.SeasonTeam")
                    .Where(x => x.GameTeam.SeasonTeam.SeasonTeamId == seasonTeamId && x.GameTeam.Game.Playoffs == playoffs)
                    .ToList();
      }

      return gameOutcomes;
    }
  }
}