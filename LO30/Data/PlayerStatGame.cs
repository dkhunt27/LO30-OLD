using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class PlayerStatGame
  {
    public PlayerStatGame()
    {
      this.Goals = 0;
      this.Assists = 0;
      this.Points = 0;
      this.PenaltyMinutes = 0;
      this.PowerPlayGoals = 0;
      this.ShortHandedGoals = 0;
      this.GameWinningGoals = 0;

      Validate();
    }

    public PlayerStatGame(int sid, int pid, int pstid, int stidpf, int gid, int g, int a, int p, int ppg, int shg, int gwg, int pim)
    {
      this.SeasonId = sid;
      this.PlayerId = pid;
      this.PlayerStatTypeId = pstid;
      this.SeasonTeamIdPlayingFor = stidpf;
      this.GameId = gid;

      this.Goals = g;
      this.Assists = a;
      this.Points = p;

      this.PowerPlayGoals = ppg;
      this.ShortHandedGoals = shg;
      this.GameWinningGoals = gwg;

      this.PenaltyMinutes = pim;

      Validate();
    }

    [Key, Column(Order = 0)]
    public int SeasonId { get; set; }

    [Key, Column(Order = 1)]
    public int PlayerId { get; set; }

    [Key, Column(Order = 2)]
    public int PlayerStatTypeId { get; set; }

    [Key, Column(Order = 3)]
    public int SeasonTeamIdPlayingFor { get; set; }

    [Key, Column(Order = 4)]
    public int GameId { get; set; }

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

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    [ForeignKey("SeasonId")]
    public virtual Season Season { get; set; }

    [ForeignKey("PlayerId")]
    public virtual Player Player { get; set; }

    [ForeignKey("PlayerStatTypeId")]
    public virtual PlayerStatType PlayerStatType { get; set; }

    [ForeignKey("SeasonTeamIdPlayingFor")]
    public virtual SeasonTeam SeasonTeamPlayingFor { get; set; }

    private void Validate()
    {
      var locationKey = string.Format("gid: {11}, sid: {0}, pid: {1}, pstid: {2}, stIdpf: {3}, g: {4}, a: {5}, p: {6}, ppg: {7}, shg: {8}, gwg: {9}, pim: {10}",
        this.SeasonId,
        this.PlayerId,
        this.PlayerStatTypeId,
        this.SeasonTeamIdPlayingFor,
        this.Goals,
        this.Assists,
        this.Points,
        this.PowerPlayGoals,
        this.ShortHandedGoals,
        this.GameWinningGoals,
        this.PenaltyMinutes,
        this.GameId);

      // make sure points is goals + assists
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

      if (this.GameWinningGoals > 1)
      {
        throw new ArgumentException("GameWinningGoals must be less than or equal to 1 for:" + locationKey, "GameWinningGoals");
      }
    }
  }
}