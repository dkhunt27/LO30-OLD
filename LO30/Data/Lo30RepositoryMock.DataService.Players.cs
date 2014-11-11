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
    public List<Player> GetPlayers()
    {
      return _players;
    }

    public Player GetPlayerByPlayerId(int playerId)
    {
      return _players.Where(x => x.PlayerId == playerId).FirstOrDefault();
    }

  }
}