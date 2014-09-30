using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class SponsorEmail
  {
    [Key, Column(Order = 0)]
    public int SponsorId { get; set; }
    
    [Key, Column(Order = 1)]
    public int EmailId { get; set; }

    [Required]
    public bool Primary { get; set; }

    [ForeignKey("SponsorId")]
    public virtual Sponsor Sponsor { get; set; }

    [ForeignKey("EmailId")]
    public virtual Email Email { get; set; }
  }
}