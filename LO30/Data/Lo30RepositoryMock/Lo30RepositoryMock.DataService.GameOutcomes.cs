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
    public List<GameOutcome> GetGameOutcomes(bool fullDetail = true)
    {
      return _gameOutcomes;
    }

    public List<GameOutcome> GetGameOutcomesByGameId(int gameId, bool fullDetail = true)
    {
      return _gameOutcomes.Where(x => x.GameTeam.GameId == gameId).ToList();
    }

    public GameOutcome GetGameOutcomeByGameIdAndHomeTeam(int gameId, bool homeTeam, bool fullDetail = true)
    {
      return _gameOutcomes.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam).FirstOrDefault();
    }

    public List<GameOutcome> GetGameOutcomesBySeasonTeamId(int seasonTeamId, bool fullDetail = true)
    {
      return _gameOutcomes.Where(x => x.GameTeam.SeasonTeam.SeasonTeamId == seasonTeamId).ToList();
    }

  }
}