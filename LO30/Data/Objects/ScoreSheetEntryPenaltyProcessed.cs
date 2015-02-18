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
  public class ScoreSheetEntryPenaltyProcessed
  {
    [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntryPenaltyId { get; set; }

    [Required, ForeignKey("Game")]
    public int GameId { get; set; }

    [Required]
    public int Period { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required, ForeignKey("GameTeam")]
    public int GameTeamId { get; set; }

    [Required, ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, ForeignKey("Penalty")]
    public int PenaltyId { get; set; }

    [Required, MaxLength(5)]
    public string TimeRemaining { get; set; }

    [Required]
    public int PenaltyMinutes { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual Game Game { get; set; }
    public virtual GameTeam GameTeam { get; set; }
    public virtual Player Player { get; set; }
    public virtual Penalty Penalty { get; set; }

    public ScoreSheetEntryPenaltyProcessed()
    {
    }

    public ScoreSheetEntryPenaltyProcessed(int ssepid, int gid, int per, bool ht, string time, int gtid, int playid, int penid, int pim, DateTime upd)
    {
      this.ScoreSheetEntryPenaltyId = ssepid;

      this.GameId = gid;
      this.Period = per;
      this.HomeTeam = ht;
      this.TimeRemaining = time;
      this.GameTeamId = gtid;

      this.PlayerId = playid;
      this.PenaltyId = penid;
      this.PenaltyMinutes = pim;

      this.UpdatedOn = upd;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("ssepid: {0}, gid: {1}",
                            this.ScoreSheetEntryPenaltyId,
                            this.GameId);

      if (this.Period < 1)
      {
        throw new ArgumentException("Period cannot be less than 1 for:" + locationKey, "Period");
      }

      if (this.Period > 4)
      {
        throw new ArgumentException("Period cannot be more than 4 for:" + locationKey, "Period");
      }

      if (this.PenaltyMinutes < 2)
      {
        throw new ArgumentException("PenaltyMinutes cannot be less than 2 for:" + locationKey, "PenaltyMinutes");
      }
    }
  }
}