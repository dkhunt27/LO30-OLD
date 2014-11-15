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
    public List<PlayerStatGame> GetPlayerStatsGame()
    {
      return _playerStatsGame.ToList();
    }

    public List<PlayerStatGame> GetPlayerStatsGameByPlayerId(int playerId)
    {
      return _playerStatsGame.Where(x => x.PlayerId == playerId).ToList();
    }

    public List<PlayerStatGame> GetPlayerStatsGameByPlayerIdSeasonId(int playerId, int seasonId)
    {
      return _playerStatsGame.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId).ToList();
    }

    public PlayerStatGame GetPlayerStatGameByPlayerIdGameId(int playerId, int gameId)
    {
      return _playerStatsGame.Where(x => x.PlayerId == playerId && x.GameId == gameId).FirstOrDefault();
    }
  }
}