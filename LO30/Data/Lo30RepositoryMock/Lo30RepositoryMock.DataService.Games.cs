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
    public List<Game> GetGames()
    {
      return _games;
    }

    public Game GetGameByGameId(int gameId)
    {
      return _games.Where(x => x.GameId == gameId).FirstOrDefault();
    }
  }
}