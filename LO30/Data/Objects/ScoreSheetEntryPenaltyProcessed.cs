using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class ScoreSheetEntryPenaltyProcessed
  {
    [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntryPenaltyId { get; set; }

    [Required, ForeignKey("Game")]
    public int GameId { get; set; }

    [Required]
    public int Period { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required, ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, ForeignKey("Penalty")]
    public int PenaltyId { get; set; }

    [Required, MaxLength(5)]
    public string TimeRemaining { get; set; }
    
    [Required]
    public int PenaltyMinutes { get; set; }

    public virtual Game Game { get; set; }
    public virtual Player Player { get; set; }
    public virtual Penalty Penalty { get; set; }
  }
}