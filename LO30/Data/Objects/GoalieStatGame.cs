using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class GoalieStatGame
  {
    [Required, Key, Column(Order = 1), ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, Key, Column(Order = 2), ForeignKey("Game")]
    public int GameId { get; set; }

    [Required, ForeignKey("Season")]
    public int SeasonId { get; set; }

    [Required, ForeignKey("SeasonTeam")]
    public int SeasonTeamId { get; set; }

    [Required]
    public bool Playoffs { get; set; }

    [Required]
    public bool Sub { get; set; }

    [Required]
    public int GoalsAgainst { get; set; }

    [Required]
    public int Shutouts { get; set; }

    [Required]
    public int Wins { get; set; }

    [Required]
    public DateTime UpdatedOn { get; set; }

    public virtual Player Player { get; set; }
    public virtual Season Season { get; set; }
    public virtual SeasonTeam SeasonTeam { get; set; }
    public virtual Game Game { get; set; }

    public GoalieStatGame()
    {
    }

    public GoalieStatGame(int pid, int gid, int sid, int stid, bool pfs, bool sub, int ga, int so, int w)
    {
      this.PlayerId = pid;
      this.GameId = gid;
      this.SeasonId = sid;
      this.SeasonTeamId = stid;
      this.Playoffs = pfs;
      this.Sub = sub;

      this.GoalsAgainst = ga;
      this.Shutouts = so;
      this.Wins = w;

      this.UpdatedOn = DateTime.Now;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("pid: {0}, gid: {1}, sid: {2}, stid: {3}, sub: {4}",
                                      this.PlayerId,
                                      this.GameId,
                                      this.SeasonId,
                                      this.SeasonTeamId,
                                      this.Sub);

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