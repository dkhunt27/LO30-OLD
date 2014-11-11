using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30Repository
  {
    public List<Player> GetPlayers()
    {
      return _ctx.Players.ToList();
    }

    public Player GetPlayerByPlayerId(int playerId)
    {
      return _contextService.FindPlayer(playerId);
    }
  }
}