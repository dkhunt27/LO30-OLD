using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class Phone
  {
    [Required, Key]
    public int PhoneId { get; set; }

    [Required, ForeignKey("PhoneType")]
    public int PhoneTypeId { get; set; }

    [Required, MaxLength(20)]
    public string PhoneNumber { get; set; }

    public virtual PhoneType PhoneType { get; set; }
  }
}