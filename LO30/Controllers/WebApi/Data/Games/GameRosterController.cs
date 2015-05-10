using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameRosterController : ApiController
  {
    private ILo30Repository _repo;
    public GameRosterController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public GameRoster GetByGameRosterId(int gameRosterId)
    {
      var result = _repo.GetGameRosterByGameRosterId(gameRosterId);
      return result;
    }

    public GameRoster GetGameRosterByGameTeamIdAndPlayerNumber(int gameTeamId, string playerNumber)
    {
      var result = _repo.GetGameRosterByGameTeamIdAndPlayerNumber(gameTeamId, playerNumber);
      return result;
    }
  }
}
