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
  public class ScoreSheetEntrySub
  {
    [Required, Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntrySubId { get; set; }

    [Required, ForeignKey("Game")]
    public int GameId { get; set; }

    [Required]
    public int SubPlayerId { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required]
    public int SubbingForPlayerId { get; set; }

    [Required]
    public string JerseyNumber { get; set; }

    [Required]
    public DateTime UpdatedOn { get; set; }

    public virtual Game Game { get; set; }

    public static List<ScoreSheetEntrySub> LoadListFromAccessDbJsonFile(string filePath, int startingGameIdToProcess, int endingGameIdToProcess)
    {
      string className = "ScoreSheetEntrySub";
      string functionName = "LoadListFromAccessDbJsonFile";
      List<ScoreSheetEntrySub> output = new List<ScoreSheetEntrySub>();

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
          int team = json["TEAM"];
          if (team == 2)
          {
            homeTeam = false;
          }

          int seasonId = json["SEASON_ID"];
          string jersey = json["JERSEY"];
          int subId = json["SUB_ID"];
          int subForId = json["SUB_FOR_ID"];
          DateTime updatedOn = json["UPDATED_ON"];

          output.Add(new ScoreSheetEntrySub()
          {
            ScoreSheetEntrySubId = json["SCORE_SHEET_ENTRY_SUB_ID"],
            GameId = gameId,
            SubPlayerId = subId,
            HomeTeam = homeTeam,
            SubbingForPlayerId = subForId,
            JerseyNumber = jersey,
            UpdatedOn = updatedOn
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