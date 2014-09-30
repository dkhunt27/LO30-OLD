using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class GameScore
  {
    [Key, Column(Order = 0)]
    public int GameId { get; set; }

    [Key, Column(Order = 1)]
    public int SeasonTeamId { get; set; }

    [Key, Column(Order = 2)]
    public int Period { get; set; }

    [Required]
    public int Score { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    [ForeignKey("SeasonTeamId")]
    public virtual SeasonTeam SeasonTeam { get; set; }
  }
}