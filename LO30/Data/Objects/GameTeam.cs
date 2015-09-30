using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class GameTeam
  {
    [Required, Key]
    public int GameTeamId { get; set; }

    [Required, ForeignKey("Game"), Index("PK2", 1, IsUnique=true)]
    public int GameId { get; set; }

    [Required, Index("PK2", 2, IsUnique = true)]
    public bool HomeTeam { get; set; }

    [Required, ForeignKey("SeasonTeam")]
    public int SeasonTeamId { get; set; }

    [Required, ForeignKey("OpponentSeasonTeam")]
    public int OpponentSeasonTeamId { get; set; }

    public virtual Game Game { get; set; }
    public virtual SeasonTeam SeasonTeam { get; set; }
    public virtual SeasonTeam OpponentSeasonTeam { get; set; }

    public GameTeam()
    {
    }

    public GameTeam(int gid, bool ht, int stid, int ostid)
    {
      this.GameId = gid;
      this.HomeTeam = ht;
      this.SeasonTeamId = stid;
      this.OpponentSeasonTeamId = ostid;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("gtid: {0}, gid: {1}, ht: {2}, stid: {3}",
                            this.GameTeamId,
                            this.GameId,
                            this.HomeTeam,
                            this.SeasonTeamId);

      if (this.SeasonTeamId == this.OpponentSeasonTeamId)
      {
        throw new ArgumentException("SeasonTeamId cannot equal OpponentSeasonTeamId:" + locationKey, "SeasonTeamId");
      }

    }
  }
}