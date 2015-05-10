using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class EmailType
  {
    [Required, Key]
    public int EmailTypeId { get; set; }

    [Required, MaxLength(10)]
    public string EmailTypeName { get; set; }
  }
}