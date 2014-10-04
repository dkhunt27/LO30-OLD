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
    [Key, Column(Order = 0)]
    public int GameTeamId { get; set; }

    [ForeignKey("Game")]
    [Index("PK2", 1, IsUnique=true)]
    public int GameId { get; set; }

    [Index("PK2", 2, IsUnique = true)]
    public bool HomeTeam { get; set; }

    [ForeignKey("SeasonTeam")]
    [Required]
    public int SeasonTeamId { get; set; }

    public virtual Game Game { get; set; }
    public virtual SeasonTeam SeasonTeam { get; set; }
  }
}