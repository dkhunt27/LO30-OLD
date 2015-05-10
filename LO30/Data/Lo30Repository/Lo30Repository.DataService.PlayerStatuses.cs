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
    public List<PlayerStatus> GetPlayerStatuses()
    {
      return _ctx.PlayerStatuses.ToList();
    }

    public List<PlayerStatus> GetPlayerStatusesByPlayerId(int playerId)
    {
      return _ctx.PlayerStatuses.Where(x=>x.PlayerId == playerId).ToList();
    }

    public PlayerStatus GetCurrentPlayerStatusByPlayerId(int playerId)
    {
      return _contextService.FindPlayerStatusWithCurrentStatus(playerId, currentStatus: true);
    }
  }
}