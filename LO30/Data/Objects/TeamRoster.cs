using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class TeamRoster
  {
    [Required, Key, Column(Order = 1), ForeignKey("SeasonTeam")]
    public int SeasonTeamId { get; set; }

    [Required, Key, Column(Order = 2), ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, Key, Column(Order = 3)]
    public int StartYYYYMMDD { get; set; }

    [Required, Key, Column(Order = 4)]
    public int EndYYYYMMDD { get; set; }

    [Required]
    public string Position { get; set; }

    [Required]
    public int RatingPrimary { get; set; }

    [Required]
    public int RatingSecondary { get; set; }

    [Required]
    public int Line { get; set; }

    [MaxLength(3)]
    public string PlayerNumber { get; set; }

    public virtual SeasonTeam SeasonTeam { get; set; }
    public virtual Player Player { get; set; }

    public TeamRoster()
    {
    }

    public TeamRoster(int stid, int pid, int symd, int eymd, string pos, int rp, int rs, int line, int pn)
      : this(stid, pid, symd, eymd, pos, rp, rs, line, pn.ToString())
    {
    }

    public TeamRoster(int stid, int pid, int symd, int eymd, string pos, int rp, int rs, int line, string pn)
    {
      this.SeasonTeamId = stid;
      this.PlayerId = pid;
      this.StartYYYYMMDD = symd;
      this.EndYYYYMMDD = eymd;

      this.Position = pos;
      this.RatingPrimary = rp;
      this.RatingSecondary = rs;
      this.Line = line;
      this.PlayerNumber = pn;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("stid: {0}, pid: {1}, symd: {2}, eymd: {3}",
                            this.SeasonTeamId,
                            this.PlayerId,
                            this.StartYYYYMMDD,
                            this.EndYYYYMMDD);

      if (this.Position != "G" && this.Position != "D" && this.Position != "F")
      {
        throw new ArgumentException("Position('" + this.Position + "') must be 'G', 'D', or 'F' for:" + locationKey, "Position");
      }

      if (this.Line < 1 || this.Line > 3)
      {
        throw new ArgumentException("Line(" + this.Line + ") must be between 1 and 3:" + locationKey, "Line");
      }

      if (this.RatingPrimary < 0 || this.RatingPrimary > 9)
      {
        throw new ArgumentException("RatingPrimary(" + this.RatingPrimary + ") must be between 0 and 9:" + locationKey, "RatingPrimary");
      }

      if (this.RatingSecondary < 0 || this.RatingSecondary > 8)
      {
        throw new ArgumentException("RatingSecondary(" + this.RatingSecondary + ") must be between 0 and 8:" + locationKey, "RatingSecondary");
      }

      int playerNumber = -1;
      if (!int.TryParse(this.PlayerNumber, out playerNumber))
      {
        throw new ArgumentException("PlayerNumber(" + this.PlayerNumber + ") must be a number:" + locationKey, "PlayerNumber");
      }

      if (playerNumber < 0 || playerNumber > 99)
      {
        throw new ArgumentException("PlayerNumber(" + this.PlayerNumber + ") must be between 0 and 99:" + locationKey, "PlayerNumber");
      }
    }
  }
}