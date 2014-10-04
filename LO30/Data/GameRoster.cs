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
    public GameRoster()
    {
    }

    public GameRoster(int gid, int stid, int pn, bool g, int pid, bool sub, int? sfpid)
    {
      this.GameId = gid;
      this.SeasonTeamId = stid;
      this.PlayerNumber = pn;

      this.Goalie = g;
      this.PlayerId = pid;
      this.Sub = sub;
      this.SubbingForPlayerId = sfpid;

      Validate();
    }

    public GameRoster(int grid, int gid, int stid, int pn, bool g, int pid, bool sub, int? sfpid)
    {
      this.GameRosterId = grid;

      this.GameId = gid;
      this.SeasonTeamId = stid;
      this.PlayerNumber = pn;

      this.Goalie = g;
      this.PlayerId = pid;
      this.Sub = sub;
      this.SubbingForPlayerId = sfpid;

      Validate();
    }

    [Key, Column(Order = 0)]
    public int GameRosterId { get; set; }

    [Index("PK2", 1, IsUnique = true)]
    public int GameId { get; set; }

    [Index("PK2", 2, IsUnique = true)]
    public int SeasonTeamId { get; set; }

    [Index("PK2", 3, IsUnique = true)]
    public int PlayerNumber { get; set; }

    [Required]
    public bool Goalie { get; set; }

    [Required]
    public int PlayerId { get; set; }

    [Required]
    public bool Sub { get; set; }

    public int? SubbingForPlayerId { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    [ForeignKey("SeasonTeamId")]
    public virtual SeasonTeam SeasonTeam { get; set; }

    [ForeignKey("PlayerId")]
    public virtual Player Player { get; set; }

    [ForeignKey("SubbingForPlayerId")]
    public virtual Player SubbingForPlayer { get; set; }

    private void Validate()
    {
      var locationKey = string.Format("grid: {0}, gid: {1}, stid: {2}, pn: {3}",
                            this.GameRosterId,
                            this.GameId,
                            this.SeasonTeamId,
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