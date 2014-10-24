using LO30.Data.Objects;
using System.Collections.Generic;
using System.Linq;

namespace LO30.Data
{
  public interface ILo30Repository
  {
    #region Data Services
    List<Game> GetGames();
    Game GetGameByGameId(int gameId);

    List<GameTeam> GetGameTeams();
    GameTeam GetGameTeamByGameTeamId(int gameTeamId);
    GameTeam GetGameTeamByGameIdAndHomeTeam(int gameId, bool homeTeam);

    List<GameRoster> GetGameRosters();
    List<GameRoster> GetGameRostersByGameId(int GameId);
    List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int GameId, bool homeTeam);
    GameRoster GetGameRosterByGameRosterId(int gameRosterId);
    GameRoster GetGameRosterByGameTeamIdAndPlayerNumber(int gameTeamId, string playerNumber);
    #endregion

    List<Article> GetArticles();

    List<TeamStanding> GetTeamStandings();

    List<PlayerStatSeason> GetPlayerStatsSeason();

    List<ForWebPlayerStat> GetPlayerStatsForWeb();

    List<ForWebGoalieStat> GetGoalieStatsForWeb();

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
