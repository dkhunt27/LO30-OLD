using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Game
  {
    [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int GameId { get; set; }

    [Required, ForeignKey("Season")]
    public int SeasonId { get; set; }

    [Required]
    public DateTime GameDateTime { get; set; }

    [Required, MaxLength(15)]
    public string Location { get; set; }

    [Required]
    public bool Playoff { get; set; }

    public virtual Season Season { get; set; }

    public Game()
    {
    }

    public Game(int gid, int sid, DateTime time, string loc, bool play)
    {
      this.GameId = gid;
      this.SeasonId = sid;

      this.GameDateTime = time;
      this.Location = loc;
      this.Playoff = play;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("gid: {0}, sid: {1}",
                            this.GameId,
                            this.SeasonId);
    }
  }
}