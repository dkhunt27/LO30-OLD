using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class PlayerRating
  {
    [Required, Key, Column(Order = 0), ForeignKey("Season")]
    public int SeasonId { get; set; }

    [Required, Key, Column(Order = 1), ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required]
    public int RatingPrimary { get; set; }

    [Required]
    public int RatingSecondary { get; set; }
    
    [Required]
    public int Line { get; set; }

    public virtual Season Season { get; set; }
    public virtual Player Player { get; set; }
  }
}