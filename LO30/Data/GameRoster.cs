using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace LO30.Data
{
  public class GameRoster
  {
    [Key, Column(Order = 0)]
    public int GameRosterId { get; set; }

    [Required, ForeignKey("GameTeam"), Index("PK2", 1, IsUnique = true)]
    public int GameTeamId { get; set; }

    [Required, Index("PK2", 2, IsUnique = true)]
    public int PlayerNumber { get; set; }

    [Required]
    public bool Goalie { get; set; }

    [Required, ForeignKey("Player")]
    public int PlayerId { get; set; }

    [Required]
    public bool Sub { get; set; }

    [ForeignKey("SubbingForPlayer")]
    public int? SubbingForPlayerId { get; set; }

    public virtual GameTeam GameTeam { get; set; }
    public virtual Player Player { get; set; }
    public virtual Player SubbingForPlayer { get; set; }

    public GameRoster()
    {
    }

    public GameRoster(int gtid, int pn, bool g, int pid, bool sub, int? sfpid)
    {
      this.GameTeamId = gtid;
      this.PlayerNumber = pn;

      this.Goalie = g;
      this.PlayerId = pid;
      this.Sub = sub;
      this.SubbingForPlayerId = sfpid;

      Validate();
    }

    public GameRoster(int grid, int gtid, int pn, bool g, int pid, bool sub, int? sfpid)
    {
      this.GameRosterId = grid;

      this.GameTeamId = gtid;
      this.PlayerNumber = pn;

      this.Goalie = g;
      this.PlayerId = pid;
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
    }

    public static List<GameRoster> LoadListFromJsonFile(string filePath)
    {
      string className = "GameRoster";
      string functionName = "LoadFromJsonFile";
      List<GameRoster> output = new List<GameRoster>();

      Debug.Print(string.Format("{0}: {1} Loading...", functionName, className));
      var start = DateTime.Now;

      string contents = File.ReadAllText(filePath);
      dynamic parsedJson = JsonConvert.DeserializeObject(contents);
      int count = parsedJson.Count;
      Debug.Print(string.Format("{0}: {1} Count:", functionName, className, count));

      for (var d = 0; d < parsedJson.Count; d++)
      {
        if (d > 0 && d % 100 == 0) Debug.Print(string.Format("{0}: {1} Processed:", functionName, className, d));

        var json = parsedJson[d];

 

          int seasonId = json["SEASON_ID"];
          int gameId = json["GAME_ID"];

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

            // TODO fix logic to handle when a goalie subs and plays out.
            bool isGoalie = false;
            var player = context.Players.Find(playerId);
            if (player != null && player.PreferredPosition == "G")
            {
              isGoalie = true;
            }

            var homeGameTeam = _lo30ContextService.FindGameTeam(gameId, homeTeam: true);

        output.Add(new GameRoster(
          gtid: awayGameTeam.GameTeamId, 
          pn: awayPlayerNumber, 
          g: isGoalie, 
          pid: playerId, 
          sub: awayPlayerSubInd, 
          sfpid: subbingForPlayerId
        ));

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

            // TODO fix logic to handle when a goalie subs and plays out.
            isGoalie = false;
            player = context.Players.Find(playerId);
            if (player != null && player.PreferredPosition == "G")
            {
              isGoalie = true;
            }

            var awayGameTeam = _lo30ContextService.FindGameTeam(gameId, homeTeam: false);

        output.Add(new GameRoster(
          gtid: awayGameTeam.GameTeamId, 
          pn: awayPlayerNumber, 
          g: isGoalie, 
          pid: playerId, 
          sub: awayPlayerSubInd, 
          sfpid: subbingForPlayerId
        ));
      }

      Debug.Print(string.Format("{0}: {1} Loaded", functionName, className));
      var end = DateTime.Now - start;
      Debug.Print("TimeToProcess: " + end.ToString());

      return output;
    }
  }
}