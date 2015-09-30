using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class Team
  {
    [Required, Key]
    public int TeamId { get; set; }

    [Required, MaxLength(15)]
    public string TeamShortName { get; set; }

    [Required, MaxLength(35)]
    public string TeamLongName { get; set; }

    [ForeignKey("Coach")]
    public int? CoachId { get; set; }

    [ForeignKey("Sponsor")]
    public int? SponsorId { get; set; }

    //[Required]
    public int? SeasonId { get; set; }

    public virtual Player Coach { get; set; }
    public virtual Sponsor Sponsor { get; set; }
  }
}