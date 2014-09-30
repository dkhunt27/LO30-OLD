using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Team
  {
    [Key, Column(Order = 0)]
    public int TeamId { get; set; }

    [Required, MaxLength(15)]
    public string TeamShortName { get; set; }

    [Required, MaxLength(35)]
    public string TeamLongName { get; set; }

    public int? CoachId { get; set; }

    public int? SponsorId { get; set; }

    [ForeignKey("CoachId")]
    public virtual Player Coach { get; set; }

    [ForeignKey("SponsorId")]
    public virtual Sponsor Sponser { get; set; }
  }
}