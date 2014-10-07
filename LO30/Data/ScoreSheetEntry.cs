using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LO30.Data
{
  [DataContract]
  public class ScoreSheetEntry
  {
    [DataMember]
    [Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntryId { get; set; }

    [DataMember]
    [Required]
    public int GameId { get; set; }

    [DataMember]
    [Required]
    public int Period { get; set; }

    [DataMember]
    [Required]
    public bool HomeTeam { get; set; }

    [DataMember]
    [Required]
    public int Goal { get; set; }

    [DataMember]
    public int? Assist1 { get; set; }

    [DataMember]
    public int? Assist2 { get; set; }

    [DataMember]
    public int? Assist3 { get; set; }

    [DataMember]
    [Required, MaxLength(5)]
    public string TimeRemaining { get; set; }

    [DataMember]
    [MaxLength(2)]
    public string ShortHandedPowerPlay { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }
  }
}