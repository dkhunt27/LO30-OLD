using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class GameTeam
  {
    [Key, Column(Order=0)]
    public int GameId { get; set; }

    [Key, Column(Order = 1)]
    public bool HomeTeam { get; set; }

    [Required]
    public int SeasonTeamId { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    [ForeignKey("SeasonTeamId")]
    public virtual SeasonTeam SeasonTeam { get; set; }
  }
}