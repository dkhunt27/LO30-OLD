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

    bool ProcessScoreSheetEntries(int startingGameId, int endingGameId);

    bool ProcessScoreSheetEntryPenalties(int startingGameId, int endingGameId);

    bool ProcessScoreSheetEntriesIntoGameResults(int startingGameId, int endingGameId);

    bool ProcessGameResultsIntoTeamStandings(int seasonId, bool playoff, int startingGameId, int endingGameId);

    bool ProcessScoreSheetEntriesIntoPlayerStats(int startingGameId, int endingGameId);
  }
}
