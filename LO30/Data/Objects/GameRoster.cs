using LO30.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data.Objects
{
  public class GameRoster
  {
    private const int _gridDefault = 0;
    private const int _rpDefault = 0;
    private const int _rsDefault = 0;

    [Required, Key]
    public int GameRosterId { get; set; }

    [Required, ForeignKey("GameTeam"), Index("PK2", 1, IsUnique = true)]
    public int GameTeamId { get; set; }

    [Required, ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required, Index("PK2", 2, IsUnique = true), MaxLength(3)]
    public string PlayerNumber { get; set; }

    [Required, MaxLength(1)]
    public string Position { get; set; }

    // TODO, make the rating, line required
    //[Required]
    public int RatingPrimary { get; set; }

    //[Required]
    public int RatingSecondary { get; set; }

    //[Required]
    public int Line { get; set; }

    [Required]
    public bool Goalie { get; set; }

    [Required]
    public bool Sub { get; set; }

    [ForeignKey("SubbingForPlayer")]
    public int? SubbingForPlayerId { get; set; }

    public virtual GameTeam GameTeam { get; set; }
    public virtual Player Player { get; set; }
    public virtual Player SubbingForPlayer { get; set; }

    // ctr0
    public GameRoster()
    {
    }

    // ctr1 (pn as int; no grid, rp, rs)
    public GameRoster(int gtid, int pn, int line, string pos, bool g, int pid, bool sub, int? sfpid):
      this(_gridDefault, gtid, pn.ToString(), line, pos, g, pid, _rpDefault, _rsDefault, sub, sfpid)  // ctr6
    {
    }

    // ctr2 (pn as int; no rp, rs)
    public GameRoster(int grid, int gtid, int pn, int line, string pos, bool g, int pid, bool sub, int? sfpid) :
      this(grid, gtid, pn.ToString(), line, pos, g, pid, _rpDefault, _rsDefault, sub, sfpid)  // ctr6
    {
    }

    // ctr3 (pn as string; no grid, rp, rs)
    public GameRoster(int gtid, string pn, int line, string pos, bool g, int pid, bool sub, int? sfpid):
      this(_gridDefault, gtid, pn, line, pos, g, pid, _rpDefault, _rsDefault, sub, sfpid)  // ctr6
    {
    }

    // ctr4  (pn as string; no rp, rs)
    public GameRoster(int grid, int gtid, string pn, int line, string pos, bool g, int pid, bool sub, int? sfpid) :
      this(grid, gtid, pn, line, pos, g, pid, _rpDefault, _rsDefault, sub, sfpid)  // ctr6
    {
    }

    // ctr5  (pn as string; no grid)
    public GameRoster(int gtid, string pn, int line, string pos, bool g, int pid, int rp, int rs, bool sub, int? sfpid) :
      this(_gridDefault, gtid, pn, line, pos, g, pid, rp, rs, sub, sfpid) // ctr6
    {
    }

    // ctr6
    public GameRoster(int grid, int gtid, string pn, int line, string pos, bool g, int pid, int rp, int rs, bool sub, int? sfpid)
    {
      this.GameRosterId = grid;

      this.GameTeamId = gtid;
      this.PlayerId = pid;
      this.PlayerNumber = pn;

      this.Line = line;
      this.Position = pos;
      this.RatingPrimary = rp;
      this.RatingSecondary = rs;
      this.Goalie = g;
      
      this.Sub = sub;
      this.SubbingForPlayerId = sfpid;

      Validate();
    }

    private void Validate()
    {
      var locationKey = string.Format("grid: {0}, gtid: {1}, pn: {2}",
                            this.GameRosterId,
                            this.GameTeamId,
                            this.PlayerNumber);

      if (this.Sub == true && this.SubbingForPlayerId == null)
      {
        throw new ArgumentException("If Sub is true, SubbingForPlayerId must be populated for:" + locationKey, "SubbingForPlayerId");
      }

      if (this.Sub == false && this.SubbingForPlayerId != null)
      {
        throw new ArgumentException("If Sub is false, SubbingForPlayerId must not be populated for:" + locationKey, "SubbingForPlayerId");
      }

      if (this.Position != "G" && this.Position != "D" && this.Position != "F")
      {
        throw new ArgumentException("Position('" + this.Position + "') must be 'G', 'D', or 'F' for:" + locationKey, "Position");
      }

      if (this.Position == "G" && this.Goalie != true)
      {
        throw new ArgumentException("If Position = 'G', Goalie must be true:" + locationKey, "Goalie");
      }

      if (this.Line < 1 || this.Line > 3)
      {
        throw new ArgumentException("Line(" + this.Line + ") must be between 1 and 3:" + locationKey, "Line");
      }

      if (this.RatingPrimary < 0 || this.RatingPrimary > 9)
      {
        throw new ArgumentException("RatingPrimary(" + this.RatingPrimary + ") must be between 0 and 9:" + locationKey, "RatingPrimary");
      }

      if (this.RatingSecondary < 0 || this.RatingSecondary > 8)
      {
        throw new ArgumentException("RatingSecondary(" + this.RatingSecondary + ") must be between 0 and 8:" + locationKey, "RatingSecondary");
      }

    }

    public static List<GameRoster> LoadListFromAccessDbJsonFile(string filePath, Lo30ContextService lo30ContextService, Lo30DataService lo30DataService)
    {
      string className = "GameRoster";
      string functionName = "LoadListFromAccessDbJsonFile";
      List<GameRoster> output = new List<GameRoster>();

      Debug.Print(string.Format("{0}: {1} Loading...", functionName, className));
      var start = DateTime.Now;

      string contents = File.ReadAllText(filePath);
      dynamic parsedJson = JsonConvert.DeserializeObject(contents);
      int count = parsedJson.Count;
      Debug.Print(string.Format("{0}: {1} Count: {2}", functionName, className, count));

      for (var d = 0; d < parsedJson.Count; d++)
      {
        if (d > 0 && d % 100 == 0) Debug.Print(string.Format("{0}: {1} Processed: {2}", functionName, className, d));
        var json = parsedJson[d];

        int seasonId = json["SEASON_ID"];
        int gameId = json["GAME_ID"];

        var game = lo30ContextService.FindGame(gameId);
        var gameDateYYYYMMDD = lo30DataService.ConvertDateTimeIntoYYYYMMDD(game.GameDateTime);

        var homeGameTeamId = lo30ContextService.FindGameTeamByPK2(gameId, homeTeam: true).GameTeamId;
        var awayGameTeamId = lo30ContextService.FindGameTeamByPK2(gameId, homeTeam: false).GameTeamId;

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
          var homeTeamRoster = lo30ContextService.FindTeamRosterWithYYYYMMDD(homeTeamId, homePlayerId, gameDateYYYYMMDD);
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

          var playerRating = lo30ContextService.FindPlayerRatingWithYYYYMMDD(seasonId, playerId, gameDateYYYYMMDD);

          var gameRoster = new GameRoster(
                                  gtid: homeGameTeamId,
                                  pn: homePlayerNumber.ToString(),
                                  line: homePlayerLine,
                                  pos: homePlayerPosition,
                                  g: isGoalie,
                                  pid: playerId,
                                  rp: playerRating.RatingPrimary,
                                  rs: playerRating.RatingSecondary,
                                  sub: homePlayerSubInd,
                                  sfpid: subbingForPlayerId
                            );

          // make sure this gameRoster doesn't have any PK issues.
          // since PK is auto assigned, just check PK2
          //var pkError = lo30ContextService.FindGameRosterByPK2(false, false, gameRoster.GameTeamId, gameRoster.PlayerNumber);

          //if (pkError != null)
          //{
            // this insert will cause a PK error
          //  throw new ArgumentException("The GameRoster will cause a PK2 error. " + gameRoster);
          //}

          output.Add(gameRoster);

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
          var awayTeamRoster = lo30ContextService.FindTeamRosterWithYYYYMMDD(homeTeamId, homePlayerId, gameDateYYYYMMDD);
          int awayPlayerLine = homeTeamRoster.Line;
          string awayPlayerPosition = homeTeamRoster.Position;

          isGoalie = false;
          if (awayTeamRoster.Position == "G")
          {
            isGoalie = true;
          }

          playerRating = lo30ContextService.FindPlayerRatingWithYYYYMMDD(seasonId, playerId, gameDateYYYYMMDD);

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
          //pkError = lo30ContextService.FindGameRosterByPK2(false, false, gameRoster.GameTeamId, gameRoster.PlayerNumber);

          //if (pkError != null)
          //{
            // this insert will cause a PK error
          //  throw new ArgumentException("The GameRoster will cause a PK2 error. " + gameRoster);
          //}

          output.Add(gameRoster);
        }
      }


      Debug.Print(string.Format("{0}: {1} Loaded", functionName, className));
      var end = DateTime.Now - start;
      Debug.Print("TimeToProcess: " + end.ToString());

      return output;
    }
  }
}