using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class ForWebGoalieStat
  {
    [Required, Key, Column(Order = 1)]
    public int PID { get; set; }

    [Required, Key, Column(Order = 2)]
    public int STIDPF { get; set; }

    [Required]
    public int SID { get; set; }

    [Required]
    public string Player { get; set; }

    [Required]
    public string Team { get; set; }

    [Required]
    public string Sub { get; set; }

    [Required]
    public int GP { get; set; }

    [Required]
    public int GA { get; set; }

    [Required]
    public double GAA { get; set; }

    [Required]
    public int SO { get; set; }

    [Required]
    public int W { get; set; }
  }
}