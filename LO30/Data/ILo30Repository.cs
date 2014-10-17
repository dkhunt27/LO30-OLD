using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public interface ILo30Repository
  {
    IQueryable<Article> GetArticles();

    IQueryable<TeamStanding> GetTeamStandings();

    IQueryable<PlayerStatSeason> GetPlayerStatsSeason();

    IQueryable<ForWebPlayerStat> GetPlayerStatsForWeb();

    IQueryable<ForWebGoalieStat> GetGoalieStatsForWeb();

    IQueryable<ScoreSheetEntry> GetScoreSheetEntries();

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
