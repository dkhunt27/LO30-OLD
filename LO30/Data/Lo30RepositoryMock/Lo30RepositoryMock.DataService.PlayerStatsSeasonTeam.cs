using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeam()
    {
      return _playerStatsSeasonTeam.ToList();
    }

    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamByPlayerId(int playerId)
    {
      return _playerStatsSeasonTeam.Where(x => x.PlayerId == playerId).ToList();
    }

    public List<PlayerStatSeasonTeam> GetPlayerStatsSeasonTeamByPlayerIdSeasonId(int playerId, int seasonId)
    {
      return _playerStatsSeasonTeam.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId).ToList();
    }

    public PlayerStatSeasonTeam GetPlayerStatSeasonTeamByPlayerIdSeasonTeamId(int playerId, int seasonTeamId)
    {
      return _playerStatsSeasonTeam.Where(x => x.PlayerId == playerId && x.SeasonTeamId == seasonTeamId).FirstOrDefault();
    }
  }
}