using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public partial interface ILo30Repository
  {
    List<Article> GetArticles();

    List<TeamStanding> GetTeamStandings();

    List<PlayerStatSeason> GetPlayerStatsSeason();

    List<ScoreSheetEntry> GetScoreSheetEntries();

    bool Save();

    void SaveTablesToJson();

    bool AddArticle(Article newArticle);

    ProcessingResult ProcessScoreSheetEntries(int startingGameId, int endingGameId);

    ProcessingResult ProcessScoreSheetEntryPenalties(int startingGameId, int endingGameId);

    ProcessingResult ProcessScoreSheetEntriesIntoGameResults(int startingGameId, int endingGameId);

    ProcessingResult ProcessGameResultsIntoTeamStandings(int seasonId, bool playoff, int startingGameId, int endingGameId);

    ProcessingResult ProcessScoreSheetEntriesIntoPlayerStats(int startingGameId, int endingGameId);

    ProcessingResult ProcessPlayerStatsIntoWebStats();
  }
}
