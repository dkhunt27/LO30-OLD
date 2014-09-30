using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Game
  {
    [Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int GameId { get; set; }

    [Required]
    public int SeasonTypeId { get; set; }

    [Required]
    public DateTime GameDateTime { get; set; }

    [Required, MaxLength(15)]
    public string Location { get; set; }

    [ForeignKey("SeasonTypeId")]
    public virtual SeasonType SeasonType { get; set; }
  }
}