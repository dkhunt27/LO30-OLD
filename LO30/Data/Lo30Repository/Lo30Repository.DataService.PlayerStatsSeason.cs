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
    public List<PlayerStatSeason> GetPlayerStatsSeason()
    {
      Expression<Func<PlayerStatSeason, bool>> whereClause = x => x.PlayerId > -1;
      return GetPlayerStatsSeasonBase(whereClause);
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerId(int playerId)
    {
      Expression<Func<PlayerStatSeason, bool>> whereClause = x => x.PlayerId == playerId;
      return GetPlayerStatsSeasonBase(whereClause);
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      Expression<Func<PlayerStatSeason, bool>> whereClause = x => x.PlayerId == playerId && x.SeasonId == seasonId;
      return GetPlayerStatsSeasonBase(whereClause);
    }

    private List<PlayerStatSeason> GetPlayerStatsSeasonBase(Expression<Func<PlayerStatSeason, bool>> whereClause)
    {
      var results = _ctx.PlayerStatsSeason
                              .Include("Player")
                              .Include("Season")
                              .Where(whereClause)
                              .ToList();
      return results;
    }

    public PlayerStatSeason GetPlayerStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool playoffs, bool sub)
    {
      return _contextService.FindPlayerStatSeason(playerId, seasonId, playoffs, sub, errorIfNotFound: false, errorIfMoreThanOneFound: true, populateFully: true);
    }
  }
}