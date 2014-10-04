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
    public int GameScoreId { get; set; }

    [Index("PK2", 1, IsUnique = true)]
    public int GameId { get; set; }

    [Index("PK2", 2, IsUnique = true)]
    public int SeasonTeamId { get; set; }

    [Index("PK2", 3, IsUnique = true)]
    public int Period { get; set; }

    [Required]
    public int Score { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    [ForeignKey("SeasonTeamId")]
    public virtual SeasonTeam SeasonTeam { get; set; }
  }
}