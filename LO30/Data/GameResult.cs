using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class GameResult
  {
    [Key, Column(Order = 0)]
    public int GameResultId { get; set; }

    [Index("PK2", 1, IsUnique = true)]
    public int GameId { get; set; }

    [Index("PK2", 2, IsUnique = true)]
    public int SeasonTeamId { get; set; }

    [Required, MaxLength(1)]
    public string Result { get; set; }

    [Required]
    public int GoalsFor { get; set; }

    [Required]
    public int GoalsAgainst { get; set; }

    [Required]
    public int PenaltyMinutes { get; set; }

    [Required]
    public bool Override { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    [ForeignKey("SeasonTeamId")]
    public virtual SeasonTeam SeasonTeam { get; set; }
  }
}