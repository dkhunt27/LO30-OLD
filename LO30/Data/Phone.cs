using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Phone
  {
    [Key, Column(Order=0)]
    public int PhoneId { get; set; }

    [Required]
    public int PhoneTypeId { get; set; }

    [Required, MaxLength(20)]
    public string PhoneNumber { get; set; }

    [ForeignKey("PhoneTypeId")]
    public virtual PhoneType PhoneType { get; set; }
  }
}