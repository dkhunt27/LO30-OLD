using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class GameOutcome
  {
    [Required, Key, ForeignKey("GameTeam")]
    public int GameTeamId { get; set; }

    [Required, MaxLength(1)]
    public string Outcome { get; set; }

    [Required]
    public int GoalsFor { get; set; }

    [Required]
    public int GoalsAgainst { get; set; }

    [Required]
    public int PenaltyMinutes { get; set; }

    [Required]
    public bool Override { get; set; }

    [Required, ForeignKey("OpponentGameTeam")]
    public int OpponentGameTeamId { get; set; }

    public virtual GameTeam GameTeam { get; set; }
    public virtual GameTeam OpponentGameTeam { get; set; }

    public GameOutcome()
    {
    }

    public GameOutcome(int gtid, string res, int gf, int ga, int pim, bool over, int ogtid)
    {
      this.GameTeamId = gtid;

      this.Outcome = res;
      this.GoalsFor = gf;
      this.GoalsAgainst = ga;
      this.PenaltyMinutes = pim;
      this.Override = over;
      this.OpponentGameTeamId = ogtid;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("gtid: {0}",
                            this.GameTeamId);

      if (this.GoalsFor < 0)
      {
        throw new ArgumentException("GoalsFor (" + this.GoalsFor + ") must be a positive number for:" + locationKey, "GoalsFor");
      }

      if (this.GoalsAgainst < 0)
      {
        throw new ArgumentException("GoalsAgainst (" + this.GoalsAgainst + ") must be a positive number for:" + locationKey, "GoalsAgainst");
      }

      if (this.PenaltyMinutes < 0)
      {
        throw new ArgumentException("PenaltyMinutes (" + this.PenaltyMinutes + ") must be a positive number for:" + locationKey, "PenaltyMinutes");
      }

      if (this.Outcome != "W" && this.Outcome != "L" && this.Outcome != "T")
      {
        throw new ArgumentException("Outcome (" + this.Outcome + ") must be 'W','L', or 'T' for:" + locationKey, "Outcome");
      }

      if (this.Override == false && this.GoalsFor > this.GoalsAgainst && this.Outcome != "W")
      {
        throw new ArgumentException("Outcome (" + this.Outcome + ") must be a 'W' if GoalsFor > GoalsAgainst without an override for:" + locationKey, "Outcome");
      }

      if (this.Override == false && this.GoalsAgainst > this.GoalsFor && this.Outcome != "L")
      {
        throw new ArgumentException("Outcome (" + this.Outcome + ") must be a 'L' if GoalsAgainst > GoalsFor without an override for:" + locationKey, "Outcome");
      }

      if (this.Override == false && this.GoalsFor == this.GoalsAgainst && this.Outcome != "T")
      {
        throw new ArgumentException("Outcome (" + this.Outcome + ") must be a 'T' if GoalsFor = GoalsAgainst without an override for:" + locationKey, "Outcome");
      }

      if (this.GameTeamId == this.OpponentGameTeamId)
      {
        throw new ArgumentException("OpponentGameTeamId (" + this.OpponentGameTeamId + ") cannot equal GameTeamId for:" + locationKey, "OpponentGameTeamId");
      }
    }
  }
}