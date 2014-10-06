using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class GameScore
  {
    [Required, Key, Column(Order = 0)]
    public int GameScoreId { get; set; }

    [Required, ForeignKey("GameTeam"), Index("PK2", 1, IsUnique = true)]
    public int GameTeamId { get; set; }

    [Required, Index("PK2", 2, IsUnique = true)]
    public int Period { get; set; }

    [Required]
    public int Score { get; set; }

    public virtual GameTeam GameTeam { get; set; }

    public GameScore()
    {
    }

    public GameScore(int gsid, int gtid, int per, int score)
    {
      this.GameScoreId = gsid;
      this.GameTeamId = gtid;

      this.Period = per;
      this.Score = score;

      Validate();
    }

    public GameScore(int gtid, int per, int score)
    {
      this.GameTeamId = gtid;

      this.Period = per;
      this.Score = score;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("gsid: {0}, gtid: {1}",
                            this.GameScoreId,
                            this.GameTeamId);

      if (this.Period < 0 && this.Period > 4)
      {
        throw new ArgumentException("Period must be between 0 and 4 for:" + locationKey, "Period");
      }

      if (this.Score < 0)
      {
        throw new ArgumentException("Score must be a positive number for:" + locationKey, "Score");
      }
    }
  }
}