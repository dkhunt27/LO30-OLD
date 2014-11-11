using LO30.Data;
using LO30.Data.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace LO30.Services
{
  class ContextTableList
  {
    public string QueryBegin { get; set; }
    public string QueryEnd { get; set; }
    public string TableName { get; set; }
    public string FileName { get; set; }
  }

    public class Lo30ContextService
    {
      Lo30Context _ctx;
      private Lo30DataSerializationService _lo30DataSerializationService;
      private Lo30DataService _lo30DataService;
      private string _folderPath;

      public Lo30ContextService(Lo30Context ctx)
      {
        _ctx = ctx;
        _lo30DataService = new Lo30DataService();
        _lo30DataSerializationService = new Lo30DataSerializationService();
        _folderPath = @"C:\git\LO30\LO30\App_Data\SqlServer\";
      }

      #region SaveOrUpdate functions

      #region SaveOrUpdate-ForWebGoalieStat
      public int SaveOrUpdateForWebGoalieStat(List<ForWebGoalieStat> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateForWebGoalieStat(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateForWebGoalieStat(ForWebGoalieStat toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        ForWebGoalieStat found = FindForWebGoalieStat(errorIfNotFound, errorIfMoreThanOneFound, toSave.PID, toSave.STIDPF);

        if (found == null)
        {
          _ctx.ForWebGoalieStats.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-ForWebPlayerStat
      public int SaveOrUpdateForWebPlayerStat(List<ForWebPlayerStat> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateForWebPlayerStat(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateForWebPlayerStat(ForWebPlayerStat toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        ForWebPlayerStat found = FindForWebPlayerStat(errorIfNotFound, errorIfMoreThanOneFound, toSave.PID, toSave.STIDPF);

        if (found == null)
        {
          _ctx.ForWebPlayerStats.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-ForWebTeamStanding
      public int SaveOrUpdateForWebTeamStanding(List<ForWebTeamStanding> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateForWebTeamStanding(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateForWebTeamStanding(ForWebTeamStanding toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        ForWebTeamStanding found = FindForWebTeamStanding(errorIfNotFound, errorIfMoreThanOneFound, toSave.STID);

        if (found == null)
        {
          _ctx.ForWebTeamStandings.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-Game
      public int SaveOrUpdateGame(List<Game> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateGame(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGame(Game toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        Game found = FindGame(errorIfNotFound, errorIfMoreThanOneFound, toSave.GameId);

        if (found == null)
        {
          _ctx.Games.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-GameOutcome
      public int SaveOrUpdateGameOutcome(List<GameOutcome> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateGameOutcome(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGameOutcome(GameOutcome toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        GameOutcome found = FindGameOutcome(errorIfNotFound, errorIfMoreThanOneFound, toSave.GameTeamId);

        if (found == null)
        {
          _ctx.GameOutcomes.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-GameRoster
      public int SaveOrUpdateGameRoster(List<GameRoster> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateGameRoster(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGameRoster(GameRoster toSave)
      {
        GameRoster found;
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;

        // lookup by PK, if it exists
        if (toSave.GameRosterId > 0)
        {
          found = FindGameRoster(errorIfNotFound, errorIfMoreThanOneFound, toSave.GameRosterId);
        }
        else
        {
          // lookup by PK2
          found = FindGameRosterByPK2(errorIfNotFound, errorIfMoreThanOneFound, toSave.GameTeamId, toSave.PlayerNumber);

          // if found, set missing PK
          if (found != null)
          {
            toSave.GameRosterId = found.GameRosterId;
          }
        }


        if (found == null)
        {
          _ctx.GameRosters.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-GameScore
      public int SaveOrUpdateGameScore(List<GameScore> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateGameScore(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGameScore(GameScore toSave)
      {
        GameScore found;
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;

        // lookup by PK, if it exists
        if (toSave.GameScoreId > 0)
        {
          found = FindGameScore(errorIfNotFound, errorIfMoreThanOneFound, toSave.GameScoreId);
        }
        else
        {
          // lookup by PK2
          found = FindGameScoreByPK2(errorIfNotFound, errorIfMoreThanOneFound, toSave.GameTeamId, toSave.Period);

          // if found, set missing PK
          if (found != null)
          {
            toSave.GameScoreId = found.GameScoreId;
          }
        }

        if (found == null)
        {
          _ctx.GameScores.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-GameTeam
      public int SaveOrUpdateGameTeam(List<GameTeam> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateGameTeam(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGameTeam(GameTeam toSave)
      {
        GameTeam found;
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;

        // lookup by PK, if it exists
        if (toSave.GameTeamId > 0)
        {
          found = FindGameTeam(errorIfNotFound, errorIfMoreThanOneFound, toSave.GameTeamId);
        }
        else
        {
          // lookup by PK2
          found = FindGameTeamByPK2(errorIfNotFound, errorIfMoreThanOneFound, toSave.GameId, toSave.HomeTeam);

          // if found, set missing PK
          if (found != null)
          {
            toSave.GameTeamId = found.GameTeamId;
          }
        }

        if (found == null)
        {
          _ctx.GameTeams.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-GoalieStatGame
      public int SaveOrUpdateGoalieStatGame(List<GoalieStatGame> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateGoalieStatGame(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGoalieStatGame(GoalieStatGame toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        GoalieStatGame found = FindGoalieStatGame(errorIfNotFound, errorIfMoreThanOneFound, toSave.PlayerId, toSave.GameId);

        if (found == null)
        {
          _ctx.GoalieStatsGame.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-GoalieStatSeason
      public int SaveOrUpdateGoalieStatSeason(List<GoalieStatSeason> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateGoalieStatSeason(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGoalieStatSeason(GoalieStatSeason toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        GoalieStatSeason found = FindGoalieStatSeason(errorIfNotFound, errorIfMoreThanOneFound, toSave.PlayerId, toSave.SeasonId, toSave.Sub);

        if (found == null)
        {
          _ctx.GoalieStatsSeason.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-GoalieStatSeasonTeam
      public int SaveOrUpdateGoalieStatSeasonTeam(List<GoalieStatSeasonTeam> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateGoalieStatSeasonTeam(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateGoalieStatSeasonTeam(GoalieStatSeasonTeam toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        GoalieStatSeasonTeam found = FindGoalieStatSeasonTeam(errorIfNotFound, errorIfMoreThanOneFound, toSave.PlayerId, toSave.SeasonTeamIdPlayingFor);

        if (found == null)
        {
          _ctx.GoalieStatsSeasonTeam.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-Player
      public int SaveOrUpdatePlayer(List<Player> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdatePlayer(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayer(Player toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        Player found = FindPlayer(errorIfNotFound, errorIfMoreThanOneFound, toSave.PlayerId);

        if (found == null)
        {
          found = _ctx.Players.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-PlayerDraft
      public int SaveOrUpdatePlayerDraft(List<PlayerDraft> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdatePlayerDraft(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayerDraft(PlayerDraft toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        PlayerDraft found = FindPlayerDraft(errorIfNotFound, errorIfMoreThanOneFound, toSave.SeasonId, toSave.PlayerId);

        if (found == null)
        {
          found = _ctx.PlayerDrafts.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }

      #endregion

      #region SaveOrUpdate-PlayerRating
      public int SaveOrUpdatePlayerRating(List<PlayerRating> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdatePlayerRating(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayerRating(PlayerRating toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        PlayerRating found = FindPlayerRating(errorIfNotFound, errorIfMoreThanOneFound, toSave.SeasonId, toSave.PlayerId, toSave.StartYYYYMMDD, toSave.EndYYYYMMDD);

        if (found == null)
        {
          found = _ctx.PlayerRatings.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }

      #endregion

      #region SaveOrUpdate-PlayerStatGame
      public int SaveOrUpdatePlayerStatGame(List<PlayerStatGame> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdatePlayerStatGame(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayerStatGame(PlayerStatGame toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        PlayerStatGame found = FindPlayerStatGame(errorIfNotFound, errorIfMoreThanOneFound, toSave.PlayerId, toSave.GameId);

        if (found == null)
        {
          found = _ctx.PlayerStatsGame.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-PlayerStatSeason
      public int SaveOrUpdatePlayerStatSeason(List<PlayerStatSeason> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdatePlayerStatSeason(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayerStatSeason(PlayerStatSeason toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        PlayerStatSeason found = FindPlayerStatSeason(errorIfNotFound, errorIfMoreThanOneFound, toSave.PlayerId, toSave.SeasonId, toSave.Sub);

        if (found == null)
        {
          found = _ctx.PlayerStatsSeason.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-PlayerStatSeasonTeam
      public int SaveOrUpdatePlayerStatSeasonTeam(List<PlayerStatSeasonTeam> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdatePlayerStatSeasonTeam(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdatePlayerStatSeasonTeam(PlayerStatSeasonTeam toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        PlayerStatSeasonTeam found = FindPlayerStatSeasonTeam(errorIfNotFound, errorIfMoreThanOneFound, toSave.PlayerId, toSave.SeasonTeamIdPlayingFor);

        if (found == null)
        {
          found = _ctx.PlayerStatsSeasonTeam.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-ScoreSheetEntry
      public int SaveOrUpdateScoreSheetEntry(List<ScoreSheetEntry> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateScoreSheetEntry(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateScoreSheetEntry(ScoreSheetEntry toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        ScoreSheetEntry found = FindScoreSheetEntry(errorIfNotFound, errorIfMoreThanOneFound, toSave.ScoreSheetEntryId);

        if (found == null)
        {
          _ctx.ScoreSheetEntries.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-ScoreSheetEntryPenalty
      public int SaveOrUpdateScoreSheetEntryPenalty(List<ScoreSheetEntryPenalty> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateScoreSheetEntryPenalty(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateScoreSheetEntryPenalty(ScoreSheetEntryPenalty toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        ScoreSheetEntryPenalty found = FindScoreSheetEntryPenalty(errorIfNotFound, errorIfMoreThanOneFound, toSave.ScoreSheetEntryPenaltyId);

        if (found == null)
        {
          _ctx.ScoreSheetEntryPenalties.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-ScoreSheetEntryProcessed
      public int SaveOrUpdateScoreSheetEntryProcessed(List<ScoreSheetEntryProcessed> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateScoreSheetEntryProcessed(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateScoreSheetEntryProcessed(ScoreSheetEntryProcessed toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        ScoreSheetEntryProcessed found = FindScoreSheetEntryProcessed(errorIfNotFound, errorIfMoreThanOneFound, toSave.ScoreSheetEntryId);

        if (found == null)
        {
          found = _ctx.ScoreSheetEntriesProcessed.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-ScoreSheetEntryPenaltyProcessed
      public int SaveOrUpdateScoreSheetEntryPenaltyProcessed(List<ScoreSheetEntryPenaltyProcessed> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateScoreSheetEntryPenaltyProcessed(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateScoreSheetEntryPenaltyProcessed(ScoreSheetEntryPenaltyProcessed toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        ScoreSheetEntryPenaltyProcessed found = FindScoreSheetEntryPenaltyProcessed(errorIfNotFound, errorIfMoreThanOneFound, toSave.ScoreSheetEntryPenaltyId);

        if (found == null)
        {
          _ctx.ScoreSheetEntryPenaltiesProcessed.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-Season
      public int SaveOrUpdateSeason(List<Season> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateSeason(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateSeason(Season toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        Season found = FindSeason(errorIfNotFound, errorIfMoreThanOneFound, toSave.SeasonId);

        if (found == null)
        {
          _ctx.Seasons.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-SeasonTeam
      public int SaveOrUpdateSeasonTeam(List<SeasonTeam> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateSeasonTeam(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateSeasonTeam(SeasonTeam toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        SeasonTeam found = FindSeasonTeam(errorIfNotFound, errorIfMoreThanOneFound, toSave.SeasonTeamId);

        if (found == null)
        {
          _ctx.SeasonTeams.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-Setting
      public int SaveOrUpdateSetting(List<Setting> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateSetting(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateSetting(Setting toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        Setting found = FindSetting(errorIfNotFound, errorIfMoreThanOneFound, toSave.SettingId);

        if (found == null)
        {
          _ctx.Settings.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-TeamRoster
      public int SaveOrUpdateTeamRoster(List<TeamRoster> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateTeamRoster(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateTeamRoster(TeamRoster toSave)
      {
        var errorIfNotFound = false;
        var errorIfMoreThanOneFound = true;
        TeamRoster found = FindTeamRoster(errorIfNotFound, errorIfMoreThanOneFound, toSave.SeasonTeamId, toSave.PlayerId, toSave.StartYYYYMMDD, toSave.EndYYYYMMDD);

        if (found == null)
        {
          _ctx.TeamRosters.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #region SaveOrUpdate-TeamStanding
      public int SaveOrUpdateTeamStanding(List<TeamStanding> listToSave)
      {
        var saved = 0;
        foreach (var toSave in listToSave)
        {
          var results = SaveOrUpdateTeamStanding(toSave);
          saved = saved + results;
        }

        return saved;
      }

      public int SaveOrUpdateTeamStanding(TeamStanding toSave)
      {
        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        TeamStanding found = FindTeamStanding(errorIfNotFound, errorIfMoreThanOneFound, toSave.SeasonTeamId, toSave.Playoff);

        if (found == null)
        {
          _ctx.TeamStandings.Add(toSave);
        }
        else
        {
          var entry = _ctx.Entry(found);
          entry.OriginalValues.SetValues(found);
          entry.CurrentValues.SetValues(toSave);
        }

        return ContextSaveChanges();
      }
      #endregion

      #endregion

      #region find functions

      #region Find-ForWebGoalieStat
      public ForWebGoalieStat FindForWebGoalieStat(int pid, int stidpf)
      {
        return FindForWebGoalieStat(errorIfNotFound: true, errorIfMoreThanOneFound: true, pid: pid, stidpf: stidpf);
      }

      public ForWebGoalieStat FindForWebGoalieStat(bool errorIfNotFound, bool errorIfMoreThanOneFound, int pid, int stidpf)
      {
        var found = _ctx.ForWebGoalieStats.Where(x => x.PID == pid && x.STIDPF == stidpf).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find ForWebGoalieStat for" +
                                                  " PID:" + pid +
                                                  " STIDPF:" + stidpf
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 ForWebGoalieStat was not found for" +
                                                  " PID:" + pid +
                                                  " STIDPF:" + stidpf
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-ForWebPlayerStat
      public ForWebPlayerStat FindForWebPlayerStat(int pid, int stidpf)
      {
        return FindForWebPlayerStat(errorIfNotFound: true, errorIfMoreThanOneFound: true, pid: pid, stidpf: stidpf);
      }

      public ForWebPlayerStat FindForWebPlayerStat(bool errorIfNotFound, bool errorIfMoreThanOneFound, int pid, int stidpf)
      {
        var found = _ctx.ForWebPlayerStats.Where(x => x.PID == pid && x.STIDPF == stidpf).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find ForWebPlayerStat for" +
                                                  " PID:" + pid +
                                                  " STIDPF:" + stidpf
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 ForWebPlayerStat was not found for" +
                                                  " PID:" + pid +
                                                  " STIDPF:" + stidpf
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-ForWebTeamStanding
      public ForWebTeamStanding FindForWebTeamStanding(int stid)
      {
        return FindForWebTeamStanding(errorIfNotFound: true, errorIfMoreThanOneFound: true, stid: stid);
      }

      public ForWebTeamStanding FindForWebTeamStanding(bool errorIfNotFound, bool errorIfMoreThanOneFound, int stid)
      {
        var found = _ctx.ForWebTeamStandings.Where(x => x.STID == stid).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find ForWebTeamStanding for" +
                                                  " STID:" + stid
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 ForWebTeamStanding was not found for" +
                                                  " STID:" + stid
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-Game
      public Game FindGame(int gameId)
      {
        return FindGame(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameId: gameId);
      }

      public Game FindGame(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameId)
      {
        var found = _ctx.Games
                          .Include("Season")
                          .Where(x => x.GameId == gameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find Game for" +
                                                  " GameId:" + gameId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 Game was not found for" +
                                                  " GameId:" + gameId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-GameOutcome (addtl finds)
      public List<GameOutcome> FindGameOutcomesWithGameIdsAndTeamId(int startingGameId, int endingGameId, int seasonTeamId)
      {
        return FindGameOutcomesWithGameIdsAndTeamId(errorIfNotFound: true, startingGameId: startingGameId, endingGameId: endingGameId, seasonTeamId: seasonTeamId);
      }

      public List<GameOutcome> FindGameOutcomesWithGameIdsAndTeamId(bool errorIfNotFound, int startingGameId, int endingGameId, int seasonTeamId)
      {
        var found = _ctx.GameOutcomes.Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId && x.GameTeam.SeasonTeamId == seasonTeamId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameOutcome for" +
                                                  " GameTeam.GameId (starting):" + startingGameId +
                                                  " GameTeam.GameId (ending):" + endingGameId +
                                                  " GameTeam.SeasonTeamId:" + seasonTeamId
                                          );
        }

        return found;
      }

      public List<GameOutcome> FindGameOutcomesWithGameIds(int startingGameId, int endingGameId)
      {
        return FindGameOutcomesWithGameIds(errorIfNotFound: true, startingGameId: startingGameId, endingGameId: endingGameId);
      }

      public List<GameOutcome> FindGameOutcomesWithGameIds(bool errorIfNotFound, int startingGameId, int endingGameId)
      {
        var found = _ctx.GameOutcomes.Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameOutcome for" +
                                                  " GameTeam.GameId (starting):" + startingGameId +
                                                  " GameTeam.GameId (ending):" + endingGameId
                                          );
        }

        return found;
      }

      public GameOutcome FindGameOutcome(int gameTeamId)
      {
        return FindGameOutcome(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameTeamId: gameTeamId);
      }

      public GameOutcome FindGameOutcome(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameTeamId)
      {
        var found = _ctx.GameOutcomes.Where(x => x.GameTeamId == gameTeamId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameOutcome for" +
                                                  " GameTeamId:" + gameTeamId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameOutcome was not found for" +
                                                  " GameTeamId:" + gameTeamId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-GameRosters (addtl finds)
      public List<GameRoster> FindGameRostersWithGameIdsAndGoalie(int startingGameId, int endingGameId, bool goalie)
      {
        return FindGameRostersWithGameIdsAndGoalie(errorIfNotFound: true, startingGameId: startingGameId, endingGameId: endingGameId, goalie: goalie);
      }

      public List<GameRoster> FindGameRostersWithGameIdsAndGoalie(bool errorIfNotFound, int startingGameId, int endingGameId, bool goalie)
      {
        var found = _ctx.GameRosters
                          .Include("GameTeam")
                          .Include("GameTeam.Game")
                          .Include("GameTeam.Game.Season")              
                          .Include("GameTeam.SeasonTeam")
                          .Include("GameTeam.SeasonTeam.Season")
                          .Include("GameTeam.SeasonTeam.Team")
                          .Include("GameTeam.SeasonTeam.Team.Coach")
                          .Include("GameTeam.SeasonTeam.Team.Sponsor")

                          .Include("Player")
                          .Include("SubbingForPlayer")
                          .Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId && x.Goalie == goalie).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameRosters for" +
                                                  " GameTeam.GameId (starting):" + startingGameId +
                                                  " GameTeam.GameId (ending):" + endingGameId +
                                                  " Goalie:" + goalie
                                          );
        }

        return found;
      }

      public List<GameRoster> FindGameRostersWithGameIds(int startingGameId, int endingGameId)
      {
        return FindGameRostersWithGameIds(errorIfNotFound: true, startingGameId: startingGameId, endingGameId: endingGameId);
      }

      public List<GameRoster> FindGameRostersWithGameIds(bool errorIfNotFound, int startingGameId, int endingGameId)
      {
        var found = _ctx.GameRosters
                          .Include("GameTeam")
                          .Include("GameTeam.Game")
                          .Include("GameTeam.Game.Season")
                          .Include("GameTeam.SeasonTeam")
                          .Include("GameTeam.SeasonTeam.Season")
                          .Include("GameTeam.SeasonTeam.Team")
                          .Include("GameTeam.SeasonTeam.Team.Coach")
                          .Include("GameTeam.SeasonTeam.Team.Sponsor")

                          .Include("Player")
                          .Include("SubbingForPlayer")
                          .Where(x => x.GameTeam.GameId >= startingGameId && x.GameTeam.GameId <= endingGameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameRosters for" +
                                                  " GameTeam.GameId (starting):" + startingGameId +
                                                  " GameTeam.GameId (ending):" + endingGameId
                                          );
        }

        return found;
      }

      public GameRoster FindGameRosterByPK2(int gameTeamId, string playerNumber)
      {
        return FindGameRosterByPK2(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameTeamId: gameTeamId, playerNumber: playerNumber);
      }

      public GameRoster FindGameRosterByPK2(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameTeamId, string playerNumber)
      {
        var found = _ctx.GameRosters
                          .Include("GameTeam")
                          .Include("GameTeam.Game")
                          .Include("GameTeam.Game.Season")
                          .Include("GameTeam.SeasonTeam")
                          .Include("GameTeam.SeasonTeam.Season")
                          .Include("GameTeam.SeasonTeam.Team")
                          .Include("GameTeam.SeasonTeam.Team.Coach")
                          .Include("GameTeam.SeasonTeam.Team.Sponsor")

                          .Include("Player")
                          .Include("SubbingForPlayer")
                          .Where(x => x.GameTeamId == gameTeamId && x.PlayerNumber == playerNumber).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameRoster for" +
                                                  " GameTeamId:" + gameTeamId +
                                                  " PlayerNumber:" + playerNumber
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameRoster was not found for" +
                                                  " GameTeamId:" + gameTeamId +
                                                  " PlayerNumber:" + playerNumber
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }

      public GameRoster FindGameRoster(int gameRosterId)
      {
        return FindGameRoster(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameRosterId: gameRosterId);
      }

      public GameRoster FindGameRoster(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameRosterId)
      {
        var found = _ctx.GameRosters
                          .Include("GameTeam")
                          .Include("GameTeam.Game")
                          .Include("GameTeam.Game.Season")
                          .Include("GameTeam.SeasonTeam")
                          .Include("GameTeam.SeasonTeam.Season")
                          .Include("GameTeam.SeasonTeam.Team")
                          .Include("GameTeam.SeasonTeam.Team.Coach")
                          .Include("GameTeam.SeasonTeam.Team.Sponsor")

                          .Include("Player")
                          .Include("SubbingForPlayer")
                          .Where(x => x.GameRosterId == gameRosterId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameRoster for" +
                                                  " GameRosterId:" + gameRosterId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameRoster was not found for" +
                                                  " GameRosterId:" + gameRosterId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-GameScore
      public GameScore FindGameScoreByPK2(int gameTeamId, int period)
      {
        return FindGameScoreByPK2(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameTeamId: gameTeamId, period: period);
      }

      public GameScore FindGameScoreByPK2(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameTeamId, int period)
      {
        var found = _ctx.GameScores.Where(x => x.GameTeamId == gameTeamId && x.Period == period).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameScore for" +
                                                  " GameTeamId:" + gameTeamId +
                                                  " Period:" + period 
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameScore was not found for" +
                                                  " GameTeamId:" + gameTeamId +
                                                  " Period:" + period 
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      public GameScore FindGameScore(int gameScoreId)
      {
        return FindGameScore(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameScoreId: gameScoreId);
      }

      public GameScore FindGameScore(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameScoreId)
      {
        var found = _ctx.GameScores.Where(x => x.GameScoreId == gameScoreId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameScore for" +
                                                  " GameScoreId:" + gameScoreId 
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameScore was not found for" +
                                                  " GameScoreId:" + gameScoreId 
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-GameTeam
      public GameTeam FindGameTeamByPK2(int gameId, bool homeTeam)
      {
        return FindGameTeamByPK2(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameId: gameId, homeTeam: homeTeam);
      }

      public GameTeam FindGameTeamByPK2(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameId, bool homeTeam)
      {
        var found = _ctx.GameTeams
                          .Include("Game")
                          .Include("Game.Season")

                          .Include("SeasonTeam")
                          .Include("SeasonTeam.Season")
                          .Include("SeasonTeam.Team")
                          .Include("SeasonTeam.Team.Coach")
                          .Include("SeasonTeam.Team.Sponsor")
                          .Where(x => x.GameId == gameId && x.HomeTeam == homeTeam).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameTeam for" +
                                                  " GameId:" + gameId +
                                                  " HomeTeam:" + homeTeam
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameTeam was not found for" +
                                                  " GameId:" + gameId +
                                                  " HomeTeam:" + homeTeam
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }

      public GameTeam FindGameTeam(int gameTeamId)
      {
        return FindGameTeam(errorIfNotFound: true, errorIfMoreThanOneFound: true, gameTeamId: gameTeamId);
      }

      public GameTeam FindGameTeam(bool errorIfNotFound, bool errorIfMoreThanOneFound, int gameTeamId)
      {
        var found = _ctx.GameTeams
                          .Include("Game")
                          .Include("Game.Season")

                          .Include("SeasonTeam")
                          .Include("SeasonTeam.Season")
                          .Include("SeasonTeam.Team")
                          .Include("SeasonTeam.Team.Coach")
                          .Include("SeasonTeam.Team.Sponsor")
                          .Where(x => x.GameTeamId == gameTeamId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GameTeam for" +
                                                  " GameTeamId:" + gameTeamId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GameTeam was not found for" +
                                                  " GameTeamId:" + gameTeamId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-GoalieStatGame
      public GoalieStatGame FindGoalieStatGame(int playerId, int gameId)
      {
        return FindGoalieStatGame(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, gameId: gameId);
      }

      public GoalieStatGame FindGoalieStatGame(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int gameId)
      {
        var found = _ctx.GoalieStatsGame.Where(x => x.PlayerId == playerId && x.GameId == gameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GoalieStatsGame for" +
                                                  " PlayerId:" + playerId +
                                                  " GameId:" + gameId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GoalieStatsGame was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " GameId:" + gameId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-GoalieStatSeason
      public GoalieStatSeason FindGoalieStatSeason(int playerId, int seasonId, bool sub)
      {
        return FindGoalieStatSeason(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, seasonId: seasonId, sub: sub);
      }

      public GoalieStatSeason FindGoalieStatSeason(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int seasonId, bool sub)
      {
        var found = _ctx.GoalieStatsSeason.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId && x.Sub == sub).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GoalieStatSeason for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonId:" + seasonId +
                                                  " Sub:" + sub
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GoalieStatSeason was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonId:" + seasonId +
                                                  " Sub:" + sub
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-GoalieStatSeasonTeam
      public GoalieStatSeasonTeam FindGoalieStatSeasonTeam(int playerId, int seasonTeamIdPlayingFor)
      {
        return FindGoalieStatSeasonTeam(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, seasonTeamIdPlayingFor: seasonTeamIdPlayingFor);
      }

      public GoalieStatSeasonTeam FindGoalieStatSeasonTeam(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int seasonTeamIdPlayingFor)
      {
        var found = _ctx.GoalieStatsSeasonTeam.Where(x => x.PlayerId == playerId && x.SeasonTeamIdPlayingFor == seasonTeamIdPlayingFor).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find GoalieStatSeasonTeam for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonTeamIdPlayingFor:" + seasonTeamIdPlayingFor
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 GoalieStatSeasonTeam was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonTeamIdPlayingFor:" + seasonTeamIdPlayingFor
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-Player
      public Player FindPlayer(int playerId)
      {
        return FindPlayer(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId);
      }

      public Player FindPlayer(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId)
      {
        var found = _ctx.Players.Where(x => x.PlayerId == playerId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find Player for" +
                                                  " PlayerId:" + playerId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 Player was not found for" +
                                                  " PlayerId:" + playerId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion
      
      #region Find-PlayerDraft
      public PlayerDraft FindPlayerDraft(int seasonId, int playerId)
      {
        return FindPlayerDraft(errorIfNotFound: true, errorIfMoreThanOneFound: true, seasonId: seasonId, playerId: playerId);
      }

      public PlayerDraft FindPlayerDraft(bool errorIfNotFound, bool errorIfMoreThanOneFound, int seasonId, int playerId)
      {
        var found = _ctx.PlayerDrafts.Where(x => x.SeasonId == seasonId && x.PlayerId == playerId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerDraft for" +
                                                  " seasonId:" + seasonId +
                                                  " PlayerId:" + playerId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerDraft was not found for" +
                                                  " SeasonId:" + seasonId +
                                                  " PlayerId:" + playerId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-PlayerRating (addtl finds)
      public PlayerRating FindPlayerRatingWithYYYYMMDD(int playerId, string position, int seasonId, int yyyymmdd)
      {
        bool errorIfNotFound = true;
        bool errorIfMoreThanOneFound = true;
        return FindPlayerRatingWithYYYYMMDD(errorIfNotFound, errorIfMoreThanOneFound, playerId, position, seasonId, yyyymmdd);
      }

      public PlayerRating FindPlayerRatingWithYYYYMMDD(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, string position, int seasonId, int yyyymmdd)
      {
        var found = _ctx.PlayerRatings.Where(x => x.SeasonId == seasonId &&
                                                  x.PlayerId == playerId &&
                                                  (x.Position == position || x.Position == "X") &&
                                                  x.StartYYYYMMDD <= yyyymmdd &&
                                                  x.EndYYYYMMDD >= yyyymmdd)
                                            .ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerRating for" +
                                                  " PlayerId:" + playerId +
                                                  " Position:" + position +
                                                  " seasonId:" + seasonId +
                                                  " StartYYYYMMDD and EndYYYYMMDD between:" + yyyymmdd
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerRating was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " Position:" + position +
                                                  " seasonId:" + seasonId +
                                                  " StartYYYYMMDD and EndYYYYMMDD between:" + yyyymmdd
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }

      public PlayerRating FindPlayerRating(int seasonId, int playerId, int startYYYYMMDD, int endYYYYMMDD)
      {
        return FindPlayerRating(errorIfNotFound: true, errorIfMoreThanOneFound: true, seasonId: seasonId, playerId: playerId, startYYYYMMDD: startYYYYMMDD, endYYYYMMDD: endYYYYMMDD);
      }

      public PlayerRating FindPlayerRating(bool errorIfNotFound, bool errorIfMoreThanOneFound, int seasonId, int playerId, int startYYYYMMDD, int endYYYYMMDD)
      {
        var found = _ctx.PlayerRatings.Where(x => x.SeasonId == seasonId &&
                                                  x.PlayerId == playerId &&
                                                  x.StartYYYYMMDD == startYYYYMMDD &&
                                                  x.EndYYYYMMDD == endYYYYMMDD)
                                            .ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerRating for" +
                                                  " seasonId:" + seasonId +
                                                  " PlayerId:" + playerId +
                                                  " StartYYYYMMDD:" + startYYYYMMDD +
                                                  " EndYYYYMMDD:" + endYYYYMMDD
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerRating was not found for" +
                                                  " seasonId:" + seasonId +
                                                  " PlayerId:" + playerId +
                                                  " StartYYYYMMDD:" + startYYYYMMDD +
                                                  " EndYYYYMMDD:" + endYYYYMMDD
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-PlayerStatGame
      public PlayerStatGame FindPlayerStatGame(int playerId, int gameId)
      {
        return FindPlayerStatGame(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, gameId: gameId);
      }

      public PlayerStatGame FindPlayerStatGame(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int gameId)
      {
        var found = _ctx.PlayerStatsGame.Where(x => x.PlayerId == playerId && x.GameId == gameId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerStatGame for" +
                                                  " PlayerId:" + playerId +
                                                  " GameId:" + gameId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerStatGame was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " GameId:" + gameId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        } 
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-PlayerStatSeason
      public PlayerStatSeason FindPlayerStatSeason(int playerId, int seasonId, bool sub)
      {
        return FindPlayerStatSeason(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, seasonId: seasonId, sub: sub);
      }

      public PlayerStatSeason FindPlayerStatSeason(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int seasonId, bool sub)
      {
        var found = _ctx.PlayerStatsSeason.Where(x => x.PlayerId == playerId && x.SeasonId == seasonId && x.Sub == sub).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerStatsSeason for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonId:" + seasonId +
                                                  " Sub:" + sub
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerStatsSeason was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonId:" + seasonId +
                                                  " Sub:" + sub
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-PlayerStatSeasonTeam
      public PlayerStatSeasonTeam FindPlayerStatSeasonTeam(int playerId, int seasonTeamIdPlayingFor)
      {
        return FindPlayerStatSeasonTeam(errorIfNotFound: true, errorIfMoreThanOneFound: true, playerId: playerId, seasonTeamIdPlayingFor: seasonTeamIdPlayingFor);
      }

      public PlayerStatSeasonTeam FindPlayerStatSeasonTeam(bool errorIfNotFound, bool errorIfMoreThanOneFound, int playerId, int seasonTeamIdPlayingFor)
      {
        var found = _ctx.PlayerStatsSeasonTeam.Where(x => x.PlayerId == playerId && x.SeasonTeamIdPlayingFor == seasonTeamIdPlayingFor).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find PlayerStatsSeasonTeam for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonTeamIdPlayingFor:" + seasonTeamIdPlayingFor
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 PlayerStatsSeasonTeam was not found for" +
                                                  " PlayerId:" + playerId +
                                                  " SeasonTeamIdPlayingFor:" + seasonTeamIdPlayingFor
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-ScoreSheetEntry
      public ScoreSheetEntry FindScoreSheetEntry(int scoreSheetEntryId)
      {
        return FindScoreSheetEntry(errorIfNotFound: true, errorIfMoreThanOneFound: true, scoreSheetEntryId: scoreSheetEntryId);
      }

      public ScoreSheetEntry FindScoreSheetEntry(bool errorIfNotFound, bool errorIfMoreThanOneFound, int scoreSheetEntryId)
      {
        var found = _ctx.ScoreSheetEntries.Where(x => x.ScoreSheetEntryId == scoreSheetEntryId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find ScoreSheetEntry for" +
                                                  " scoreSheetEntryId:" + scoreSheetEntryId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 ScoreSheetEntry was not found for" +
                                                  " scoreSheetEntryId:" + scoreSheetEntryId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-ScoreSheetEntryPenalty
      public ScoreSheetEntryPenalty FindScoreSheetEntryPenalty(int scoreSheetEntryPenaltyId)
      {
        return FindScoreSheetEntryPenalty(errorIfNotFound: true, errorIfMoreThanOneFound: true, scoreSheetEntryPenaltyId: scoreSheetEntryPenaltyId);
      }

      public ScoreSheetEntryPenalty FindScoreSheetEntryPenalty(bool errorIfNotFound, bool errorIfMoreThanOneFound, int scoreSheetEntryPenaltyId)
      {
        var found = _ctx.ScoreSheetEntryPenalties.Where(x => x.ScoreSheetEntryPenaltyId == scoreSheetEntryPenaltyId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find ScoreSheetEntryPenalty for" +
                                                  " scoreSheetEntryPenaltyId:" + scoreSheetEntryPenaltyId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 ScoreSheetEntryPenalty was not found for" +
                                                  " scoreSheetEntryPenaltyId:" + scoreSheetEntryPenaltyId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-ScoreSheetEntryProcessed
      public ScoreSheetEntryProcessed FindScoreSheetEntryProcessed(int scoreSheetEntryId)
      {
        return FindScoreSheetEntryProcessed(errorIfNotFound: true, errorIfMoreThanOneFound: true, scoreSheetEntryId: scoreSheetEntryId);
      }

      public ScoreSheetEntryProcessed FindScoreSheetEntryProcessed(bool errorIfNotFound, bool errorIfMoreThanOneFound, int scoreSheetEntryId)
      {
        var found = _ctx.ScoreSheetEntriesProcessed.Where(x => x.ScoreSheetEntryId == scoreSheetEntryId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find ScoreSheetEntryProcessed for" +
                                                  " scoreSheetEntryId:" + scoreSheetEntryId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 ScoreSheetEntryProcessed was not found for" +
                                                  " scoreSheetEntryId:" + scoreSheetEntryId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-ScoreSheetEntryPenaltyProcessed
      public ScoreSheetEntryPenaltyProcessed FindScoreSheetEntryPenaltyProcessed(int scoreSheetEntryPenaltyId)
      {
        return FindScoreSheetEntryPenaltyProcessed(errorIfNotFound: true, errorIfMoreThanOneFound: true, scoreSheetEntryPenaltyId: scoreSheetEntryPenaltyId);
      }

      public ScoreSheetEntryPenaltyProcessed FindScoreSheetEntryPenaltyProcessed(bool errorIfNotFound, bool errorIfMoreThanOneFound, int scoreSheetEntryPenaltyId)
      {
        var found = _ctx.ScoreSheetEntryPenaltiesProcessed.Where(x => x.ScoreSheetEntryPenaltyId == scoreSheetEntryPenaltyId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find ScoreSheetEntryPenaltyProcessed for" +
                                                  " scoreSheetEntryPenaltyId:" + scoreSheetEntryPenaltyId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 ScoreSheetEntryPenaltyProcessed was not found for" +
                                                  " scoreSheetEntryPenaltyId:" + scoreSheetEntryPenaltyId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-Season (addtl finds)
      public Season FindSeasonWithYYYYMMDD(int yyyymmdd)
      {
        return FindSeasonWithYYYYMMDD(errorIfNotFound: true, errorIfMoreThanOneFound: true, yyyymmdd: yyyymmdd);
      }

      public Season FindSeasonWithYYYYMMDD(bool errorIfNotFound, bool errorIfMoreThanOneFound, int yyyymmdd)
      {
        var found = _ctx.Seasons.Where(x => x.StartYYYYMMDD >= yyyymmdd && x.EndYYYYMMDD <= yyyymmdd).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find Season for" +
                                                  " StartYYYYMMDD and EndYYYYMMDD between:" + yyyymmdd
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 Season was not found for" +
                                                  " StartYYYYMMDD and EndYYYYMMDD between:" + yyyymmdd
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }

      public Season FindSeasonWithIsCurrentSeason(bool isCurrentSeason)
      {
        return FindSeasonCurrent(errorIfNotFound: true, errorIfMoreThanOneFound: true, isCurrentSeason: isCurrentSeason);
      }

      public Season FindSeasonCurrent(bool errorIfNotFound, bool errorIfMoreThanOneFound, bool isCurrentSeason)
      {
        var found = _ctx.Seasons.Where(x => x.IsCurrentSeason == isCurrentSeason).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find Season for" +
                                                  " isCurrentSeason:" + isCurrentSeason
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 Season was not found for" +
                                                  " isCurrentSeason:" + isCurrentSeason
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }

      public Season FindSeason(int seasonId)
      {
        return FindSeason(errorIfNotFound: true, errorIfMoreThanOneFound: true, seasonId: seasonId);
      }

      public Season FindSeason(bool errorIfNotFound, bool errorIfMoreThanOneFound, int seasonId)
      {
        var found = _ctx.Seasons.Where(x => x.SeasonId == seasonId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find Season for" +
                                                  " SeasonId:" + seasonId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 Season was not found for" +
                                                  " SeasonId:" + seasonId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-SeasonTeam
      public SeasonTeam FindSeasonTeam(int seasonTeamId)
      {
        return FindSeasonTeam(errorIfNotFound: true, errorIfMoreThanOneFound: true, seasonTeamId: seasonTeamId);
      }

      public SeasonTeam FindSeasonTeam(bool errorIfNotFound, bool errorIfMoreThanOneFound, int seasonTeamId)
      {
        var found = _ctx.SeasonTeams.Where(x => x.SeasonTeamId == seasonTeamId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find SeasonTeam for" +
                                                  " SeasonTeamId:" + seasonTeamId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 SeasonTeam was not found for" +
                                                  " SeasonTeamId:" + seasonTeamId 
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-Setting
      public Setting FindSetting(int settingId)
      {
        bool errorIfNotFound = true;
        bool errorIfMoreThanOneFound = true;
        return FindSetting(errorIfNotFound, errorIfMoreThanOneFound, settingId);
      }

      public Setting FindSetting(bool errorIfNotFound, bool errorIfMoreThanOneFound, int settingId)
      {
        var found = _ctx.Settings.Where(x => x.SettingId == settingId).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find Setting for" +
                                                  " SettingId:" + settingId
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 Setting was not found for" +
                                                  " SettingId:" + settingId
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-TeamRoster
      public TeamRoster FindTeamRosterWithYYYYMMDD(int seasonTeamId, int playerId, int yyyymmdd)
      {
        return FindTeamRosterWithYYYYMMDD(errorIfNotFound: true, errorIfMoreThanOneFound: true, seasonTeamId: seasonTeamId, playerId: playerId, yyyymmdd: yyyymmdd);
      }

      public TeamRoster FindTeamRosterWithYYYYMMDD(bool errorIfNotFound, bool errorIfMoreThanOneFound, int seasonTeamId, int playerId, int yyyymmdd)
      {
        var found = _ctx.TeamRosters.Where(x => x.SeasonTeamId == seasonTeamId &&
                                                  x.PlayerId == playerId &&
                                                  x.StartYYYYMMDD <= yyyymmdd &&
                                                  x.EndYYYYMMDD >= yyyymmdd)
                                            .ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find TeamRoster for" +
                                                  " SeasonTeamId:" + seasonTeamId +
                                                  " PlayerId:" + playerId +
                                                  " StartYYYYMMDD and EndYYYYMMDD between:" + yyyymmdd
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 TeamRoster was not found for" +
                                                  " SeasonTeamId:" + seasonTeamId +
                                                  " PlayerId:" + playerId +
                                                  " StartYYYYMMDD and EndYYYYMMDD between:" + yyyymmdd
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }

      public TeamRoster FindTeamRoster(int seasonTeamId, int playerId, int startYYYYMMDD, int endYYYYMMDD)
      {
        return FindTeamRoster(errorIfNotFound: true, errorIfMoreThanOneFound: true, seasonTeamId: seasonTeamId, playerId: playerId, startYYYYMMDD: startYYYYMMDD, endYYYYMMDD: endYYYYMMDD);
      }

      public TeamRoster FindTeamRoster(bool errorIfNotFound, bool errorIfMoreThanOneFound, int seasonTeamId, int playerId, int startYYYYMMDD, int endYYYYMMDD)
      {
        var found = _ctx.TeamRosters.Where(x => x.SeasonTeamId == seasonTeamId &&
                                                  x.PlayerId == playerId &&
                                                  x.StartYYYYMMDD == startYYYYMMDD &&
                                                  x.EndYYYYMMDD == endYYYYMMDD)
                                            .ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find TeamRoster for" +
                                                  " SeasonTeamId:" + seasonTeamId +
                                                  " PlayerId:" + playerId +
                                                  " StartYYYYMMDD:" + startYYYYMMDD +
                                                  " EndYYYYMMDD:" + endYYYYMMDD
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 TeamRoster was not found for" +
                                                  " SeasonTeamId:" + seasonTeamId +
                                                  " PlayerId:" + playerId +
                                                  " StartYYYYMMDD:" + startYYYYMMDD +
                                                  " EndYYYYMMDD:" + endYYYYMMDD
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #region Find-TeamStanding
      public TeamStanding FindTeamStanding(int seasonTeamId, bool playoff)
      {
        return FindTeamStanding(errorIfNotFound: true, errorIfMoreThanOneFound: true, seasonTeamId: seasonTeamId, playoff: playoff);
      }

      public TeamStanding FindTeamStanding(bool errorIfNotFound, bool errorIfMoreThanOneFound, int seasonTeamId, bool playoff)
      {
        var found = _ctx.TeamStandings.Where(x => x.SeasonTeamId == seasonTeamId && x.Playoff == playoff).ToList();

        if (errorIfNotFound == true && found.Count < 1)
        {
          throw new ArgumentNullException("found", "Could not find TeamStanding for" +
                                                  " SeasonTeamId:" + seasonTeamId +
                                                  " Playoff:" + playoff
                                          );
        }

        if (errorIfMoreThanOneFound == true && found.Count > 1)
        {
          throw new ArgumentNullException("found", "More than 1 TeamStanding was not found for" +
                                                  " SeasonTeamId:" + seasonTeamId +
                                                  " Playoff:" + playoff
                                          );
        }

        if (found.Count == 1)
        {
          return found[0];
        }
        else
        {
          return null;
        }
      }
      #endregion

      #endregion

      public int ContextSaveChanges()
      {
        try
        {
          return _ctx.SaveChanges();
        }
        catch (DbEntityValidationException e)
        {
          foreach (var eve in e.EntityValidationErrors)
          {
            Debug.Print("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
            foreach (var ve in eve.ValidationErrors)
            {
              Debug.Print("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
            }
          }
          throw;
        }
        catch (Exception ex)
        {
          Debug.Print(ex.Message);
          var innerEx = ex.InnerException;

          while (innerEx != null)
          {
            Debug.Print("With inner exception of:");
            Debug.Print(innerEx.Message);

            innerEx = innerEx.InnerException;
          }

          Debug.Print(ex.StackTrace);

          throw ex;
        }
      }


      public void SaveTablesToJson()
      {
        DateTime first = DateTime.Now;
        DateTime last = DateTime.Now;
        TimeSpan diffFromFirst = new TimeSpan();

        //Address
        //Article
        //Email
        //EmailType

        _lo30DataSerializationService.ToJsonToFile(_ctx.ForWebGoalieStats
                                           .Where(x => x.SID == 54).ToList(),
                                     _folderPath + "ForWebGoalieStats.json");
        _lo30DataSerializationService.ToJsonToFile(_ctx.ForWebPlayerStats
                                            .Where(x => x.SID == 54).ToList(),
                                      _folderPath + "ForWebPlayerStats.json");

        _lo30DataSerializationService.ToJsonToFile(_ctx.Games
                                            .Include("Season")
                                            .Where(x => x.SeasonId == 54).ToList(), 
                                      _folderPath + "Games.json");
        _lo30DataSerializationService.ToJsonToFile(_ctx.GameOutcomes
                                            .Include("GameTeam")
                                            .Include("GameTeam.Game")
                                            .Include("GameTeam.Game.Season")
                                            .Include("GameTeam.SeasonTeam")
                                            .Include("GameTeam.SeasonTeam.Season")
                                            .Include("GameTeam.SeasonTeam.Team")
                                            .Include("GameTeam.SeasonTeam.Team.Coach")
                                            .Include("GameTeam.SeasonTeam.Team.Sponsor")
                                            .Where(x => x.GameTeam.SeasonTeam.SeasonId == 54).ToList(), 
                                      _folderPath + "GameOutcomes.json");
        _lo30DataSerializationService.ToJsonToFile(_ctx.GameRosters
                                            .Include("GameTeam")
                                            .Include("GameTeam.Game")
                                            .Include("GameTeam.Game.Season")
                                            .Include("GameTeam.SeasonTeam")
                                            .Include("GameTeam.SeasonTeam.Season")
                                            .Include("GameTeam.SeasonTeam.Team")
                                            .Include("GameTeam.SeasonTeam.Team.Coach")
                                            .Include("GameTeam.SeasonTeam.Team.Sponsor")
                                            .Where(x => x.GameTeam.SeasonTeam.SeasonId == 54).ToList(),
                                      _folderPath + "GameRosters.json");
        _lo30DataSerializationService.ToJsonToFile(_ctx.GameScores
                                            .Include("GameTeam")
                                            .Include("GameTeam.Game")
                                            .Include("GameTeam.Game.Season")
                                            .Include("GameTeam.SeasonTeam")
                                            .Include("GameTeam.SeasonTeam.Season")
                                            .Include("GameTeam.SeasonTeam.Team")
                                            .Include("GameTeam.SeasonTeam.Team.Coach")
                                            .Include("GameTeam.SeasonTeam.Team.Sponsor")
                                            .Where(x => x.GameTeam.SeasonTeam.SeasonId == 54).ToList(),
                                      _folderPath + "GameScores.json");
        _lo30DataSerializationService.ToJsonToFile(_ctx.GameTeams
                                            .Include("Game")
                                            .Include("Game.Season")
                                            .Include("SeasonTeam")
                                            .Include("SeasonTeam.Season")
                                            .Include("SeasonTeam.Team")
                                            .Include("SeasonTeam.Team.Coach")
                                            .Include("SeasonTeam.Team.Sponsor")
                                            .Where(x => x.SeasonTeam.SeasonId == 54).ToList(),
                                      _folderPath + "GameTeams.json");

        //GoalieStatGame
        //GoalieStatSeason
        //GoalieStatSeasonTeam
        //Penalty
        //Phone
        //PhoneType

        _lo30DataSerializationService.ToJsonToFile(_ctx.Players.ToList(),
                                     _folderPath + "Players.json");

        // PlayerDraft
        // PlayerEmail
        // PlayerPhone

        _lo30DataSerializationService.ToJsonToFile(_ctx.PlayerRatings
                                            .Include("Season")
                                            .Include("Player")
                                            .Where(x => x.Season.SeasonId == 54).ToList(),
                                      _folderPath + "PlayerRatings.json");

        //PlayerStatCareer
        //PlayerStatGame
        //PlayerStatSeason
        //PlayerStatSeasonTeam
        //PlayerStatus
        //PlayerStatusType

        _lo30DataSerializationService.ToJsonToFile(_ctx.ScoreSheetEntries
                                            .Include("Game")
                                            .Include("Game.Season")
                                            .Where(x => x.Game.Season.SeasonId == 54).ToList(),
                                      _folderPath + "ScoreSheetEntries.json");

        _lo30DataSerializationService.ToJsonToFile(_ctx.ScoreSheetEntryPenalties
                                            .Include("Game")
                                            .Include("Game.Season")
                                            .Where(x => x.Game.Season.SeasonId == 54).ToList(),
                                      _folderPath + "ScoreSheetEntryPenalties.json");

        _lo30DataSerializationService.ToJsonToFile(_ctx.ScoreSheetEntriesProcessed
                                            .Include("Game")
                                            .Include("Game.Season")
                                            .Include("GoalPlayer")
                                            .Include("Assist1Player")
                                            .Include("Assist2Player")
                                            .Include("Assist3Player")
                                            .Where(x => x.Game.Season.SeasonId == 54).ToList(),
                                      _folderPath + "ScoreSheetEntriesProcessed.json");

        _lo30DataSerializationService.ToJsonToFile(_ctx.ScoreSheetEntryPenaltiesProcessed
                                            .Include("Game")
                                            .Include("Game.Season")
                                            .Include("Player")
                                            .Include("Penalty")
                                            .Where(x => x.Game.Season.SeasonId == 54).ToList(),
                                      _folderPath + "ScoreSheetEntryPenaltiesProcessed.json");

        _lo30DataSerializationService.ToJsonToFile(_ctx.Seasons
                                            .Where(x => x.SeasonId == 54).ToList(),
                                      _folderPath + "Seasons.json");

        _lo30DataSerializationService.ToJsonToFile(_ctx.SeasonTeams
                                            .Include("Season")
                                            .Include("Team")
                                            .Include("Team.Coach")
                                            .Include("Team.Sponsor")
                                            .Where(x => x.Season.SeasonId == 54).ToList(),
                                      _folderPath + "SeasonTeams.json");

        _lo30DataSerializationService.ToJsonToFile(_ctx.Sponsors.ToList(),
                                     _folderPath + "Sponsors.json");

        //SponsorEmail
        //SponsorPhone

        _lo30DataSerializationService.ToJsonToFile(_ctx.Teams
                                            .Include("Coach")
                                            .Include("Sponsor")
                                            .ToList(),
                                      _folderPath + "Teams.json");

        _lo30DataSerializationService.ToJsonToFile(_ctx.TeamRosters
                                            .Include("SeasonTeam")
                                            .Include("SeasonTeam.Season")
                                            .Include("SeasonTeam.Team")
                                            .Include("SeasonTeam.Team.Coach")
                                            .Include("SeasonTeam.Team.Sponsor")
                                            .Include("Player")
                                            .Where(x => x.SeasonTeam.SeasonId == 54).ToList(),
                                      _folderPath + "TeamRosters.json");

        //TeamStanding

        diffFromFirst = DateTime.Now - first;
        Debug.Print("Total TimeToProcess: " + diffFromFirst.ToString());

      }

      public ProcessingResult LoadScoreSheetEntriesFromAccessDBJson()
      {
        var results = new ProcessingResult();

        var folderPath = @"C:\git\LO30\LO30\App_Data\Access\";

        DateTime last = DateTime.Now;
        TimeSpan diffFromLast = new TimeSpan();

        List<ScoreSheetEntry> scoreSheetEntries = ScoreSheetEntry.LoadListFromAccessDbJsonFile(folderPath + "ScoreSheetEntries.json");
        results.toProcess = scoreSheetEntries.Count;
        results.modified = SaveOrUpdateScoreSheetEntry(scoreSheetEntries);
        Debug.Print("LoadScoreSheetEntriesFromAccessDBJson: Saved ScoreSheetEntries " + _ctx.ScoreSheetEntries.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
        results.time = diffFromLast.ToString();

        return results;
      }

      public ProcessingResult LoadScoreSheetEntryPenaltiesFromAccessDBJson()
      {
        var results = new ProcessingResult();

        var folderPath = @"C:\git\LO30\LO30\App_Data\Access\";

        DateTime last = DateTime.Now;
        TimeSpan diffFromLast = new TimeSpan();

        List<ScoreSheetEntryPenalty> scoreSheetEntryPenalties = ScoreSheetEntryPenalty.LoadListFromAccessDbJsonFile(folderPath + "ScoreSheetEntryPenalties.json");
        results.toProcess = scoreSheetEntryPenalties.Count;
        results.modified = SaveOrUpdateScoreSheetEntryPenalty(scoreSheetEntryPenalties);
        Debug.Print("LoadScoreSheetEntryPenaltiesFromAccessDBJson: Saved ScoreSheetEntryPenalties " + _ctx.ScoreSheetEntryPenalties.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
        results.time = diffFromLast.ToString();

        return results;
      }

      public ProcessingResult LoadGameRostersFromAccessDBJson()
      {
        var results = new ProcessingResult();

        var folderPath = @"C:\git\LO30\LO30\App_Data\Access\";

        DateTime last = DateTime.Now;
        TimeSpan diffFromLast = new TimeSpan();

        List<GameRoster> gameRosters = GameRoster.LoadListFromAccessDbJsonFile(folderPath + "GameRosters.json", this, _lo30DataService);
        results.toProcess = gameRosters.Count;
        results.modified = SaveOrUpdateGameRoster(gameRosters);
        Debug.Print("LoadGameRostersFromAccessDBJson: Saved GameRosters " + _ctx.ScoreSheetEntryPenalties.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
        results.time = diffFromLast.ToString();

        return results;
      }
    }
}