using LO30.Data;
using LO30.Data.Objects;
using LO30.Models;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LO30.Controllers.Admin
{
  public class DataProcessingController : ApiController
  {
    private ILo30Repository _repo;
    public DataProcessingController(ILo30Repository repo)
    {
      _repo = repo;
    }

    public HttpResponseMessage Get()
    {
      return Request.CreateResponse(HttpStatusCode.NotImplemented);
    }

    public HttpResponseMessage Get(int id)
    {
      return Request.CreateResponse(HttpStatusCode.NotImplemented);
    }

    public HttpResponseMessage Post([FromBody]DataProcessingModel model)
    {
      int results = -1;
      switch (model.action)
      {
        case "ProcessScoreSheetEntries":
          results = DoWorkProcessScoreSheetEntries(model);
          break;
        default:
          return Request.CreateResponse(HttpStatusCode.BadRequest, "The model.Action (" + model.action + ") is not implemented!");
      }

      if (results < 0)
      {
        return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred during processing. Consult log for details");
      }
      else
      {
        return Request.CreateResponse(HttpStatusCode.Created, new {results = results});
      }
    }


    // do the actual work
    private int DoWorkProcessScoreSheetEntries(DataProcessingModel model)
    {
      int results = -1;
      try
      {
        DateTime last = DateTime.Now;
        TimeSpan diffFromLast = new TimeSpan();

        Debug.Print("ScoreSheetEntries processing...");
        last = DateTime.Now;
        results = _repo.ProcessScoreSheetEntries(model.startingGameId, model.endingGameId);
        Debug.Print("ScoreSheetEntries processed");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      } 
      catch (Exception ex)
      {
        ErrorHandlingService.PrintFullErrorMessage(ex);
        results = -2;
      }

      return results;
    }

    private void DoWorkProcessScoreSheetEntriesIntoGameResults(DataProcessingModel model)
    {
      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      Debug.Print("ScoreSheetEntries into GameResults processing...");
      last = DateTime.Now;
      var results = _repo.ProcessScoreSheetEntriesIntoGameResults(model.startingGameId, model.endingGameId);
      Debug.Print("ScoreSheetEntries into GameResults processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      return;
    }

    private void DoWorkProcessGameResultsIntoTeamStandings(DataProcessingModel model)
    {
      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      Debug.Print("GameResults into TeamStandings processing...");
      last = DateTime.Now;
      var results = _repo.ProcessGameResultsIntoTeamStandings(model.seasonId, model.playoff, model.startingGameId, model.endingGameId);
      Debug.Print("GameResults into TeamStandings processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      return;
    }

    private void DoWorkProcessScoreSheetEntriesIntoPlayerStats(DataProcessingModel model)
    {
      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      Debug.Print("ScoreSheetEntries into PlayerStats processing...");
      last = DateTime.Now;
      var results = _repo.ProcessScoreSheetEntriesIntoPlayerStats(model.startingGameId, model.endingGameId);
      Debug.Print("ScoreSheetEntries into PlayerStats processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      return;
    }

    private void DoWorkProcessPlayerStatsIntoWebStats(DataProcessingModel model)
    {
      DateTime last = DateTime.Now;
      TimeSpan diffFromLast = new TimeSpan();

      Debug.Print("PlayerStats into WebStats processing...");
      last = DateTime.Now;
      var results = _repo.ProcessPlayerStatsIntoWebStats();
      Debug.Print("PlayerStats into WebStats processed");
      diffFromLast = DateTime.Now - last;
      Debug.Print("TimeToProcess: " + diffFromLast.ToString());

      return;
    }

    private void DoWorkProcessAll(DataProcessingModel model)
    {
      DateTime first = DateTime.Now;
      TimeSpan diffFromFirst = new TimeSpan();

      DoWorkProcessScoreSheetEntries(model);
      DoWorkProcessScoreSheetEntriesIntoGameResults(model);
      DoWorkProcessGameResultsIntoTeamStandings(model);
      DoWorkProcessScoreSheetEntriesIntoPlayerStats(model);

      diffFromFirst = DateTime.Now - first;
      Debug.Print("Total TimeToProcess: " + diffFromFirst.ToString());

      return;
    }
  }
}
