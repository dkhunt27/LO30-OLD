using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class PlayerStatType
  {
    [Key, Column(Order = 0)]
    public int PlayerStatTypeId { get; set; }

    [Required, MaxLength(25)]
    public string PlayerStatTypeName { get; set; }
  }
}