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
    public List<PlayerStatCareer> GetPlayerStatsCareer()
    {
      Expression<Func<PlayerStatCareer, bool>> whereClause = x => x.PlayerId > -1;
      return GetPlayerStatsCareerBase(whereClause);
    }

    public List<PlayerStatCareer> GetPlayerStatsCareerByPlayerId(int playerId)
    {
      Expression<Func<PlayerStatCareer, bool>> whereClause = x => x.PlayerId == playerId;
      return GetPlayerStatsCareerBase(whereClause);
    }

    private List<PlayerStatCareer> GetPlayerStatsCareerBase(Expression<Func<PlayerStatCareer, bool>> whereClause)
    {
      var results = _ctx.PlayerStatsCareer
                              .Include("Player")
                              .Where(whereClause)
                              .ToList();
      return results;
    }

    public PlayerStatCareer GetPlayerStatCareerByPlayerIdSub(int playerId, bool sub)
    {
      return _contextService.FindPlayerStatCareer(playerId, sub, errorIfNotFound: false, errorIfMoreThanOneFound: true, populateFully: true);
    }
  }
}