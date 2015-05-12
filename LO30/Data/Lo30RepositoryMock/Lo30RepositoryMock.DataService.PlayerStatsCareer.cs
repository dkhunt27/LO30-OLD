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
    public List<PlayerStatCareer> GetPlayerStatsCareer()
    {
      return _playerStatsCareer.ToList();
    }

    public List<PlayerStatCareer> GetPlayerStatsCareerByPlayerId(int playerId)
    {
      return _playerStatsCareer.Where(x => x.PlayerId == playerId).ToList();
    }

    public PlayerStatCareer GetPlayerStatCareerByPlayerIdPlayoffs(int playerId, bool playoffs)
    {
      return _playerStatsCareer.Where(x => x.PlayerId == playerId && x.Playoffs == playoffs).FirstOrDefault();
    }
  }
}