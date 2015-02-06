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
  public class ScoreSheetEntry
  {
    [Required, Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntryId { get; set; }

    [Required, ForeignKey("Game")]
    public int GameId { get; set; }

    [Required]
    public int Period { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required]
    public string Goal { get; set; }

    public string Assist1 { get; set; }

    public string Assist2 { get; set; }

    public string Assist3 { get; set; }

    [Required, MaxLength(5)]
    public string TimeRemaining { get; set; }

    [MaxLength(2)]
    public string ShortHandedPowerPlay { get; set; }

    public virtual Game Game { get; set; }

    public static List<ScoreSheetEntry> LoadListFromAccessDbJsonFile(string filePath, int startingGameIdToProcess, int endingGameIdToProcess)
    {
      string className = "ScoreSheetEntry";
      string functionName = "LoadListFromAccessDbJsonFile";
      List<ScoreSheetEntry> output = new List<ScoreSheetEntry>();

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
        int gameId = json["GAME_ID"];

        if (gameId >= startingGameIdToProcess && gameId <= endingGameIdToProcess)
        {
          bool homeTeam = true;
          string teamJson = json["TEAM"];
          string team = teamJson.ToLower();
          if (team == "2" || team == "v" || team == "a" || team == "g")
          {
            homeTeam = false;
          }

          output.Add(new ScoreSheetEntry()
          {
            ScoreSheetEntryId = json["SCORE_SHEET_ENTRY_ID"],
            GameId = gameId,
            Period = json["PERIOD"],
            HomeTeam = homeTeam,
            Goal = json["GOAL"],
            Assist1 = json["ASSIST1"],
            Assist2 = json["ASSIST2"],
            Assist3 = json["ASSIST3"],
            TimeRemaining = json["TIME_REMAINING"],
            ShortHandedPowerPlay = json["SH_PP"],
          });
        }
      }

      Debug.Print(string.Format("{0}: {1} Loaded {2} Record(s)", functionName, className, output.Count));
      var end = DateTime.Now - start;
      Debug.Print("TimeToProcess: " + end.ToString());

      return output;
    }
  }
}