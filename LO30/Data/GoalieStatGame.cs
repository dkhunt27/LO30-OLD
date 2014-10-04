using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class GoalieStatGame
  {
    public GoalieStatGame()
    {

    }

    public GoalieStatGame(int pid, int pstid, int sid, int stidpf, int gid, int ga, int so, int w)
    {
      this.PlayerId = pid;
      this.PlayerStatTypeId = pstid;
      this.SeasonId = sid;
      this.SeasonTeamIdPlayingFor = stidpf;
      this.GameId = gid;

      this.GoalsAgainst = ga;
      this.Shutouts = so;
      this.Wins = w;

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
    public int GoalsAgainst { get; set; }

    [Required]
    public int Shutouts { get; set; }

    [Required]
    public int Wins { get; set; }

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

      if (this.Shutouts < 0 || this.Shutouts > 1)
      {
        throw new ArgumentException("Shutouts can only be 0 or 1 for:" + locationKey, "Shutouts");
      }

      if (this.Wins < 0 || this.Wins > 1)
      {
        throw new ArgumentException("Wins can only be 0 or 1 for:" + locationKey, "Wins");
      }
    }
  }
}