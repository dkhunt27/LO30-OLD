using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Address
  {
    [Key, Column(Order = 0)]
    public int AddressId { get; set; }

    [Required, MaxLength(50)]
    public string Address1 { get; set; }

    public string Address2 { get; set; }

    [Required, MaxLength(35)]
    public string City { get; set; }

    [Required, MaxLength(2)]
    public string State { get; set; }

    [Required, MaxLength(10)]
    public string ZipCode { get; set; }
  }
}