using LO30.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<ForWebTeamStanding> GetTeamStandingsForWeb(int seasonId, bool playoffs);

    DateTime GetTeamStandingsForWebDataGoodThru(int seasonId);
  }
}
