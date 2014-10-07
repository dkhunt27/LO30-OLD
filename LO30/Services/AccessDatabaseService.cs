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
  class TableList
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

    public void ProcessAccessTableToJsonFile(string queryBegin, string queryEnd, string table, string file)
    {
      Debug.Print("ProcessAccessTableToJsonFile: Processing " + table);
      var last = DateTime.Now;

      var sql = queryBegin + " " + table + " " + queryEnd;
      var dsView = new DataSet();
      var adp = new OleDbDataAdapter(sql, _connString);
      adp.Fill(dsView, "AccessData");
      adp.Dispose();
      var tbl = dsView.Tables["AccessData"];

      SaveObjToJsonFile(tbl, _folderPath + file + ".json");

      Debug.Print("ProcessAccessTableToJsonFile: Processed " + table);
      var diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());
    }

    public void SaveTablesToJson()
    {
      DateTime first = DateTime.Now;
      DateTime last = DateTime.Now;
      TimeSpan diffFromFirst = new TimeSpan();

      var connString = System.Configuration.ConfigurationManager.ConnectionStrings["LO30AccessDB"].ConnectionString;

      List<TableList> accessTables = new List<TableList>()
      {
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="GAME", FileName="Games"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="GAME_ROSTER", FileName="GameRosters"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="PENALTY_DETAIL", FileName="PenaltyDetails"},
        new TableList(){QueryBegin="SELECT PLAYER_ID, PLAYER_FIRST_NAME, PLAYER_LAST_NAME, PLAYER_SUFFIX, PLAYER_POSITION, SHOOTS FROM", QueryEnd="", TableName="PLAYER", FileName="Players"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="PLAYER_RATING", FileName="PlayerRatings"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="PLAYER_STATUS", FileName="PlayerStatuses"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="REF_PENALTY", FileName="Penalties"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="REF_SEASON", FileName="Seasons"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="REF_STATUS", FileName="Statuses"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="SCORE_SHEET_ENTRY", FileName="ScoreSheetEntries"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="SCORE_SHEET_ENTRY_PENALTY", FileName="ScoreSheetEntryPenalties"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="SCORING_DETAIL", FileName="ScoringDetails"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="TEAM", FileName="Teams"},
        new TableList(){QueryBegin="SELECT * FROM", QueryEnd="", TableName="TEAM_ROSTER", FileName="Team_Rosters"}
      };

      foreach (var table in accessTables)
      {
        ProcessAccessTableToJsonFile(table.QueryBegin, table.QueryEnd, table.TableName, table.FileName);
      }

      diffFromFirst = DateTime.Now - first;
      Debug.Print("Total TimeToProcess: " + diffFromFirst.ToString());

    }
  }
}
