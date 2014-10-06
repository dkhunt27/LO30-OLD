using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class GameRoster
  {
    [Key, Column(Order = 0)]
    public int GameRosterId { get; set; }

    [Required, ForeignKey("GameTeam"), Index("PK2", 1, IsUnique = true)]
    public int GameTeamId { get; set; }

    [Required, Index("PK2", 2, IsUnique = true)]
    public int PlayerNumber { get; set; }

    [Required]
    public bool Goalie { get; set; }

    [Required, ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required]
    public bool Sub { get; set; }

    [ForeignKey("SubbingForPlayer")]
    public int? SubbingForPlayerId { get; set; }

    public virtual GameTeam GameTeam { get; set; }
    public virtual Player Player { get; set; }
    public virtual Player SubbingForPlayer { get; set; }

    public GameRoster()
    {
    }

    public GameRoster(int gtid, int pn, bool g, int pid, bool sub, int? sfpid)
    {
      this.GameTeamId = gtid;
      this.PlayerNumber = pn;

      this.Goalie = g;
      this.PlayerId = pid;
      this.Sub = sub;
      this.SubbingForPlayerId = sfpid;

      Validate();
    }

    public GameRoster(int grid, int gtid, int pn, bool g, int pid, bool sub, int? sfpid)
    {
      this.GameRosterId = grid;

      this.GameTeamId = gtid;
      this.PlayerNumber = pn;

      this.Goalie = g;
      this.PlayerId = pid;
      this.Sub = sub;
      this.SubbingForPlayerId = sfpid;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("grid: {0}, gtid: {1}, pn: {2}",
                            this.GameRosterId,
                            this.GameTeamId,
                            this.PlayerNumber);

      if (this.Sub == true && this.SubbingForPlayerId == null)
      {
        throw new ArgumentException("If Sub is true, SubbingForPlayerId must be populated for:" + locationKey, "SubbingForPlayerId");
      }

      if (this.Sub == false && this.SubbingForPlayerId != null)
      {
        throw new ArgumentException("If Sub is false, SubbingForPlayerId must not be populated for:" + locationKey, "SubbingForPlayerId");
      }
    }
  }
}