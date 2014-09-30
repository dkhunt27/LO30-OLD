using LO30.Data;
using LO30.Models;
using LO30.Services;
using System;
using System.Collections.Generic;
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
      _repo.ProcessScoreSheetEntriesIntoGameResults(3200, 3213);
      _repo.ProcessGameResultsIntoTeamStandings(54, 1, 3200, 3213);

      return Redirect("/ScoreSheetEntry/ScoreSheetEntry");
    }
  }
}
