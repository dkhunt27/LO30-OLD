using LO30.Data;
using LO30.Data.Objects;
using LO30.Models;
using LO30.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LO30.Controllers
{
  public class ScoreSheetEntryController : Controller
  {
    private Lo30Repository _repo;
    private AccessDatabaseService _accessDbService;
    private Lo30DataService _lo30DataService;

    public ScoreSheetEntryController(Lo30Repository repo)
    {
      _accessDbService = new AccessDatabaseService();
      _lo30DataService = new Lo30DataService();
      _repo = repo;
    }

    [Authorize(Roles = "admin")]
    public ActionResult ScoreSheetEntry()
    {
      return View();
    }

    [Authorize(Roles = "admin")]
    public ActionResult Process()
    {
      DateTime first = DateTime.Now;
      DateTime last = DateTime.Now;
      TimeSpan diffFromFirst = new TimeSpan();
      TimeSpan diffFromLast = new TimeSpan();

      int seasonId = 54;
      bool playoff = false;
      int startingGameId = 3200;
      int endingGameId = 3227;

      Debug.Print("ScoreSheetEntries processing...");
      last = DateTime.Now;
      _repo.ProcessScoreSheetEntries(startingGameId, endingGameId);
      Debug.Print("ScoreSheetEntries processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      Debug.Print("ScoreSheetEntries into GameResults processing...");
      last = DateTime.Now;
      _repo.ProcessScoreSheetEntriesIntoGameResults(startingGameId, endingGameId);
      Debug.Print("ScoreSheetEntries into GameResults processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      Debug.Print("GameResults into TeamStandings processing...");
      last = DateTime.Now;
      _repo.ProcessGameResultsIntoTeamStandings(seasonId, playoff, startingGameId, endingGameId);
      Debug.Print("GameResults into TeamStandings processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());



      Debug.Print("ScoreSheetEntries into PlayerStats processing...");
      last = DateTime.Now;
      _repo.ProcessScoreSheetEntriesIntoPlayerStats(startingGameId, endingGameId);
      Debug.Print("ScoreSheetEntries into PlayerStats processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      diffFromFirst = DateTime.Now - first;
      Debug.Print("Total TimeToProcess: " + diffFromFirst.ToString());

      return Redirect("/ScoreSheetEntry/ScoreSheetEntry");
    }

    [Authorize(Roles = "admin")]
    public ActionResult ProcessForWeb()
    {
      DateTime first = DateTime.Now;
      DateTime last = DateTime.Now;
      TimeSpan diffFromFirst = new TimeSpan();
      TimeSpan diffFromLast = new TimeSpan();

      int seasonId = 54;
      bool playoff = false;
      int startingGameId = 3200;
      int endingGameId = 3227;

      Debug.Print("PlayerStats into WebStats processing...");
      last = DateTime.Now;
      _repo.ProcessPlayerStatsIntoWebStats();
      Debug.Print("PlayerStats into WebStats processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      diffFromFirst = DateTime.Now - first;
      Debug.Print("Total TimeToProcess: " + diffFromFirst.ToString());

      return Redirect("/ScoreSheetEntry/ScoreSheetEntry");
    }

    [Authorize(Roles = "admin")]
    public ActionResult ContextToJson()
    {
      _repo.SaveTablesToJson();

      return Redirect("/ScoreSheetEntry/ScoreSheetEntry");
    }

    [Authorize(Roles = "admin")]
    public ActionResult AccessDbToJson()
    {
      _accessDbService.SaveTablesToJson();

      return Redirect("/ScoreSheetEntry/ScoreSheetEntry");
    }

    [Authorize(Roles = "admin")]
    public ActionResult AccessDbFromJson()
    {
      //_accessDbService.LoadTablesFromJson();

      return Redirect("/ScoreSheetEntry/ScoreSheetEntry");
    }

    [Authorize(Roles = "admin")]
    public ActionResult LoadForWeb()
    {
      var folderPath = "~/Data/SqlServer/";
      List<ForWebPlayerStat> webPlayerStats = _lo30DataService.FromJsonFromFile<List<ForWebPlayerStat>>(folderPath + "ForWebPlayerStats.json");

      return Redirect("/ScoreSheetEntry/ScoreSheetEntry");
    }
  }
}
