using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class EmailType
  {
    [Key, Column(Order = 0)]
    public int EmailTypeId { get; set; }

    [Required, MaxLength(10)]
    public string EmailTypeName { get; set; }
  }
}