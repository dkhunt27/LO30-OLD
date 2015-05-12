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

    [Required, ForeignKey("Division")]
    public int DivisionId { get; set; }

    public virtual Season Season { get; set; }
    public virtual Team Team { get; set; }
    public virtual Division Division { get; set; }

    public SeasonTeam()
    {
    }

    public SeasonTeam(int stid, int sid, int tid, int div)
    {
      this.SeasonTeamId = stid;
      this.SeasonId = sid;
      this.TeamId = tid;
      this.DivisionId = div;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("stid: {0}",
                                      this.SeasonTeamId);
    }
  }
}