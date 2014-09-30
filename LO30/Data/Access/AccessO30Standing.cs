using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Access
{
  public class AccessO30Standing
  {
    [Key, Column(Order = 0)]
    public int SeasonId { get; set; }

    [Key, Column(Order = 1)]
    public int TeamId { get; set; }

    public string TeamName { get; set; }

    public int RegSeasonGamesPlayed { get; set; }
    public int RegSeasonWins { get; set; }
    public int RegSeasonLosses { get; set; }
    public int RegSeasonTies { get; set; }
    public int RegSeasonPoints { get; set; }
    public int RegSeasonGoalsFor { get; set; }
    public int RegSeasonGoalsAgainst { get; set; }
    public int RegSeasonPenaltyMinutes { get; set; }

  }
}