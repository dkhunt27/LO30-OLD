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
    public List<ForWebTeamStanding> GetTeamStandingsForWeb()
    {
      return _ctx.ForWebTeamStandings.ToList();
    }
  }
}