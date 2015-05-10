using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    int currentSeasonId = 54;

    private Lo30DataService _lo30DataService;
    private Lo30DataSerializationService _lo30DataSerializationService;
    
    private List<ForWebGoalieStat> _webGoalieStats;
    private List<ForWebPlayerStat> _webPlayerStats;
    private List<ForWebTeamStanding> _webTeamStandings;
    private List<GameOutcome> _gameOutcomes;
    private List<GameRoster> _gameRosters;
    private List<Game> _games;
    private List<GameScore> _gameScores;
    private List<GameTeam> _gameTeams;
    private List<GoalieStatGame> _goalieStatsGame;
    private List<GoalieStatSeason> _goalieStatsSeason;
    private List<GoalieStatSeasonTeam> _goalieStatsSeasonTeam;
    private List<Player> _players;
    private List<PlayerRating> _playerRatings;
    private List<PlayerStatCareer> _playerStatsCareer;
    private List<PlayerStatGame> _playerStatsGame;
    private List<PlayerStatSeason> _playerStatsSeason;
    private List<PlayerStatSeasonTeam> _playerStatsSeasonTeam;
    private List<PlayerStatus> _playerStatuses;
    private List<ScoreSheetEntryProcessed> _scoreSheetEntriesProcessed;
    private List<ScoreSheetEntryPenaltyProcessed> _scoreSheetEntryPenaltiesProcessed;
    private List<Season> _seasons;
    private List<TeamRoster> _teamRosters;


    public Lo30RepositoryMock()
    {
      _lo30DataService = new Lo30DataService();
      _lo30DataSerializationService = new Lo30DataSerializationService();

      //string appRoot = Environment.GetEnvironmentVariable("RoleRoot");
      //string folderPath2 = Path.Combine(appRoot + @"\", string.Format(@"approot\{0}", "ForWebGoalieStats.json"));

      var folderPath = @"C:\git\LO30\LO30\App_Data\SqlServer\";
      _webGoalieStats = _lo30DataSerializationService.FromJsonFromFile<List<ForWebGoalieStat>>(folderPath + "ForWebGoalieStats.json");
      _webPlayerStats = _lo30DataSerializationService.FromJsonFromFile<List<ForWebPlayerStat>>(folderPath + "ForWebPlayerStats.json");
      _webTeamStandings = _lo30DataSerializationService.FromJsonFromFile<List<ForWebTeamStanding>>(folderPath + "ForWebTeamStandings.json");
      _gameOutcomes = _lo30DataSerializationService.FromJsonFromFile<List<GameOutcome>>(folderPath + "GameOutcomes.json");
      _gameRosters = _lo30DataSerializationService.FromJsonFromFile<List<GameRoster>>(folderPath + "GameRosters.json");
      _games = _lo30DataSerializationService.FromJsonFromFile<List<Game>>(folderPath + "Games.json");
      _gameScores = _lo30DataSerializationService.FromJsonFromFile<List<GameScore>>(folderPath + "GameScores.json");
      _gameTeams = _lo30DataSerializationService.FromJsonFromFile<List<GameTeam>>(folderPath + "GameTeams.json");
      _goalieStatsGame = _lo30DataSerializationService.FromJsonFromFile<List<GoalieStatGame>>(folderPath + "GoalieStatsGame.json");
      _goalieStatsSeason = _lo30DataSerializationService.FromJsonFromFile<List<GoalieStatSeason>>(folderPath + "GoalieStatsSeason.json");
      _goalieStatsSeasonTeam = _lo30DataSerializationService.FromJsonFromFile<List<GoalieStatSeasonTeam>>(folderPath + "GoalieStatsSeasonTeam.json");
      _players = _lo30DataSerializationService.FromJsonFromFile<List<Player>>(folderPath + "Players.json");
      _playerRatings = _lo30DataSerializationService.FromJsonFromFile<List<PlayerRating>>(folderPath + "PlayerRatings.json");
      _playerStatsCareer = _lo30DataSerializationService.FromJsonFromFile<List<PlayerStatCareer>>(folderPath + "PlayerStatsCareer.json");
      _playerStatsGame = _lo30DataSerializationService.FromJsonFromFile<List<PlayerStatGame>>(folderPath + "PlayerStatsGame.json");
      _playerStatsSeason = _lo30DataSerializationService.FromJsonFromFile<List<PlayerStatSeason>>(folderPath + "PlayerStatsSeason.json");
      _playerStatsSeasonTeam = _lo30DataSerializationService.FromJsonFromFile<List<PlayerStatSeasonTeam>>(folderPath + "PlayerStatsSeasonTeam.json");
      _playerStatuses = _lo30DataSerializationService.FromJsonFromFile<List<PlayerStatus>>(folderPath + "PlayerStatuses.json");

      // TODO, mock this data
      //_scoreSheetEntriesProcessed = _lo30DataSerializationService.FromJsonFromFile<List<ScoreSheetEntryProcessed>>(folderPath + "ScoreSheetEntriesProcessed.json");
      //_scoreSheetEntryPenaltiesProcessed = _lo30DataSerializationService.FromJsonFromFile<List<ScoreSheetEntryPenaltyProcessed>>(folderPath + "ScoreSheetEntryPenaltiesProcessed.json");

      _seasons = _lo30DataSerializationService.FromJsonFromFile<List<Season>>(folderPath + "Seasons.json");
      _teamRosters = _lo30DataSerializationService.FromJsonFromFile<List<TeamRoster>>(folderPath + "TeamRosters.json");
    }

    public List<Article> GetArticles()
    {
      throw new NotImplementedException();
    }

    public List<TeamStanding> GetTeamStandings()
    {
      throw new NotImplementedException();
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

    public ProcessingResult ProcessGameResultsIntoTeamStandings(int seasonId, bool playoffs, int startingGameId, int endingGameId)
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