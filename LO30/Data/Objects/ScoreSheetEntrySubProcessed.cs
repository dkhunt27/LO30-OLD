using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class ScoreSheetEntrySubProcessed
  {
    [Required, Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntrySubId { get; set; }

    [Required, Index("PK2", 1, IsUnique = true), ForeignKey("Game")]
    public int GameId { get; set; }

    [Required, Index("PK2", 2, IsUnique = true), ForeignKey("SubPlayer")]
    public int SubPlayerId { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required, ForeignKey("GameTeam")]
    public int GameTeamId { get; set; }

    [Required, Index("PK2", 3, IsUnique = true), ForeignKey("SubbingForPlayer")]
    public int SubbingForPlayerId { get; set; }

    [Required]
    public string JerseyNumber { get; set; }

    [Required]
    public DateTime UpdatedOn { get; set; }

    public virtual Game Game { get; set; }
    public virtual GameTeam GameTeam { get; set; }
    public virtual Player SubPlayer { get; set; }
    public virtual Player SubbingForPlayer { get; set; }

    public ScoreSheetEntrySubProcessed()
    {
    }

    public ScoreSheetEntrySubProcessed(int ssesid, int gid, bool ht, int gtid, string jer, int spid, int sfpid, DateTime upd)
    {
      this.ScoreSheetEntrySubId = ssesid;

      this.GameId = gid;
      this.HomeTeam = ht;
      this.GameTeamId = gtid;
      this.JerseyNumber = jer;
      this.SubPlayerId = spid;
      this.SubbingForPlayerId = sfpid;

      this.UpdatedOn = upd;

      Validate();
    }


    private void Validate()
    {
      var locationKey = string.Format("ssesid: {0}, gid: {1}",
                            this.ScoreSheetEntrySubId,
                            this.GameId);
    }
  }
}