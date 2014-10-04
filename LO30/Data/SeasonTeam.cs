using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class SeasonTeam
  {
    public SeasonTeam()
    {
    }

    public SeasonTeam(int stid, int sid, int tid)
    {
      this.SeasonTeamId = stid;
      this.SeasonId = sid;
      this.TeamId = tid;

      Validate();
    }

    [Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int SeasonTeamId { get; set; }

    [ForeignKey("Season")]
    [Required]
    public int SeasonId { get; set; }

    [ForeignKey("Team")]
    [Required]
    public int TeamId { get; set; }

    public virtual Season Season { get; set; }
    public virtual Team Team { get; set; }

    private void Validate()
    {
      var locationKey = string.Format("stid: {0}",
                                      this.SeasonTeamId);
    }
  }
}