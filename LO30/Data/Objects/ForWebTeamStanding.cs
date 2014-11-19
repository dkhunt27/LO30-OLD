using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class ForWebTeamStanding
  {
    [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int STID { get; set; }

    [Required]
    public int Rank { get; set; }

    [Required, MaxLength(35)]
    public string Team { get; set; }

    [Required]
    public int GP { get; set; }

    [Required]
    public int W { get; set; }

    [Required]
    public int L { get; set; }

    [Required]
    public int T { get; set; }

    [Required]
    public int PTS { get; set; }

    [Required]
    public int GF { get; set; }

    [Required]
    public int GA { get; set; }

    [Required]
    public int PIM { get; set; }

    [Required]
    public float WPCT { get; set; }
  }
}