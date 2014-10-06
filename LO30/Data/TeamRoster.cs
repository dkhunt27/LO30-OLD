using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class TeamRoster
  {
    [Key, Column(Order = 0)]
    public int SeasonTeamId { get; set; }

    [Key, Column(Order = 1)]
    public int PlayerId { get; set; }

    [Key, Column(Order = 2)]
    public int SeasonTypeId { get; set; }

    public int? PlayerNumber { get; set; }

    [ForeignKey("SeasonTeamId")]
    public virtual SeasonTeam SeasonTeam { get; set; }

    [ForeignKey("PlayerId")]
    public virtual Player Player { get; set; }

    [Required]
    public bool Playoff { get; set; }
  }
}