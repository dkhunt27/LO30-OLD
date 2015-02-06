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
    public List<PlayerStatSeason> GetPlayerStatsSeason()
    {
      return _playerStatsSeason.ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerId(int playerId)
    {
      return _playerStatsSeason.Where(x => x.PlayerId == playerId).ToList();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeasonByPlayerIdSeasonId(int playerId, int seasonId)
    {
      return _playerStatsSeason.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId).ToList();
    }

    public PlayerStatSeason GetPlayerStatSeasonByPlayerIdSeasonIdSub(int playerId, int seasonId, bool playoffs, bool sub)
    {
      return _playerStatsSeason.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId && x.Playoffs == playoffs && x.Sub == sub).FirstOrDefault();
    }
  }
}