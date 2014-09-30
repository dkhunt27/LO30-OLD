using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class PhoneType
  {
    [Key, Column(Order = 0)]
    public int PhoneTypeId { get; set; }

    [Required, MaxLength(10)]
    public string PhoneTypeName { get; set; }
  }
}