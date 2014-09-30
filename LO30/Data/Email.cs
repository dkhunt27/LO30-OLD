using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Email
  {
    [Key, Column(Order=0)]
    public int EmailId { get; set; }

    [Required]
    public int EmailTypeId { get; set; }

    [Required, MaxLength(50)]
    public string EmailAddress { get; set; }

    [ForeignKey("EmailTypeId")]
    public virtual EmailType EmailType { get; set; }
  }
}