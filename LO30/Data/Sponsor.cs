using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Sponsor
  {
    [Key, Column(Order = 0)]
    public int SponsorId { get; set; }

    [Required, MaxLength(15)]
    public string TeamShortName { get; set; }

    [Required, MaxLength(25)]
    public string TeamLongName { get; set; }
  }
}