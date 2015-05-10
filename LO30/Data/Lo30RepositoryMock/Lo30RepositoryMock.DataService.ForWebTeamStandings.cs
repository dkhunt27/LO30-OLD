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
    public List<ForWebTeamStanding> GetTeamStandingsForWeb(int seasonId, bool playoffs)
    {
      return _webTeamStandings.Where(x => x.SID == seasonId && x.PFS == playoffs).ToList();
    }

    public DateTime GetTeamStandingsForWebDataGoodThru(int seasonId)
    {
      var maxGameData = _gameOutcomes
              .GroupBy(x => new { x.GameTeam.SeasonTeam.SeasonId, x.GameTeam.Game.Playoffs })
              .Select(grp => new
              {
                SeasonId = grp.Key.SeasonId,
                Playoffs = grp.Key.Playoffs,
                GameId = grp.Max(x => x.GameTeam.GameId),
                GameDateTime = grp.Max(x => x.GameTeam.Game.GameDateTime)
              })
              .Where(x => x.SeasonId == seasonId)
              .ToList();

      var gameDateTime = maxGameData.FirstOrDefault().GameDateTime;

      return gameDateTime;
    }
  }
}