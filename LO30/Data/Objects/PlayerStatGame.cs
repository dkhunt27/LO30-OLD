using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class PlayerStatGame
  {
    [Required, Key, Column(Order = 1), ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, Key, Column(Order = 2), ForeignKey("Game")]
    public int GameId { get; set; }

    [Required, ForeignKey("Season")]
    public int SeasonId { get; set; }

    [Required, ForeignKey("SeasonTeamPlayingFor")]
    public int SeasonTeamIdPlayingFor { get; set; }

    [Required]
    public int Line { get; set; }

    [Required]
    public string Position { get; set; }

    [Required]
    public bool Sub { get; set; }

    [Required]
    public int Goals { get; set; }

    [Required]
    public int Assists { get; set; }

    [Required]
    public int Points { get; set; }

    [Required]
    public int PenaltyMinutes { get; set; }

    [Required]
    public int PowerPlayGoals { get; set; }

    [Required]
    public int ShortHandedGoals { get; set; }

    [Required]
    public int GameWinningGoals { get; set; }

    [Required]
    public DateTime UpdatedOn { get; set; }

    public virtual Player Player { get; set; }
    public virtual Season Season { get; set; }
    public virtual SeasonTeam SeasonTeamPlayingFor { get; set; }
    public virtual Game Game { get; set; }

    public PlayerStatGame()
    {
    }

    public PlayerStatGame(int pid, int gid, int sid, int stidpf, int line, string pos, bool sub, int g, int a, int p, int ppg, int shg, int gwg, int pim)
    {
      this.PlayerId = pid;
      this.GameId = gid;
      this.SeasonId = sid;
      this.SeasonTeamIdPlayingFor = stidpf;
      this.Line = line;
      this.Position = pos;
      this.Sub = sub;

      this.Goals = g;
      this.Assists = a;
      this.Points = p;

      this.PowerPlayGoals = ppg;
      this.ShortHandedGoals = shg;
      this.GameWinningGoals = gwg;

      this.PenaltyMinutes = pim;

      this.UpdatedOn = DateTime.Now;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("pid: {0}, gid: {1}, sid: {2}, stIdpf: {3}, sub: {4}",
                                      this.PlayerId,
                                      this.GameId,
                                      this.SeasonId,
                                      this.SeasonTeamIdPlayingFor,
                                      this.Sub);

      if (this.Points != this.Goals + this.Assists)
      {
        throw new ArgumentException("Points must equal Goals + Assists for:" + locationKey, "Points");
      }

      if (this.PowerPlayGoals > this.Goals)
      {
        throw new ArgumentException("PowerPlayGoals must be less than or equal to Goals for:" + locationKey, "PowerPlayGoals");
      }

      if (this.ShortHandedGoals > this.Goals)
      {
        throw new ArgumentException("ShortHandedGoals must be less than or equal to Goals for:" + locationKey, "ShortHandedGoals");
      }

      if (this.GameWinningGoals > this.Goals)
      {
        throw new ArgumentException("GameWinningGoals must be less than or equal to Goals for:" + locationKey, "GameWinningGoals");
      }

      if (this.GameWinningGoals < 0 || this.GameWinningGoals > 1)
      {
        throw new ArgumentException("GameWinningGoals can only be 0 or 1 for:" + locationKey, "GameWinningGoals");
      }

      if (this.Position != "G" && this.Position != "D" && this.Position != "F")
      {
        throw new ArgumentException("Position('" + this.Position + "') must be 'G', 'D', or 'F' for:" + locationKey, "Position");
      }
    }
  }
}