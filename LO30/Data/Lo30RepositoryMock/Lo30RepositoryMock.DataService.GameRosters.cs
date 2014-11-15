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
    public List<GameRoster> GetGameRosters()
    {
      return _gameRosters;
    }

    public List<GameRoster> GetGameRostersByGameId(int gameId)
    {
      return _gameRosters.Where(x => x.GameTeam.GameId == gameId).ToList();
    }

    public List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return _gameRosters.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam).ToList();
    }

    public GameRoster GetGameRosterByGameRosterId(int gameRosterId)
    {
      return _gameRosters.Where(x => x.GameRosterId == gameRosterId).FirstOrDefault();
    }

    public GameRoster GetGameRosterByGameTeamIdAndPlayerNumber(int gameTeamId, string playerNumber)
    {
      return _gameRosters.Where(x => x.GameTeamId == gameTeamId && x.PlayerNumber == playerNumber).FirstOrDefault();
    }

  }
}