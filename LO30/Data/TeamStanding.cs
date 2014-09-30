﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class TeamStanding
  {
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

    [Key, Column(Order = 0)]
    public int SeasonTeamId { get; set; }

    [Key, Column(Order = 2)]
    public int SeasonTypeId { get; set; }

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

    [ForeignKey("SeasonTeamId")]
    public virtual SeasonTeam SeasonTeam { get; set; }

    [ForeignKey("SeasonTypeId")]
    public virtual SeasonType SeasonType { get; set; }
  }
}