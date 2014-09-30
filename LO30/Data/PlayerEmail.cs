using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class PlayerEmail
  {
    [Key, Column(Order = 0)]
    public int PlayerId { get; set; }
    
    [Key, Column(Order = 1)]
    public int EmailId { get; set; }

    [Required]
    public bool Primary { get; set; }

    [ForeignKey("PlayerId")]
    public virtual Player Player { get; set; }

    [ForeignKey("EmailId")]
    public virtual Email Email { get; set; }
  }
}