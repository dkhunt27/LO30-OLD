using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class ScoreSheetEntryProcessed
  {
    [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntryId { get; set; }

    [Required, ForeignKey("Game")]
    public int GameId { get; set; }

    [Required]
    public int Period { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required, ForeignKey("GameTeam")]
    public int GameTeamId { get; set; }

    [Required, ForeignKey("GoalPlayer")]
    public int GoalPlayerId { get; set; }

    [ForeignKey("Assist1Player")]
    public int? Assist1PlayerId { get; set; }

    [ForeignKey("Assist2Player")]
    public int? Assist2PlayerId { get; set; }

    [ForeignKey("Assist3Player")]
    public int? Assist3PlayerId { get; set; }

    [Required, MaxLength(5)]
    public string TimeRemaining { get; set; }

    [Required]
    public bool ShortHandedGoal { get; set; }

    [Required]
    public bool PowerPlayGoal { get; set; }

    [Required]
    public bool GameWinningGoal { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual Game Game { get; set; }
    public virtual GameTeam GameTeam { get; set; }
    public virtual Player GoalPlayer { get; set; }
    public virtual Player Assist1Player { get; set; }
    public virtual Player Assist2Player { get; set; }
    public virtual Player Assist3Player { get; set; }

    public ScoreSheetEntryProcessed()
    {
    }

    public ScoreSheetEntryProcessed(int sseid, int gid, int per, bool ht, string time, int gtid, int gpid, int? a1pid, int? a2pid, int? a3pid, bool shg, bool ppg, bool gwg, DateTime upd)
    {
      this.ScoreSheetEntryId = sseid;

      this.GameId = gid;
      this.Period = per;
      this.HomeTeam = ht;
      this.TimeRemaining = time;
      this.GameTeamId = gtid;

      this.GoalPlayerId = gpid;
      this.Assist1PlayerId = a1pid;
      this.Assist2PlayerId = a2pid;
      this.Assist3PlayerId = a3pid;
      this.ShortHandedGoal = shg;
      this.PowerPlayGoal = ppg;
      this.GameWinningGoal = gwg;

      this.UpdatedOn = upd;

      Validate();
    }


    private void Validate()
    {
      var locationKey = string.Format("sseid: {0}, gid: {1}",
                            this.ScoreSheetEntryId,
                            this.GameId);

      if (this.ShortHandedGoal == true && this.PowerPlayGoal == true)
      {
        throw new ArgumentException("ShortHandedGoal and PowerPlayGoal both cannot be true for:" + locationKey, "PowerPlayGoal");
      }

      if (this.Period < 1)
      {
        throw new ArgumentException("Period cannot be less than 1 for:" + locationKey, "Period");
      }

      if (this.Period > 4)
      {
        throw new ArgumentException("Period cannot be more than 4 for:" + locationKey, "Period");
      }

      if ((this.GoalPlayerId != null && this.GoalPlayerId != 0) && (this.GoalPlayerId == this.Assist1PlayerId || this.GoalPlayerId == this.Assist2PlayerId || this.GoalPlayerId == this.Assist3PlayerId))
      {
        throw new ArgumentException("GoalPlayerId cannot also be an Assist#PlayerId for:" + locationKey, "GoalPlayerId");
      }

      if ((this.Assist1PlayerId != null && this.Assist1PlayerId != 0) && (this.Assist1PlayerId == this.Assist2PlayerId || this.Assist1PlayerId == this.Assist3PlayerId))
      {
        throw new ArgumentException("Assist1PlayerId cannot also be an Assist#PlayerId for:" + locationKey, "Assist1PlayerId");
      }

      if ((this.Assist2PlayerId != null && this.Assist2PlayerId != 0) && (this.Assist2PlayerId == this.Assist1PlayerId || this.Assist2PlayerId == this.Assist3PlayerId))
      {
        throw new ArgumentException("Assist2PlayerId cannot also be an Assist#PlayerId for:" + locationKey, "Assist2PlayerId");
      }

      // Can't happen, covered by other 2 assist checks
      //if ((this.Assist3PlayerId != null && this.Assist3PlayerId != 0) && (this.Assist3PlayerId == this.Assist1PlayerId || this.Assist3PlayerId == this.Assist2PlayerId))
      //{
      //  throw new ArgumentException("Assist3PlayerId cannot also be an Assist#PlayerId for:" + locationKey, "Assist3PlayerId");
      //}
    }
  }
}