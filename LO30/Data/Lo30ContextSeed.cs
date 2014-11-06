using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace LO30.Data
{
  public class Lo30ContextSeed
  {
    private Lo30ContextService _lo30ContextService;
    private AccessDatabaseService _accessDatabaseService;
    private Lo30DataService _lo30DataService;
    private Lo30ContextService contextService;
    private Lo30Context context;

    public Lo30ContextSeed(Lo30Context context2, Lo30ContextService contextService2)
    {
      context = context2;
      contextService = contextService2;
    }

    public void Seed()
    {
      //string appRoot = Environment.GetEnvironmentVariable("RoleRoot");
      //string folderPath2 = Path.Combine(appRoot + @"\", string.Format(@"approot\{0}", "Penalties.json"));

      string appDataPath = @"C:\git\LO30\LO30\App_Data\";
      if (ConfigurationManager.AppSettings["ENV"].ToString() == "PROD")
      {
        appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      }
      var folderPath = Path.Combine(appDataPath, "Access");
      folderPath = folderPath + @"\";

      _lo30ContextService = contextService;
      _accessDatabaseService = new AccessDatabaseService();
      _lo30DataService = new Lo30DataService();

      DateTime first = DateTime.Now;
      DateTime last = DateTime.Now;
      TimeSpan diffFromFirst = new TimeSpan();
      TimeSpan diffFromLast = new TimeSpan();

      #region not populated for now

      #region 0:Addresses
      if (context.Addresses.Count() == 0)
      {
      }
      #endregion

      #region 0:Emails
      if (context.Emails.Count() == 0)
      {
      }
      #endregion

      #region 0:Phones
      if (context.Phones.Count() == 0)
      {
      }
      #endregion

      #region 0:PlayerEmails
      if (context.PlayerEmails.Count() == 0)
      {
      }
      #endregion

      #region 0:PlayerPhones
      if (context.PlayerPhones.Count() == 0)
      {
      }
      #endregion

      #region 0:PlayerStatsCareer
      if (context.PlayerStatsCareer.Count() == 0)
      {
      }
      #endregion

      #region 0:PlayerStatsGame
      if (context.PlayerStatsGame.Count() == 0)
      {
      }
      #endregion

      #region 0:PlayerStatsSeason
      if (context.PlayerStatsSeason.Count() == 0)
      {
      }
      #endregion

      #region 0:PlayerStatuses
      if (context.PlayerStatuses.Count() == 0)
      {
      }
      #endregion

      #region 0:Sponsors
      if (context.Sponsors.Count() == 0)
      {
      }
      #endregion

      #region 0:SponsorEmails
      if (context.SponsorEmails.Count() == 0)
      {
      }
      #endregion

      #region 0:SponsorPhones
      if (context.SponsorPhones.Count() == 0)
      {
      }
      #endregion

      #endregion

      #region 1:Articles
      if (context.Articles.Count() == 0)
      {
        Debug.Print("Data Group 1: Creating Articles");
        last = DateTime.Now;

        var article = new Article()
        {
          Title = "Payment Schedule",
          Created = new DateTime(2014, 09, 10, 20, 5, 32),
          Body = "Payments are due the Third Thursday of every month as follows:",
        };

        context.Articles.Add(article);

        article = new Article()
        {
          Title = "Game Schedule",
          Created = new DateTime(2014, 09, 8, 15, 12, 41),
          Body = "The schedule has been updated and posted"
        };

        context.Articles.Add(article);

        Debug.Print("Data Group 1: Created Articles");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 1: Saved Articles" + context.PlayerStatusTypes.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 1:EmailTypes
      if (context.EmailTypes.Count() == 0)
      {
        Debug.Print("Data Group 1: Creating EmailTypes");
        last = DateTime.Now;

        var emailType = new EmailType()
        {
          EmailTypeName = "work"
        };

        context.EmailTypes.Add(emailType);

        emailType = new EmailType()
        {
          EmailTypeName = "home"
        };

        context.EmailTypes.Add(emailType);

        emailType = new EmailType()
        {
          EmailTypeName = "other"
        };

        context.EmailTypes.Add(emailType);

        Debug.Print("Data Group 1: Created EmailTypes");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 1: Saved EmailTypes" + context.EmailTypes.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 1:Penalties
      if (context.Penalties.Count() == 0)
      {
        Debug.Print("Data Group 1: Creating Penalties");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(Path.Combine(folderPath, "Penalties.json"));
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          var penalty = new Penalty()
          {
            PenaltyId = json["PENALTY_ID"],
            PenaltyCode = json["PENALTY_SHORT_DESC"],
            PenaltyName = json["PENALTY_LONG_DESC"],
            DefaultPenaltyMinutes = json["DEFAULT_PENALTY_MINUTES"],
            StickPenalty = json["STICK_PENALTY"]
          };

          context.Penalties.Add(penalty);
        }

        Debug.Print("Data Group 1: Created Penalties");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 1: Saved Penalties" + context.Penalties.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }

      #endregion

      #region 1:PhoneTypes
      if (context.PhoneTypes.Count() == 0)
      {
        Debug.Print("Data Group 1: Creating PhoneTypes");
        last = DateTime.Now;

        var phoneType = new PhoneType()
        {
          PhoneTypeName = "work"
        };

        context.PhoneTypes.Add(phoneType);

        phoneType = new PhoneType()
        {
          PhoneTypeName = "home"
        };

        context.PhoneTypes.Add(phoneType);

        phoneType = new PhoneType()
        {
          PhoneTypeName = "cell"
        };

        context.PhoneTypes.Add(phoneType);

        phoneType = new PhoneType()
        {
          PhoneTypeName = "other"
        };

        context.PhoneTypes.Add(phoneType);

        Debug.Print("Data Group 1: Created PhoneTypes");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 1: Saved PhoneTypes" + context.PhoneTypes.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion
      
      #region 1:PlayerStatusTypes
      if (context.PlayerStatusTypes.Count() == 0)
      {
        Debug.Print("Data Group 1: Creating PlayerStatusTypes");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "Statuses.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          var playerStatusType = new PlayerStatusType()
          {
            PlayerStatusTypeId = json["STATUS_ID"],
            PlayerStatusTypeName = json["STATUS_DESC"]
          };

          context.PlayerStatusTypes.Add(playerStatusType);
        }

        Debug.Print("Data Group 1: Created PlayerStatusTypes");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 1: Saved PlayerStatusTypes " + context.PlayerStatusTypes.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 2:Seasons
      if (context.Seasons.Count() == 0)
      {
        Debug.Print("Data Group 2: Creating Seasons");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "Seasons.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          DateTime? startDate = null;
          DateTime? endDate = null;

          if (json["START_DATE"] != null)
          {
            startDate = json["START_DATE"];
          }

          if (json["END_DATE"] != null)
          {
            endDate = json["END_DATE"];
          }

          int seasonId = json["SEASON_ID"];

          if (seasonId == 54)
          {
            startDate = new DateTime(2014, 9, 4);
            endDate = new DateTime(2015, 3, 29);
          }

          var season = new Season()
          {
            SeasonId = seasonId,
            SeasonName = json["SEASON_NAME"],
            IsCurrentSeason = json["CURRENT_SEASON_IND"],
            StartYYYYMMDD = _lo30DataService.ConvertDateTimeIntoYYYYMMDD(startDate),
            EndYYYYMMDD = _lo30DataService.ConvertDateTimeIntoYYYYMMDD(endDate)
          };

          context.Seasons.Add(season);
        }

        Debug.Print("Data Group 2: Created Seasons");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 2: Saved Seasons " + context.Seasons.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 2:Teams
      if (context.Teams.Count() == 0)
      {
        Debug.Print("Data Group 2: Creating Teams");
        last = DateTime.Now;

        #region add position night teams
        var team = new Team()
        {
          TeamShortName = "1st",
          TeamLongName = "1st Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "2nd",
          TeamLongName = "2nd Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "3rd",
          TeamLongName = "3rd Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "4th",
          TeamLongName = "4th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "5th",
          TeamLongName = "5th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "6th",
          TeamLongName = "6th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "7th",
          TeamLongName = "7th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "8th",
          TeamLongName = "8th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "9th",
          TeamLongName = "9th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "10th",
          TeamLongName = "10th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "11th",
          TeamLongName = "11th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "12th",
          TeamLongName = "12th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "13th",
          TeamLongName = "13th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "14th",
          TeamLongName = "14th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "15th",
          TeamLongName = "15th Place Team Placeholder"
        };
        context.Teams.Add(team);

        team = new Team()
        {
          TeamShortName = "16th",
          TeamLongName = "16th Place Team Placeholder"
        };
        context.Teams.Add(team);
        #endregion

        var sql = "SELECT DISTINCT TEAM_SHORT_NAME, TEAM_LONG_NAME FROM TEAM";

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "Teams.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          team = new Team()
          {
            TeamShortName = json["TEAM_SHORT_NAME"],
            TeamLongName = json["TEAM_LONG_NAME"]
          };

          context.Teams.Add(team);
        }

        Debug.Print("Data Group 2: Created Teams");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 2: Saved Teams " + context.Teams.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 2:Players
      if (context.Players.Count() == 0)
      {
        Debug.Print("Data Group 2: Creating Players");
        last = DateTime.Now;

        var player = new Player()
        {
          PlayerId = 0,
          FirstName = "Unknown",
          LastName = "Player",
          Suffix = null,
          PreferredPosition = "X",
          Shoots = "X",
          BirthDate = DateTime.Parse("1/1/1970"),
          Profession = null,
          WifesName = null
        };

        context.Players.Add(player);

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "Players.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];
          int playerId = json["PLAYER_ID"];

          string firstName = json["PLAYER_FIRST_NAME"];
          if (string.IsNullOrWhiteSpace(firstName)) {
            firstName = "_";
          };

          string lastName = json["PLAYER_LAST_NAME"];
          if (string.IsNullOrWhiteSpace(lastName)) {
            lastName = "_";
          };

          string position, positionMapped;
          position = json["PLAYER_POSITION"];

          if (string.IsNullOrWhiteSpace(position))
          {
            position = "X";
          }

          switch (position.ToLower()) 
          {
            case "f":
            case "forward":
              positionMapped = "F";
              break;
            case "d":
            case "defense":
              positionMapped = "D";
              break;
            case "g":
            case "goal":
            case "goalie":
              positionMapped = "G";
              break;
            default:
              positionMapped = "X";
              break;
          }

          string shoots, shootsMapped;
          shoots = json["SHOOTS"];
          if (string.IsNullOrWhiteSpace(shoots))
          {
            shoots = "X";
          }

          switch (shoots.ToLower()) 
          {
            case "l":
              shootsMapped = "L";
              break;
            case "r":
              shootsMapped = "R";
              break;
            default:
              shootsMapped = "X";
              break;
          }

          DateTime? birthDate = null;

          if (json["BIRTHDATE"] != null)
          {
            birthDate = json["BIRTHDATE"];
          }

          player = new Player()
          {
            PlayerId = playerId,
            FirstName = firstName,
            LastName = lastName,
            Suffix = json["PLAYER_SUFFIX"],
            PreferredPosition = positionMapped,
            Shoots = shootsMapped,
            BirthDate = birthDate,
            Profession = json["PROFESSION"],
            WifesName = json["WIFES_NAME"]
          };

          context.Players.Add(player);
        }

        Debug.Print("Data Group 2: Created Players");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 2: Saved Players " + context.Players.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 3:SeasonTeam
      if (context.SeasonTeams.Count() == 0)
      {
        Debug.Print("Data Group 3: Creating SeasonTeams");
        last = DateTime.Now;

        #region add the position night placeholders for this season
        var team = context.Teams.Where(t => t.TeamShortName == "1st").FirstOrDefault();
        var seasonTeam = new SeasonTeam(stid: 321, sid: 54, tid: team.TeamId);
        context.SeasonTeams.Add(seasonTeam);

        team = context.Teams.Where(t => t.TeamShortName == "2nd").FirstOrDefault();
        seasonTeam = new SeasonTeam(stid: 322, sid: 54, tid: team.TeamId);
        context.SeasonTeams.Add(seasonTeam);

        team = context.Teams.Where(t => t.TeamShortName == "3rd").FirstOrDefault();
        seasonTeam = new SeasonTeam(stid: 323, sid: 54, tid: team.TeamId);
        context.SeasonTeams.Add(seasonTeam);

        team = context.Teams.Where(t => t.TeamShortName == "4th").FirstOrDefault();
        seasonTeam = new SeasonTeam(stid: 324, sid: 54, tid: team.TeamId);
        context.SeasonTeams.Add(seasonTeam);

        team = context.Teams.Where(t => t.TeamShortName == "5th").FirstOrDefault();
        seasonTeam = new SeasonTeam(stid: 325, sid: 54, tid: team.TeamId);
        context.SeasonTeams.Add(seasonTeam);

        team = context.Teams.Where(t => t.TeamShortName == "6th").FirstOrDefault();
        seasonTeam = new SeasonTeam(stid: 326, sid: 54, tid: team.TeamId);
        context.SeasonTeams.Add(seasonTeam);

        team = context.Teams.Where(t => t.TeamShortName == "7th").FirstOrDefault();
        seasonTeam = new SeasonTeam(stid: 327, sid: 54, tid: team.TeamId);
        context.SeasonTeams.Add(seasonTeam);

        team = context.Teams.Where(t => t.TeamShortName == "8th").FirstOrDefault();
        seasonTeam = new SeasonTeam(stid: 328, sid: 54, tid: team.TeamId);
        context.SeasonTeams.Add(seasonTeam);
        #endregion

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "Teams.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          string teamShortName = json["TEAM_SHORT_NAME"];

          team = context.Teams.Where(t => t.TeamShortName == teamShortName).FirstOrDefault();

          int stid = json["TEAM_ID"];
          int sid = json["SEASON_ID"];
          int tid = team.TeamId;

          seasonTeam = new SeasonTeam(stid, sid, tid);

          context.SeasonTeams.Add(seasonTeam);
        }

        Debug.Print("Data Group 3: Created SeasonTeams");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 3: Saved SeasonTeams " + context.SeasonTeams.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion
      
      #region 3:Games
      if (context.Games.Count() == 0)
      {
        Debug.Print("Data Group 3: Creating Games");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "Games.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          int seasonId = json["SEASON_ID"];
          int gameId = json["GAME_ID"];
          DateTime gameDate = json["GAME_DATE"];
          DateTime gameTime = json["GAME_TIME"];
          bool playoffGame = json["PLAYOFF_GAME_IND"];

          var timeSpan = new TimeSpan(gameTime.Hour, gameTime.Minute, gameTime.Second);

          var gameDateTime = gameDate.Add(timeSpan);

          var game = new Game(
                gid: gameId,
                time: gameDateTime,
                loc: "not set",
                sid: seasonId,
                play: playoffGame
          );

          context.Games.Add(game);
        }

        Debug.Print("Data Group 3: Created Games");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 3: Saved Games " + context.Games.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 3:GameTeams
      if (context.GameTeams.Count() == 0)
      {
        Debug.Print("Data Group 3: Creating GameTeams");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "Games.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          int gameId = json["GAME_ID"];
          int homeTeamId, awayTeamId;

          switch (gameId)
          {
            case 3276:
              homeTeamId = 322;
              awayTeamId = 321;
              break;
            case 3277:
              homeTeamId = 324;
              awayTeamId = 323;
              break;
            case 3278:
              homeTeamId = 326;
              awayTeamId = 325;
              break;
            case 3279:
              homeTeamId = 328;
              awayTeamId = 327;
              break;
            case 3316:
              homeTeamId = 328;
              awayTeamId = 327;
              break;
            case 3317:
              homeTeamId = 326;
              awayTeamId = 325;
              break;
            case 3318:
              homeTeamId = 324;
              awayTeamId = 323;
              break;
            case 3319:
              homeTeamId = 322;
              awayTeamId = 321;
              break;
            default:
              homeTeamId = json["HOME_TEAM_ID"];
              awayTeamId = json["AWAY_TEAM_ID"];
              break;
          };

          var gameTeam = new GameTeam(gid: gameId, ht: true, stid: homeTeamId);
          context.GameTeams.Add(gameTeam);

          gameTeam = new GameTeam(gid: gameId, ht: false, stid: awayTeamId);
          context.GameTeams.Add(gameTeam);
        }

        Debug.Print("Data Group 3: Created GameTeams");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 3: Saved GameTeams " + context.GameTeams.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 3:PlayerDrafts
      if (context.PlayerDrafts.Count() == 0)
      {
        Debug.Print("Data Group 3: Creating PlayerDrafts");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "PlayerRatings.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          int seasonId = json["SEASON_ID"];
          int playerId = json["PLAYER_ID"];

          string draftRound = "";
          string position = "";
          if (json["PLAYER_DRAFT_ROUND"] != null)
          {
            draftRound = json["PLAYER_DRAFT_ROUND"];
          }

          int round = -1;
          int line = -1;

          switch (draftRound.ToLower())
          {
            case "g":
            case "1g":
            case "2g":
            case "3g":
            case "4g":
            case "5g":
            case "6g":
            case "7g":
            case "8g":
              position = "G";
              line = 1;
              round = 1;
              break;
            case "1d":
              position = "D";
              line = 1;
              round = 2;
              break;
            case "2d":
              position = "D";
              line = 1;
              round = 5;
              break;
            case "3d":
              position = "D";
              line = 2;
              round = 8;
              break;
            case "4d":
              position = "D";
              line = 2;
              round = 11;
              break;
            case "5d":
              position = "D";
              line = 3;
              break;
            case "6d":
              position = "D";
              line = 3;
              round = 14;
              break;
            case "1f":
              position = "F";
              line = 1;
              round = 3;
              break;
            case "2f":
              position = "F";
              line = 1;
              round = 4;
              break;
            case "3f":
              position = "F";
              line = 1;
              round = 6;
              break;
            case "4f":
              position = "F";
              line = 2;
              round = 7;
              break;
            case "5f":
              position = "F";
              line = 2;
              round = 9;
              break;
            case "6f":
              position = "F";
              line = 2;
              round = 10;
              break;
            case "7f":
              position = "F";
              line = 3;
              round = 12;
              break;
            case "8f":
              position = "F";
              line = 3;
              round = 13;
              break;
            case "9f":
              position = "F";
              line = 3;
              round = 15;
              break;
          }
          if (playerId == 545 || playerId == 512 || playerId == 426 || playerId == 432 || playerId == 381 || playerId == 282)
          {
            // skip these players...they do not exist in the players table
          }
          else
          {
            var playerDraft = new PlayerDraft()
            {
              SeasonId = seasonId,
              PlayerId = playerId,
              Round = round,
              Order = -1,
              Position = position,
              Line = line,
              Special = false
            };

            context.PlayerDrafts.Add(playerDraft);
          }
        }

        Debug.Print("Data Group 3: Created PlayerDrafts");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 3: Saved PlayerDrafts " + context.PlayerDrafts.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 3:PlayerRatings...dependency on Season, Players, and PlayerDrafts
      if (context.PlayerRatings.Count() == 0)
      {
        Debug.Print("Data Group 3: Creating PlayerRatings");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "PlayerRatings.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          string[] ratingParts = new string[0];

          if (json["PLAYER_RATING"] != null)
          {
            string rating = json["PLAYER_RATING"];
            ratingParts = rating.Split('.');
          }

          int ratingPrimary = -1;
          int ratingSecondary = -1;
          if (ratingParts.Length > 0)
          {
            ratingPrimary = Convert.ToInt32(ratingParts[0]);
            ratingSecondary = 0;
            if (ratingParts.Length > 1)
            {
              ratingSecondary = Convert.ToInt32(ratingParts[1]);
            }
          }

          int line = 0;
          if (json["PLAYER_LINE"] != null)
          {
            line = json["PLAYER_LINE"];
          }

          int seasonId = json["SEASON_ID"];
          int playerId = json["PLAYER_ID"];

          if (playerId == 545 || playerId == 512 || playerId == 426 || playerId == 432 || playerId == 381 || playerId == 282)
          {
            // skip these players...they do not exist in the players table
          }
          else
          {

            var season = _lo30ContextService.FindSeason(seasonId);
            var playerDraft = _lo30ContextService.FindPlayerDraft(seasonId, playerId);

            // default the players rating to the start/end of the season
            var playerRating = new PlayerRating(
                                      sid: seasonId,
                                      pid: playerId,
                                      symd: season.StartYYYYMMDD,
                                      eymd: season.EndYYYYMMDD,
                                      rp: ratingPrimary,
                                      rs: ratingSecondary,
                                      line: line
                                      );

            context.PlayerRatings.Add(playerRating);
          }
        }

        Debug.Print("Data Group 3: Created PlayerRatings");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 3: Saved PlayerRatings " + context.PlayerRatings.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        // add missing players with default rating (0.0)

        var players = context.Players.ToList();

        foreach (var player in players)
        {
          var found = context.PlayerRatings.Where(x => x.PlayerId == player.PlayerId).ToList();

          if (found == null || found.Count == 0)
          {
            // TODO, remove hard coding
            var season = _lo30ContextService.FindSeason(54);

            // default the players rating to the start/end of the season
            var playerRating = new PlayerRating(
                                      sid: season.SeasonId,
                                      pid: player.PlayerId,
                                      symd: season.StartYYYYMMDD,
                                      eymd: season.EndYYYYMMDD
                                      );

            context.PlayerRatings.Add(playerRating);
          }
        }
      }
      #endregion

      #region 3:TeamRosters...dependency on Season, SeasonTeam, PlayerDrafts, and PlayerRatings
      if (context.TeamRosters.Count() == 0)
      {
        Debug.Print("Data Group 3: Creating TeamRosters");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "TeamRosters.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          int seasonId = json["SEASON_ID"];

          // ONLY PROCESS THIS YEARS...TODO implement position/draft lookup for other years
          if (seasonId == 54)
          {

            int teamId = json["TEAM_ID"];
            int playerId = json["PLAYER_ID"];
            bool playoff = json["PLAYOFF_SEASON_IND"];

            int playerNumber = -1;
            if (json["PLAYER_NUMBER"] != null)
            {
              playerNumber = json["PLAYER_NUMBER"];
            }

            // based on the draft spot, determine team roster line and position
            var seasonTeam = _lo30ContextService.FindSeasonTeam(teamId);
            var season = _lo30ContextService.FindSeason(seasonId);
            var playerRating = _lo30ContextService.FindPlayerRatingWithYYYYMMDD(seasonTeam.SeasonId, playerId, season.StartYYYYMMDD);
            var playerDraft = _lo30ContextService.FindPlayerDraft(seasonTeam.SeasonId, playerId);

            // default the team roster to the start/end of the season
            var teamRoster = new TeamRoster(
                                      stid: teamId,
                                      pid: playerId,
                                      symd: season.StartYYYYMMDD,
                                      eymd: season.EndYYYYMMDD,
                                      pos: playerDraft.Position,
                                      rp: playerRating.RatingPrimary,
                                      rs: playerRating.RatingSecondary,
                                      line: playerDraft.Line,
                                      pn: playerNumber
                              );

            context.TeamRosters.Add(teamRoster);
          }
        }

        Debug.Print("Data Group 3: Created TeamRosters");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 3: Saved TeamRosters " + context.TeamRosters.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 4:GameRosters...dependency on Games, PlayerRatings, GameTeams, and TeamRoster
      if (context.GameRosters.Count() == 0)
      {
        Debug.Print("Data Group 4: Creating GameRosters");
        last = DateTime.Now;

        dynamic parsedJson = _accessDatabaseService.ParseObjectFromJsonFile(folderPath + "GameRosters.json");
        int count = parsedJson.Count;

        Debug.Print("Access records to process:" + count);

        for (var d = 0; d < parsedJson.Count; d++)
        {
          if (d % 100 == 0) { Debug.Print("Access records processed:" + d); }
          var json = parsedJson[d];

          int seasonId = json["SEASON_ID"];
          int gameId = json["GAME_ID"];

          var game = _lo30ContextService.FindGame(gameId);
          var gameDateYYYYMMDD = _lo30DataService.ConvertDateTimeIntoYYYYMMDD(game.GameDateTime);

          var homeGameTeamId = _lo30ContextService.FindGameTeamByPK2(gameId, homeTeam: true).GameTeamId;
          var awayGameTeamId = _lo30ContextService.FindGameTeamByPK2(gameId, homeTeam: false).GameTeamId;

          // ONLY PROCESS THIS YEARS...TODO speed up to process historic data
          if (seasonId == 54 && gameId >= 3200)
          {
            int homeTeamId = -1;
            if (json["HOME_TEAM_ID"] != null)
            {
              homeTeamId = json["HOME_TEAM_ID"];
            }

            int homePlayerId = -1;
            if (json["HOME_PLAYER_ID"] != null)
            {
              homePlayerId = json["HOME_PLAYER_ID"];
            }

            int homeSubPlayerId = -1;
            if (json["HOME_SUB_FOR_PLAYER_ID"] != null)
            {
              homeSubPlayerId = json["HOME_SUB_FOR_PLAYER_ID"];
            }

            bool homePlayerSubInd = false;
            if (json["HOME_PLAYER_SUB_IND"] != null)
            {
              homePlayerSubInd = json["HOME_PLAYER_SUB_IND"];
            }

            int homePlayerNumber = -1;
            if (json["HOME_PLAYER_NUMBER"] != null)
            {
              homePlayerNumber = json["HOME_PLAYER_NUMBER"];
            }

            if (homeTeamId == -1)
            {
              Debug.Print(string.Format("The homeTeamId is -1, not sure how to process. homeTeamId:{0}, homePlayerId:{1}, homeSubPlayerId:{2}, homePlayerSubInd:{3}, homePlayerNumber:{4}, gameId:{5}", homeTeamId, homePlayerId, homeSubPlayerId, homePlayerSubInd, homePlayerNumber, gameId));
            }
            else if (homePlayerId == -1)
            {
              Debug.Print(string.Format("The homePlayerId is -1, not sure how to process. homeTeamId:{0}, homePlayerId:{1}, homeSubPlayerId:{2}, homePlayerSubInd:{3}, homePlayerNumber:{4}, gameId:{5}", homeTeamId, homePlayerId, homeSubPlayerId, homePlayerSubInd, homePlayerNumber, gameId));
            }
            else if (homePlayerNumber == -1)
            {
              Debug.Print(string.Format("The homePlayerId is -1, not sure how to process. homeTeamId:{0}, homePlayerId:{1}, homeSubPlayerId:{2}, homePlayerSubInd:{3}, homePlayerNumber:{4}, gameId:{5}", homeTeamId, homePlayerId, homeSubPlayerId, homePlayerSubInd, homePlayerNumber, gameId));
            }

            // set the line and position equal to the players drafted / set line position from the team roster
            var homeTeamRoster = _lo30ContextService.FindTeamRosterWithYYYYMMDD(homeTeamId, homePlayerId, gameDateYYYYMMDD);
            int homePlayerLine = homeTeamRoster.Line;
            string homePlayerPosition = homeTeamRoster.Position;

            int playerId;
            int? subbingForPlayerId;

            if (homePlayerSubInd)
            {
              playerId = homeSubPlayerId;
              subbingForPlayerId = homePlayerId;
            }
            else
            {
              playerId = homePlayerId;
              subbingForPlayerId = null;
            }

            bool isGoalie = false;
            if (homeTeamRoster.Position == "G")
            {
              isGoalie = true;
            }

            var playerRating = _lo30ContextService.FindPlayerRatingWithYYYYMMDD(seasonId, playerId, gameDateYYYYMMDD);

            var gameRoster = new GameRoster(
                                    gtid: homeGameTeamId, 
                                    pn: homePlayerNumber.ToString(), 
                                    line: homePlayerLine, 
                                    pos: homePlayerPosition, 
                                    g: isGoalie, 
                                    pid: playerId, 
                                    rp: playerRating.RatingPrimary, 
                                    rs:playerRating.RatingSecondary, 
                                    sub: homePlayerSubInd, 
                                    sfpid: subbingForPlayerId
                              );

            // make sure this gameRoster doesn't have any PK issues.
            // since PK is auto assigned, just check PK2
            var pkError = _lo30ContextService.FindGameRosterByPK2(false, false, gameRoster.GameTeamId, gameRoster.PlayerNumber);

            if (pkError != null)
            {
              // this insert will cause a PK error
              throw new ArgumentException("The GameRoster will cause a PK2 error. " + gameRoster);
            }

            context.GameRosters.Add(gameRoster);

            int awayTeamId = -1;
            if (json["AWAY_TEAM_ID"] != null)
            {
              awayTeamId = json["AWAY_TEAM_ID"];
            }

            int awayPlayerId = -1;
            if (json["AWAY_PLAYER_ID"] != null)
            {
              awayPlayerId = json["AWAY_PLAYER_ID"];
            }

            int awaySubPlayerId = -1;
            if (json["AWAY_SUB_FOR_PLAYER_ID"] != null)
            {
              awaySubPlayerId = json["AWAY_SUB_FOR_PLAYER_ID"];
            }

            bool awayPlayerSubInd = false;
            if (json["AWAY_PLAYER_SUB_IND"] != null)
            {
              awayPlayerSubInd = json["AWAY_PLAYER_SUB_IND"];

            }

            int awayPlayerNumber = -1;
            if (json["AWAY_PLAYER_NUMBER"] != null)
            {
              awayPlayerNumber = json["AWAY_PLAYER_NUMBER"];
            }

            if (awayTeamId == -1)
            {
              Debug.Print(string.Format("The awayTeamId is -1, not sure how to process. awayTeamId:{0}, awayPlayerId:{1}, awaySubPlayerId:{2}, awayPlayerSubInd:{3}, awayPlayerNumber:{4}, gameId:{5}", awayTeamId, awayPlayerId, awaySubPlayerId, awayPlayerSubInd, awayPlayerNumber, gameId));
            }
            else if (awayPlayerId == -1)
            {
              Debug.Print(string.Format("The awayPlayerId is -1, not sure how to process. awayTeamId:{0}, awayPlayerId:{1}, awaySubPlayerId:{2}, awayPlayerSubInd:{3}, awayPlayerNumber:{4}, gameId:{5}", awayTeamId, awayPlayerId, awaySubPlayerId, awayPlayerSubInd, awayPlayerNumber, gameId));
            }
            else if (awayPlayerNumber == -1)
            {
              Debug.Print(string.Format("The awayPlayerId is -1, not sure how to process. awayTeamId:{0}, awayPlayerId:{1}, awaySubPlayerId:{2}, awayPlayerSubInd:{3}, awayPlayerNumber:{4}, gameId:{5}", awayTeamId, awayPlayerId, awaySubPlayerId, awayPlayerSubInd, awayPlayerNumber, gameId));
            }

            if (awayPlayerSubInd)
            {
              playerId = awaySubPlayerId;
              subbingForPlayerId = awayPlayerId;
            }
            else
            {
              playerId = awayPlayerId;
              subbingForPlayerId = null;
            }

            // set the line and position equal to the players drafted / set line position from the team roster
            var awayTeamRoster = _lo30ContextService.FindTeamRosterWithYYYYMMDD(homeTeamId, homePlayerId, gameDateYYYYMMDD);
            int awayPlayerLine = homeTeamRoster.Line;
            string awayPlayerPosition = homeTeamRoster.Position;

            isGoalie = false;
            if (awayTeamRoster.Position == "G")
            {
              isGoalie = true;
            }

            playerRating = _lo30ContextService.FindPlayerRatingWithYYYYMMDD(seasonId, playerId, gameDateYYYYMMDD);

            gameRoster = new GameRoster(
                                    gtid: awayGameTeamId,
                                    pn: awayPlayerNumber.ToString(),
                                    line: awayPlayerLine,
                                    pos: awayPlayerPosition,
                                    g: isGoalie,
                                    pid: playerId,
                                    rp: playerRating.RatingPrimary,
                                    rs: playerRating.RatingSecondary,
                                    sub: awayPlayerSubInd,
                                    sfpid: subbingForPlayerId
                              );



            // make sure this gameRoster doesn't have any PK issues.
            // since PK is auto assigned, just check PK2
            pkError = _lo30ContextService.FindGameRosterByPK2(false, false, gameRoster.GameTeamId, gameRoster.PlayerNumber);

            if (pkError != null)
            {
              // this insert will cause a PK error
              throw new ArgumentException("The GameRoster will cause a PK2 error. " + gameRoster);
            }

            context.GameRosters.Add(gameRoster);
          }
        }

        Debug.Print("Data Group 4: Created GameRosters");
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());

        _lo30ContextService.ContextSaveChanges();
        Debug.Print("Data Group 4: Saved GameRosters " + context.GameRosters.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 4:ScoreSheetEntries...using loadJson
      if (context.ScoreSheetEntries.Count() == 0)
      {
        List<ScoreSheetEntry> scoreSheetEntries = ScoreSheetEntry.LoadListFromAccessDbJsonFile(folderPath + "ScoreSheetEntries.json");
        _lo30ContextService.SaveOrUpdateScoreSheetEntry(scoreSheetEntries);
        Debug.Print("Data Group 4: Saved ScoreSheetEntries " + context.ScoreSheetEntries.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      #region 4:ScoreSheetEntryPenalties...using loadJson
      if (context.ScoreSheetEntryPenalties.Count() == 0)
      {
        List<ScoreSheetEntryPenalty> scoreSheetEntryPenalties = ScoreSheetEntryPenalty.LoadListFromAccessDbJsonFile(folderPath + "ScoreSheetEntryPenalties.json");
        _lo30ContextService.SaveOrUpdateScoreSheetEntryPenalty(scoreSheetEntryPenalties);
        Debug.Print("Data Group 4: Saved ScoreSheetEntryPenalties " + context.ScoreSheetEntries.Count());
        diffFromLast = DateTime.Now - last;
        Debug.Print("TimeToProcess: " + diffFromLast.ToString());
      }
      #endregion

      diffFromFirst = DateTime.Now - first;
      Debug.Print("Total TimeToProcess: " + diffFromFirst.ToString());

      #region populated via other processes
      #region 99:GameOutcomes
      if (context.GameOutcomes.Count() == 0)
      {
        // populated via other process
      }
      #endregion

      #region 99:TeamStandings
      if (context.TeamStandings.Count() == 0)
      {
        // populated via other process
      }
      #endregion
      #endregion
    }
  }
}
