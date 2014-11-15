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
    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeam()
    {
      Expression<Func<PlayerStatSeasonTeam, bool>> whereClause = x => x.PlayerId > -1;
      return GetPlayerStatsSeasonTeamBase(whereClause);
    }

    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamByPlayerId(int playerId)
    {
      Expression<Func<PlayerStatSeasonTeam, bool>> whereClause = x => x.PlayerId == playerId;
      return GetPlayerStatsSeasonTeamBase(whereClause);
    }

    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamByPlayerIdSeasonId(int playerId, int seasonId)
    {
      Expression<Func<PlayerStatSeasonTeam, bool>> whereClause = x => x.PlayerId == playerId && x.SeasonId == seasonId;
      return GetPlayerStatsSeasonTeamBase(whereClause);
    }

    private List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamBase(Expression<Func<PlayerStatSeasonTeam, bool>> whereClause)
    {
      var results = _ctx.PlayerStatsSeasonTeam
                              .Include("Season")
                              .Include("Player")
                              .Include("SeasonTeam")
                              //.Include("SeasonTeam.Season")
                              .Include("SeasonTeam.Team")
                              .Include("SeasonTeam.Team.Coach")
                              .Include("SeasonTeam.Team.Sponsor")
                              .Where(whereClause)
                              .ToList();
      return results;
    }

    public PlayerStatSeasonTeam GetPlayerStatSeasonTeamByPlayerIdSeasonTeamId(int playerId, int seasonTeamId)
    {
      return _contextService.FindPlayerStatSeasonTeam(playerId, seasonTeamId, errorIfNotFound: false, errorIfMoreThanOneFound: true, populateFully: true);
    }
  }
}