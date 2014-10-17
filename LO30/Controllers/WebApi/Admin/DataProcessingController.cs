using LO30.Areas.AdminDataProcessing;
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
    private AccessDatabaseService _accessDbService;
    private Lo30ContextService _contextService;

    public DataProcessingController(ILo30Repository repo, Lo30Context context)
    {
      _repo = repo;
      _accessDbService = new AccessDatabaseService();
      _contextService = new Lo30ContextService(context);
    }

    public HttpResponseMessage Get()
    {
      return Request.CreateResponse(HttpStatusCode.NotImplemented);
    }

    public HttpResponseMessage Get(int id)
    {
      return Request.CreateResponse(HttpStatusCode.NotImplemented);
    }

    public HttpResponseMessage Post([FromBody]AdminDataProcessingModel model)
    {
      ProcessingResult results = new ProcessingResult();
      switch (model.action)
      {
        case "ProcessScoreSheetEntries":
          results = _repo.ProcessScoreSheetEntries(model.startingGameId, model.endingGameId);
          break;
        case "ProcessScoreSheetEntriesIntoGameResults":
          results = _repo.ProcessScoreSheetEntriesIntoGameResults(model.startingGameId, model.endingGameId);
          break;
        case "ProcessGameResultsIntoTeamStandings":
          results = _repo.ProcessGameResultsIntoTeamStandings(model.seasonId, model.playoff, model.startingGameId, model.endingGameId);
          break;
        case "ProcessScoreSheetEntriesIntoPlayerStats":
          results = _repo.ProcessScoreSheetEntriesIntoPlayerStats(model.startingGameId, model.endingGameId);
          break;
        case "ProcessPlayerStatsIntoWebStats":
          results = _repo.ProcessPlayerStatsIntoWebStats();
          break;
        case "ProcessAll":
          ProcessingResult result1, result2, result3, result4, result5;
          
          result1 = _repo.ProcessScoreSheetEntries(model.startingGameId, model.endingGameId);
          results.error = result1.error;
          results.toProcess = result1.toProcess;
          results.modified = result1.modified;

          if (string.IsNullOrWhiteSpace(results.error))
          {
            result2 = _repo.ProcessScoreSheetEntriesIntoGameResults(model.startingGameId, model.endingGameId);
            results.error = result2.error;
            results.toProcess += result2.toProcess;
            results.modified += result2.modified;
          }

          if (string.IsNullOrWhiteSpace(results.error))
          {
            result3 = _repo.ProcessGameResultsIntoTeamStandings(model.seasonId, model.playoff, model.startingGameId, model.endingGameId);
            results.error = result3.error;
            results.toProcess += result3.toProcess;
            results.modified += result3.modified;
          }

          if (string.IsNullOrWhiteSpace(results.error))
          {
            result4 = _repo.ProcessScoreSheetEntriesIntoPlayerStats(model.startingGameId, model.endingGameId);
            results.error = result4.error;
            results.toProcess += result4.toProcess;
            results.modified += result4.modified;
          }

          if (string.IsNullOrWhiteSpace(results.error))
          {
            result5 = _repo.ProcessPlayerStatsIntoWebStats();
            results.error = result5.error;
            results.toProcess += result5.toProcess;
            results.modified += result5.modified;
          }

          break;
        case "AccessDbToJson":
          results = _accessDbService.SaveTablesToJson();
          break;
        case "LoadScoreSheetEntriesFromAccessDbToJson":
          results = _contextService.LoadScoreSheetEntriesFromAccessDBJson();

          if (string.IsNullOrWhiteSpace(results.error))
          {
            result1 = _contextService.LoadScoreSheetEntryPenaltiesFromAccessDBJson();
            results.error = result1.error;
            results.toProcess += result1.toProcess;
            results.modified += result1.modified;
          }

          break;
        default:
          results = new ProcessingResult() { toProcess = -2, modified = -2, time = "n/a", error = "The model.Action (" + model.action + ") is not implemented!" };
          break;
      }

      return Request.CreateResponse(HttpStatusCode.Accepted, new { results = results });
    }
  }
}
