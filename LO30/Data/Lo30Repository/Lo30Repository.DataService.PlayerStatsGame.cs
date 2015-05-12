using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace LO30.Data
{
  public partial class Lo30Repository
  {
    public List<PlayerStatGame> GetPlayerStatsGame()
    {
      Expression<Func<PlayerStatGame, bool>> whereClause = x => x.PlayerId > -1;
      return GetPlayerStatsGameBase(whereClause);
    }

    public List<PlayerStatGame> GetPlayerStatsGameByPlayerId(int playerId)
    {
      Expression<Func<PlayerStatGame, bool>> whereClause = x => x.PlayerId == playerId;
      return GetPlayerStatsGameBase(whereClause);
    }

    public List<PlayerStatGame> GetPlayerStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    {
      Expression<Func<PlayerStatGame, bool>> whereClause = x => x.PlayerId == playerId && x.SeasonId == seasonId;
      return GetPlayerStatsGameBase(whereClause);
    }

    private List<PlayerStatGame> GetPlayerStatsGameBase(Expression<Func<PlayerStatGame, bool>> whereClause)
    {
      var results = _ctx.PlayerStatsGame
                              .Include("Season")
                              .Include("Player")
                              .Include("Game")
                              //.Include("Game.Season")
                              .Include("SeasonTeam")
                              //.Include("SeasonTeam.Season")
                              .Include("SeasonTeam.Team")
                              .Include("SeasonTeam.Team.Coach")
                              .Include("SeasonTeam.Team.Sponsor")
                              .Where(whereClause)
                              .ToList();
      return results;
    }

    public PlayerStatGame GetPlayerStatGameByPlayerIdGameId(int playerId, int gameId)
    {
      return _contextService.FindPlayerStatGame(playerId, gameId, errorIfNotFound: false, errorIfMoreThanOneFound: true, populateFully: true);
    }
  }
}