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
    public List<Game> GetGames()
    {
      return _ctx.Games
                    .Include("Season")
                    .ToList();
    }

    public Game GetGameByGameId(int gameId)
    {
      return _contextService.FindGame(gameId);
    }
  }
}