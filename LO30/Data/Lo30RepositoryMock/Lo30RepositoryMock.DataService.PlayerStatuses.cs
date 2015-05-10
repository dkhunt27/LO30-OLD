using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    public List<PlayerStatus> GetPlayerStatuses()
    {
      return _playerStatuses;
    }

    public List<PlayerStatus> GetPlayerStatusesByPlayerId(int playerId)
    {
      return _playerStatuses.Where(x => x.PlayerId == playerId).ToList();

    }
    public PlayerStatus GetCurrentPlayerStatusByPlayerId(int playerId)
    {
      return _playerStatuses.Where(x => x.PlayerId == playerId && x.CurrentStatus == true).FirstOrDefault();
    }

  }
}