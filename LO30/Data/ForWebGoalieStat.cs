using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class ForWebGoalieStat
  {
    public string Player { get; set; }
    public string Team { get; set; }
    public string Sub { get; set; }
    public int GP { get; set; }
    public int GA { get; set; }
    public double GAA { get; set; }
    public int SO { get; set; }
    public int W { get; set; }
  }
}