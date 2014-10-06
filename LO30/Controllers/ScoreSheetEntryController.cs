﻿using LO30.Data;
using LO30.Models;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LO30.Controllers
{
  public class ScoreSheetEntryController : Controller
  {
    private ILo30Repository _repo;

    public ScoreSheetEntryController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public ActionResult ScoreSheetEntry()
    {
      return View();
    }

    public ActionResult Process()
    {
      DateTime first = DateTime.Now;
      DateTime last = DateTime.Now;
      TimeSpan diffFromFirst = new TimeSpan();
      TimeSpan diffFromLast = new TimeSpan();

      int seasonId = 54;
      bool playoff = false;
      int startingGameId = 3200;
      int endingGameId = 3220;

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
  }
}
