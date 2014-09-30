using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class PlayerPhone
  {
    [Key, Column(Order = 0)]
    public int PlayerId { get; set; }

    [Key, Column(Order = 1)]
    public int PhoneId { get; set; }

    [Required]
    public bool Primary { get; set; }

    [ForeignKey("PlayerId")]
    public virtual Player Player { get; set; }

    [ForeignKey("PhoneId")]
    public virtual Phone Phone { get; set; }
  }
}