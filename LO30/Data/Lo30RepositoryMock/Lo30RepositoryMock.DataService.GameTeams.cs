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
    public List<GameTeam> GetGameTeams()
    {
      return _gameTeams;
    }

    public List<GameTeam> GetGameTeamsByGameId(int gameId)
    {
      return _gameTeams.Where(x => x.GameId == gameId).ToList();
    }

    public GameTeam GetGameTeamByGameTeamId(int gameTeamId)
    {
      return _gameTeams.Where(x => x.GameTeamId == gameTeamId).FirstOrDefault();
    }

    public GameTeam GetGameTeamByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return _gameTeams.Where(x => x.GameId == gameId && x.HomeTeam == homeTeam).FirstOrDefault();
    }
  }
}