using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class Lo30RepositoryMock : ILo30Repository
  {
    private Lo30DataSerializationService _lo30DataSerializationService;
    
    private List<ForWebGoalieStat> _webGoalieStats;
    private List<ForWebPlayerStat> _webPlayerStats;
    private List<GameOutcome> _gameOutcomes;
    private List<GameRoster> _gameRosters;
    private List<Game> _games;
    private List<GameScore> _gameScores;
    private List<GameTeam> _gameTeams;

    public Lo30RepositoryMock()
    {
      _lo30DataSerializationService = new Lo30DataSerializationService();

      var folderPath = @"C:\git\LO30\LO30\Data\SqlServer\";
      _webGoalieStats = _lo30DataSerializationService.FromJsonFromFile<List<ForWebGoalieStat>>(folderPath + "ForWebGoalieStats.json");
      _webPlayerStats = _lo30DataSerializationService.FromJsonFromFile<List<ForWebPlayerStat>>(folderPath + "ForWebPlayerStats.json");
      _gameOutcomes = _lo30DataSerializationService.FromJsonFromFile<List<GameOutcome>>(folderPath + "GameOutcomes.json");
      _gameRosters = _lo30DataSerializationService.FromJsonFromFile<List<GameRoster>>(folderPath + "GameRosters.json");
      _games = _lo30DataSerializationService.FromJsonFromFile<List<Game>>(folderPath + "Games.json");
      _gameScores = _lo30DataSerializationService.FromJsonFromFile<List<GameScore>>(folderPath + "GameScores.json");
      _gameTeams = _lo30DataSerializationService.FromJsonFromFile<List<GameTeam>>(folderPath + "GameTeams.json");

    }
    #region Data Services
    public List<Game> GetGames()
    {
      return _games;
    }

    public Game GetGameByGameId(int gameId)
    {
      return _games.Where(x => x.GameId == gameId).FirstOrDefault();
    }

    public List<GameTeam> GetGameTeams()
    {
      return _gameTeams;
    }

    public GameTeam GetGameTeamByGameTeamId(int gameTeamId)
    {
      return _gameTeams.Where(x => x.GameTeamId == gameTeamId).FirstOrDefault();
    }

    public GameTeam GetGameTeamByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return _gameTeams.Where(x => x.GameId == gameId && x.HomeTeam == homeTeam).FirstOrDefault();
    }

    public List<GameRoster> GetGameRosters()
    {
      return _gameRosters;
    }

    public List<GameRoster> GetGameRostersByGameId(int gameId)
    {
      return _gameRosters.Where(x => x.GameTeam.GameId == gameId).ToList();
    }

    public List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return _gameRosters.Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam).ToList();
    }

    public GameRoster GetGameRosterByGameRosterId(int gameRosterId)
    {
      return _gameRosters.Where(x => x.GameRosterId == gameRosterId).FirstOrDefault();
    }

    public GameRoster GetGameRosterByGameTeamIdAndPlayerNumber(int gameTeamId, string playerNumber)
    {
      return _gameRosters.Where(x => x.GameTeamId == gameTeamId && x.PlayerNumber == playerNumber).FirstOrDefault();
    }
    #endregion

    public List<Article> GetArticles()
    {
      throw new NotImplementedException();
    }

    public List<TeamStanding> GetTeamStandings()
    {
      throw new NotImplementedException();
    }

    public List<PlayerStatSeason> GetPlayerStatsSeason()
    {
      throw new NotImplementedException();
    }

    public List<ForWebPlayerStat> GetPlayerStatsForWeb()
    {
      return _webPlayerStats;
    }

    public List<ForWebGoalieStat> GetGoalieStatsForWeb()
    {
      return _webGoalieStats;
    }

    public List<ScoreSheetEntry> GetScoreSheetEntries()
    {
      throw new NotImplementedException();
    }

    public bool Save()
    {
      throw new NotImplementedException();
    }

    public void SaveTablesToJson()
    {
      throw new NotImplementedException();
    }

    public bool AddArticle(Article newArticle)
    {
      throw new NotImplementedException();
    }

    public ProcessingResult ProcessScoreSheetEntries(int startingGameId, int endingGameId)
    {
      throw new NotImplementedException();
    }

    public ProcessingResult ProcessScoreSheetEntryPenalties(int startingGameId, int endingGameId)
    {
      throw new NotImplementedException();
    }

    public ProcessingResult ProcessScoreSheetEntriesIntoGameResults(int startingGameId, int endingGameId)
    {
      throw new NotImplementedException();
    }

    public ProcessingResult ProcessGameResultsIntoTeamStandings(int seasonId, bool playoff, int startingGameId, int endingGameId)
    {
      throw new NotImplementedException();
    }

    public ProcessingResult ProcessScoreSheetEntriesIntoPlayerStats(int startingGameId, int endingGameId)
    {
      throw new NotImplementedException();
    }

    public ProcessingResult ProcessPlayerStatsIntoWebStats()
    {
      throw new NotImplementedException();
    }
  }
}