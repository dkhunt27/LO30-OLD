using LO30.Data;
using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LO30.Controllers.Data.Games
{
  public class GameOutcomesBySeasonTeamController : ApiController
  {
    private ILo30Repository _repo;
    public GameOutcomesBySeasonTeamController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public List<GameOutcome> GetGameOutcomesBySeasonTeamId(int seasonId, bool playoffs, int seasonTeamId, bool fullDetail = true)
    {
      var results = _repo.GetGameOutcomesBySeasonTeamId(seasonId, playoffs, seasonTeamId, fullDetail);
      return results.OrderByDescending(x => x.GameTeam.GameId)
                    .ToList();
    }
  }
}
