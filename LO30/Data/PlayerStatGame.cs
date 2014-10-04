﻿using System;
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
    }

    public PlayerStatGame(int pid, int pstid, int sid, int stidpf, int gid, int g, int a, int p, int ppg, int shg, int gwg, int pim)
    {
      this.PlayerId = pid;
      this.PlayerStatTypeId = pstid;
      this.SeasonId = sid;
      this.SeasonTeamIdPlayingFor = stidpf;
      this.GameId = gid;

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

    [Key, Column(Order = 1)]
    public int PlayerId { get; set; }

    [Key, Column(Order = 2)]
    public int PlayerStatTypeId { get; set; }

    [Key, Column(Order = 3)]
    public int SeasonId { get; set; }

    [Key, Column(Order = 4)]
    public int SeasonTeamIdPlayingFor { get; set; }

    [Key, Column(Order = 5)]
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

    [Required]
    public DateTime UpdatedOn { get; set; }

    [ForeignKey("PlayerId")]
    public virtual Player Player { get; set; }

    [ForeignKey("PlayerStatTypeId")]
    public virtual PlayerStatType PlayerStatType { get; set; }

    [ForeignKey("SeasonId")]
    public virtual Season Season { get; set; }

    [ForeignKey("SeasonTeamIdPlayingFor")]
    public virtual SeasonTeam SeasonTeamPlayingFor { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    private void Validate()
    {
      var locationKey = string.Format("pid: {0}, pstid: {1}, sid: {2}, stIdpf: {3}, gid: {4}",
                                      this.PlayerId,
                                      this.PlayerStatTypeId,
                                      this.SeasonId,
                                      this.SeasonTeamIdPlayingFor,
                                      this.GameId);

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
    }
  }
}