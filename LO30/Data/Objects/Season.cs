using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class Season
  {
    [Required, Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int SeasonId { get; set; }

    [Required, MaxLength(12)]
    public string SeasonName { get; set; }

    [Required]
    public bool IsCurrentSeason { get; set; }

    //TODO..add back the PK2 is unique
    //[Required, Index("PK2", 2, IsUnique = true)]
    [Required]
    public int StartYYYYMMDD { get; set; }

    //[Required, Index("PK2", 3, IsUnique = true)]
    [Required]
    public int EndYYYYMMDD { get; set; }
  }
}