using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class ScoreSheetEntryProcessed
  {
    public ScoreSheetEntryProcessed()
    {
    }

    public ScoreSheetEntryProcessed(int sseid, int gid, int per, bool ht, string time, int gpid, int? a1pid, int? a2pid, int? a3pid, bool shg, bool ppg, bool gwg)
    {
      this.ScoreSheetEntryId = sseid;

      this.GameId = gid;
      this.Period = per;
      this.HomeTeam = ht;
      this.TimeRemaining = time;

      this.GoalPlayerId = gpid;
      this.Assist1PlayerId = a1pid;
      this.Assist2PlayerId = a2pid;
      this.Assist3PlayerId = a3pid;
      this.ShortHandedGoal = shg;
      this.PowerPlayGoal = ppg;
      this.GameWinningGoal = gwg;

      Validate();
    }

    [Key, Column(Order = 0), DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int ScoreSheetEntryId { get; set; }

    [Required]
    public int GameId { get; set; }

    [Required]
    public int Period { get; set; }

    [Required]
    public bool HomeTeam { get; set; }

    [Required]
    public int GoalPlayerId { get; set; }

    public int? Assist1PlayerId { get; set; }

    public int? Assist2PlayerId { get; set; }

    public int? Assist3PlayerId { get; set; }

    [Required, MaxLength(5)]
    public string TimeRemaining { get; set; }

    [Required]
    public bool ShortHandedGoal { get; set; }

    [Required]
    public bool PowerPlayGoal { get; set; }

    [Required]
    public bool GameWinningGoal { get; set; }

    [ForeignKey("GameId")]
    public virtual Game Game { get; set; }

    [ForeignKey("GoalPlayerId")]
    public virtual Player GoalPlayer { get; set; }

    [ForeignKey("Assist1PlayerId")]
    public virtual Player Assist1Player { get; set; }

    [ForeignKey("Assist2PlayerId")]
    public virtual Player Assist2Player { get; set; }

    [ForeignKey("Assist3PlayerId")]
    public virtual Player Assist3Player { get; set; }

    private void Validate()
    {
      var locationKey = string.Format("sseid: {0}, gid: {1}",
                            this.ScoreSheetEntryId,
                            this.GameId);

      if (this.ShortHandedGoal == true && this.PowerPlayGoal == true)
      {
        throw new ArgumentException("ShortHandedGoal and PowerPlayGoal both cannot be true for:" + locationKey, "PowerPlayGoal");
      }

      if (this.Period < 1)
      {
        throw new ArgumentException("Period cannot be less than 1 for:" + locationKey, "Period");
      }

      if (this.Period > 4)
      {
        throw new ArgumentException("Period cannot be more than 4 for:" + locationKey, "Period");
      }

      if (this.GoalPlayerId == this.Assist1PlayerId || this.GoalPlayerId == this.Assist2PlayerId || this.GoalPlayerId == this.Assist3PlayerId)
      {
        throw new ArgumentException("GoalPlayerId cannot also be an Assist#PlayerId for:" + locationKey, "GoalPlayerId");
      }

      if (this.Assist1PlayerId != null && (this.Assist1PlayerId == this.Assist2PlayerId || this.Assist1PlayerId == this.Assist3PlayerId))
      {
        throw new ArgumentException("Assist1PlayerId cannot also be an Assist#PlayerId for:" + locationKey, "Assist1PlayerId");
      }

      if (this.Assist2PlayerId != null && (this.Assist2PlayerId == this.Assist1PlayerId || this.Assist2PlayerId == this.Assist3PlayerId))
      {
        throw new ArgumentException("Assist2PlayerId cannot also be an Assist#PlayerId for:" + locationKey, "Assist2PlayerId");
      }

      if (this.Assist3PlayerId != null && (this.Assist3PlayerId == this.Assist1PlayerId || this.Assist3PlayerId == this.Assist2PlayerId))
      {
        throw new ArgumentException("Assist3PlayerId cannot also be an Assist#PlayerId for:" + locationKey, "Assist3PlayerId");
      }
    }

    public void SaveObjToJsonFile(dynamic obj, string destPath)
    {
      var output = JsonConvert.SerializeObject(obj, Formatting.Indented);

      StringBuilder sb = new StringBuilder();
      sb.Append(output);

      using (StreamWriter outfile = new StreamWriter(destPath))
      {
        outfile.Write(sb.ToString());
      }
    }

    public static List<ScoreSheetEntryProcessed> LoadListFromJsonFile(string filePath)
    {
      string className = "ScoreSheetEntryProcessed";
      string functionName = "LoadFromJsonFile";
      List<ScoreSheetEntry> output = new List<ScoreSheetEntry>();

      Debug.Print(string.Format("{0}: {1} Loading...", functionName, className));
      var start = DateTime.Now;

      string contents = File.ReadAllText(filePath);
      dynamic parsedJson = JsonConvert.DeserializeObject(contents);
      int count = parsedJson.Count;
      Debug.Print(string.Format("{0}: {1} Count:", functionName, className, count));

      for (var d = 0; d < parsedJson.Count; d++)
      {
        if (d > 0 && d % 100 == 0) Debug.Print(string.Format("{0}: {1} Processed:", functionName, className, d));

        var json = parsedJson[d];

        //int? assist1 = null;
        //if (json["ASSIST1"] != null)
        //{
        //  assist1 = json["ASSIST1"];
        //}

        //int? assist2 = null;
        //if (json["ASSIST2"] != null)
        //{
        //  assist2 = json["ASSIST2"];
        //}

        //int? assist3 = null;
        //if (json["ASSIST3"] != null)
        //{
        //  assist3 = json["ASSIST3"];
        //}

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
          GameId = json["GAME_ID"],
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

      Debug.Print(string.Format("{0}: {1} Loaded", functionName, className));
      var end = DateTime.Now - start;
      Debug.Print("TimeToProcess: " + end.ToString());

      return output;
    }
  }
}