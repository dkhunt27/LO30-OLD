using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class PlayerStatSeason
  {
    public PlayerStatSeason()
    {
      this.Games = 0;
      this.Goals = 0;
      this.Assists = 0;
      this.Points = 0;
      this.PenaltyMinutes = 0;
      this.PowerPlayGoals = 0;
      this.ShortHandedGoals = 0;
      this.GameWinningGoals = 0;
    }

    [Key, Column(Order = 0)]
    public int SeasonId { get; set; }

    [Key, Column(Order = 1)]
    public int PlayerId { get; set; }

    [Key, Column(Order = 2)]
    public int PlayerStatTypeId { get; set; }

    [Required]
    public int Games { get; set; }

    [Required]
    public int Goals { get; set; }

    [Required]
    public int Assists { get; set; }

    [Required]
    public int Points { get; set; }

    [Required]
    public int PenaltyMinutes { get; set; }

    [Required]
    public int PowerPlayGoals { get; set; }

    [Required]
    public int ShortHandedGoals { get; set; }

    [Required]
    public int GameWinningGoals { get; set; }

    [ForeignKey("SeasonId")]
    public virtual Season Season { get; set; }

    [ForeignKey("PlayerId")]
    public virtual Player Player { get; set; }

    [ForeignKey("PlayerStatTypeId")]
    public virtual PlayerStatType PlayerStatType { get; set; }
  }
}