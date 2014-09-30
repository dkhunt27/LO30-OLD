using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Access
{
  [Table("REF_SEASON")]
  public class AccessO30Season
  {
    [Key, Column(Order = 0)]
    public int SeasonId { get; set; }
    public int SeasonName { get; set; }
    public string CurrentSeasonInd { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

  }
}