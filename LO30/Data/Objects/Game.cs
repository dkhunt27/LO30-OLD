using LO30.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class Game
  {
    private Lo30DataService _lo30DataService;

    [Required, Key, DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int GameId { get; set; }

    [Required, ForeignKey("Season")]
    public int SeasonId { get; set; }

    [Required]
    public DateTime GameDateTime { get; set; }

    [Required]
    public int GameYYYYMMDD { get; set; }

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
      this.GameYYYYMMDD = ConvertDateTimeIntoYYYYMMDD(time, ifNullReturnMax: false);
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

    public int ConvertDateTimeIntoYYYYMMDD(DateTime? toConvert, bool ifNullReturnMax)
    {
      var lo30DataService = new Lo30DataService();
      return lo30DataService.ConvertDateTimeIntoYYYYMMDD(toConvert, ifNullReturnMax);
    }
  }
}