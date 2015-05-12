using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class PlayerEmail
  {
    [Required, Key, Column(Order = 0), ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, Key, Column(Order = 1), ForeignKey("Email")]
    public int EmailId { get; set; }

    [Required]
    public bool Primary { get; set; }

    public virtual Player Player { get; set; }
    public virtual Email Email { get; set; }
  }
}