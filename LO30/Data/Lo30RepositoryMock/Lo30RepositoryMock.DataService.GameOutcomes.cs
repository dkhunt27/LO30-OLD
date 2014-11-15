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
    public List<GameOutcome> GetGameOutcomes()
    {
      return _gameOutcomes;
    }

    public List<GameOutcome> GetGameOutcomesByGameId(int gameId)
    {
      return _gameOutcomes.Where(x => x.GameTeam.GameId == gameId).ToList();
    }

    public List<GameOutcome> GetGameOutcomesByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return _gameOutcomes.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam).ToList();
    }
  }
}