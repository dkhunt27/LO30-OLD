using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class TeamRoster
  {
    [Required, Key, Column(Order = 0), ForeignKey("SeasonTeam")]
    public int SeasonTeamId { get; set; }

    [Required, Key, Column(Order = 1), ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, Key, Column(Order = 2)]
    public bool Playoff { get; set; }

    [Required]
    public int Line { get; set; }

    [Required]
    public string Position { get; set; }

    public int? PlayerNumber { get; set; }

    public virtual SeasonTeam SeasonTeam { get; set; }
    public virtual Player Player { get; set; }
  }
}