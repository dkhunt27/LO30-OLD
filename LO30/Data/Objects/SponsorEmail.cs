using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class SponsorEmail
  {
    [Required, Key, Column(Order = 0), ForeignKey("Sponsor")]
    public int SponsorId { get; set; }

    [Required, Key, Column(Order = 1), ForeignKey("Email")]
    public int EmailId { get; set; }

    [Required]
    public bool Primary { get; set; }

    public virtual Sponsor Sponsor { get; set; }
    public virtual Email Email { get; set; }
  }
}