using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class Email
  {
    [Required, Key]
    public int EmailId { get; set; }

    [Required, ForeignKey("EmailType")]
    public int EmailTypeId { get; set; }

    [Required, MaxLength(50)]
    public string EmailAddress { get; set; }

    public virtual EmailType EmailType { get; set; }
  }
}