using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class SeasonTeam
  {
    [Required, Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int SeasonTeamId { get; set; }

    [Required, ForeignKey("Season")]
    public int SeasonId { get; set; }

    [Required, ForeignKey("Team")]
    public int TeamId { get; set; }

    [MaxLength(35)]
    public string Division { get; set; }

    public virtual Season Season { get; set; }
    public virtual Team Team { get; set; }

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

    public SeasonTeam(int stid, int sid, int tid, string div)
    {
      this.SeasonTeamId = stid;
      this.SeasonId = sid;
      this.TeamId = tid;
      this.Division = div;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("stid: {0}",
                                      this.SeasonTeamId);
    }
  }
}