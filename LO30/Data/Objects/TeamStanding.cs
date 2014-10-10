﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class TeamStanding
  {
    [Required, Key, Column(Order = 0), ForeignKey("SeasonTeam")]
    public int SeasonTeamId { get; set; }

    [Required, Key, Column(Order = 2)]
    public bool Playoff { get; set; }

    [Required]
    public int Rank { get; set; }

    [Required]
    public int Games { get; set; }

    [Required]
    public int Wins { get; set; }

    [Required]
    public int Losses { get; set; }

    [Required]
    public int Ties { get; set; }

    [Required]
    public int Points { get; set; }

    [Required]
    public int GoalsFor { get; set; }

    [Required]
    public int GoalsAgainst { get; set; }

    [Required]
    public int PenaltyMinutes { get; set; }

    public SeasonTeam SeasonTeam { get; set; }

    public TeamStanding()
    {
      this.Games = 0;
      this.Wins = 0;
      this.Losses = 0;
      this.Ties = 0;
      this.Points = 0;
      this.GoalsFor = 0;
      this.GoalsAgainst = 0;
      this.PenaltyMinutes = 0;
    }
  }
}