using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LO30.Data
{
  public interface ILo30Repository
  {
    IQueryable<Article> GetArticles();

    IQueryable<TeamStanding> GetTeamStandings();

    bool Save();

    bool AddArticle(Article newArticle);

    bool ProcessScoreSheetEntries(int startingGameId, int endingGameId);

    bool ProcessScoreSheetEntryPenalties(int startingGameId, int endingGameId);

    bool ProcessScoreSheetEntriesIntoGameResults(int startingGameId, int endingGameId);

    bool ProcessGameResultsIntoTeamStandings(int seasonId, int seasonTypeId, int startingGameId, int endingGameId);

    bool ProcessScoreSheetEntriesIntoPlayerStats(int startingGameId, int endingGameId);
  }
}
