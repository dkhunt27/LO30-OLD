using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class GameRoster
  {
    [Required, Key]
    public int GameRosterId { get; set; }

    [Required, ForeignKey("GameTeam"), Index("PK2", 1, IsUnique = true)]
    public int GameTeamId { get; set; }

    [Required, Index("PK2", 2, IsUnique = true), MaxLength(3)]
    public string PlayerNumber { get; set; }

    [Required]
    public int Line { get; set; }

    [Required]
    public string Position { get; set; }

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

    public GameRoster(int gtid, int pn, int line, string pos, bool g, int pid, bool sub, int? sfpid):
      this(0, gtid, pn, line, pos, g, pid, sub, sfpid)
    {
    }

    public GameRoster(int grid, int gtid, int pn, int line, string pos, bool g, int pid, bool sub, int? sfpid) :
      this(grid, gtid, pn.ToString(), line, pos, g, pid, sub, sfpid)
    {

    }
    public GameRoster(int gtid, string pn, int line, string pos, bool g, int pid, bool sub, int? sfpid):
      this(0, gtid, pn, line, pos, g, pid, sub, sfpid)
    {
    }

    public GameRoster(int grid, int gtid, string pn, int line, string pos, bool g, int pid, bool sub, int? sfpid)
    {
      this.GameRosterId = grid;

      this.GameTeamId = gtid;
      this.PlayerNumber = pn;

      this.Line = line;
      this.Position = pos;
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

      if (this.Position != "G" && this.Position != "D" && this.Position != "F")
      {
        throw new ArgumentException("Position('" + this.Position + "') must be 'G', 'D', or 'F' for:" + locationKey, "Position");
      }

      if (this.Position == "G" && this.Goalie != true)
      {
        throw new ArgumentException("If Position = 'G', Goalie must be true:" + locationKey, "Goalie");
      }
    }
  }
}