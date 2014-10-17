using LO30.Data.Objects;
using LO30.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace LO30.Services
{
  class AccessTableList
  {
    public string QueryBegin { get; set; }
    public string QueryEnd { get; set; }
    public string TableName { get; set; }
    public string FileName { get; set; }
  }

  public class AccessDatabaseService
  {
    private string _folderPath;
    private string _connString;

    public AccessDatabaseService()
    {
      _connString = System.Configuration.ConfigurationManager.ConnectionStrings["LO30AccessDB"].ConnectionString;
      _folderPath = "C:\\git\\LO30\\LO30\\Data\\Access\\";
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

    public dynamic ParseObjectFromJsonFile(string srcPath)
    {
      string contents = File.ReadAllText(srcPath);
      dynamic parsedJson = JsonConvert.DeserializeObject(contents);
      return parsedJson;
    }

    public ProcessingResult ProcessAccessTableToJsonFile(string queryBegin, string queryEnd, string table, string file)
    {
      var result = new ProcessingResult();

      Debug.Print("ProcessAccessTableToJsonFile: Processing " + table);
      var last = DateTime.Now;

      var sql = queryBegin + " " + table + " " + queryEnd;
      var dsView = new DataSet();
      var adp = new OleDbDataAdapter(sql, _connString);
      adp.Fill(dsView, "AccessData");
      adp.Dispose();
      var tbl = dsView.Tables["AccessData"];

      result.toProcess = tbl.Rows.Count;

      SaveObjToJsonFile(tbl, _folderPath + file + ".json");

      result.modified = tbl.Rows.Count;

      Debug.Print("ProcessAccessTableToJsonFile: Processed " + table);
      var diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      return result;
    }

    public ProcessingResult SaveTablesToJson()
    {
      var results = new ProcessingResult();
      results.toProcess = 0;
      results.modified = 0;

      DateTime first = DateTime.Now;
      DateTime last = DateTime.Now;
      TimeSpan diffFromFirst = new TimeSpan();

      var connString = System.Configuration.ConfigurationManager.ConnectionStrings["LO30AccessDB"].ConnectionString;

      List<AccessTableList> accessTables = new List<AccessTableList>()
      {
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="WHERE SEASON_ID=54", TableName="GAME", FileName="Games"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="WHERE SEASON_ID=54", TableName="GAME_ROSTER", FileName="GameRosters"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="WHERE SEASON_ID=54", TableName="PENALTY_DETAIL", FileName="PenaltyDetails"},
        new AccessTableList(){QueryBegin="SELECT PLAYER_ID, PLAYER_FIRST_NAME, PLAYER_LAST_NAME, PLAYER_SUFFIX, PLAYER_POSITION, SHOOTS FROM", QueryEnd="", TableName="PLAYER", FileName="Players"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="WHERE SEASON_ID=54", TableName="PLAYER_RATING", FileName="PlayerRatings"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="PLAYER_STATUS", FileName="PlayerStatuses"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="REF_PENALTY", FileName="Penalties"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="REF_SEASON", FileName="Seasons"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="REF_STATUS", FileName="Statuses"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="SCORE_SHEET_ENTRY", FileName="ScoreSheetEntries"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="SCORE_SHEET_ENTRY_PENALTY", FileName="ScoreSheetEntryPenalties"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="WHERE SEASON_ID=54", TableName="SCORING_DETAIL", FileName="ScoringDetails"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="TEAM", FileName="Teams"},
        new AccessTableList(){QueryBegin="SELECT * FROM", QueryEnd="WHERE SEASON_ID=54", TableName="TEAM_ROSTER", FileName="Team_Rosters"}
      };

      foreach (var table in accessTables)
      {
        var result = ProcessAccessTableToJsonFile(table.QueryBegin, table.QueryEnd, table.TableName, table.FileName);

        results.error = result.error;
        results.toProcess += result.toProcess;
        results.modified += result.modified;

        if (!string.IsNullOrWhiteSpace(result.error))
        {
          break;
        }
      }

      diffFromFirst = DateTime.Now - first;
      Debug.Print("Total TimeToProcess: " + diffFromFirst.ToString());
      results.time = diffFromFirst.ToString();

      return results;
    }
  }
}
