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
  public class ScoreSheetEntryPenalty
  {
    [Required, Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntryPenaltyId { get; set; }

    [Required, ForeignKey("Game")]
    public int GameId { get; set; }

    [Required]
    public int Period { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required]
    public string Player { get; set; }

    [Required, MaxLength(3)]
    public string PenaltyCode { get; set; }

    [Required, MaxLength(5)]
    public string TimeRemaining { get; set; }
    
    [Required]
    public int PenaltyMinutes { get; set; }

    public virtual Game Game { get; set; }

    public static List<ScoreSheetEntryPenalty> LoadListFromAccessDbJsonFile(string filePath)
    {
      string className = "ScoreSheetEntryPenalty";
      string functionName = "LoadListFromAccessDbJsonFile";
      List<ScoreSheetEntryPenalty> output = new List<ScoreSheetEntryPenalty>();

      Debug.Print(string.Format("{0}: {1} Loading...", functionName, className));
      var start = DateTime.Now;

      string contents = File.ReadAllText(filePath);
      dynamic parsedJson = JsonConvert.DeserializeObject(contents);
      int count = parsedJson.Count;
      Debug.Print(string.Format("{0}: {1} Count: {2}", functionName, className, count));

      for (var d = 0; d < parsedJson.Count; d++)
      {
        if (d > 0 && d % 100 == 0) Debug.Print(string.Format("{0}: {1} Processed: {2}", functionName, className, d));

        var json = parsedJson[d];

        bool homeTeam = true;
        string teamJson = json["TEAM"];
        string team = teamJson.ToLower();
        if (team == "2" || team == "v" || team == "a" || team == "g")
        {
          homeTeam = false;
        }

        output.Add(new ScoreSheetEntryPenalty()
        {
          ScoreSheetEntryPenaltyId = json["SCORE_SHEET_ENTRY_PENALTY_ID"],
          GameId = json["GAME_ID"],
          Period = json["PERIOD"],
          HomeTeam = homeTeam,
          Player = json["PLAYER"],
          PenaltyCode = json["PENALTY_CODE"],
          TimeRemaining = json["TIME_REMAINING"],
          PenaltyMinutes = json["PENALTY_MINUTES"]
        });
      }

      Debug.Print(string.Format("{0}: {1} Loaded", functionName, className));
      var end = DateTime.Now - start;
      Debug.Print("TimeToProcess: " + end.ToString());

      return output;
    }
  }
}