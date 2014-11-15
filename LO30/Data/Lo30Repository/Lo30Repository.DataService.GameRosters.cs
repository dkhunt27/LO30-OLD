using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30Repository
  {
    public List<GameRoster> GetGameRosters()
    {
      return _ctx.GameRosters
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
                    .ToList();
    }

    public List<GameRoster> GetGameRostersByGameId(int gameId)
    {
      return GetGameRosters().Where(x => x.GameTeam.GameId == gameId).ToList();
    }

    public List<GameRoster> GetGameRostersByGameIdAndHomeTeam(int gameId, bool homeTeam)
    {
      return GetGameRosters().Where(x => x.GameTeam.GameId == gameId && x.GameTeam.HomeTeam == homeTeam).ToList();
    }

    public GameRoster GetGameRosterByGameRosterId(int gameRosterId)
    {
      return _contextService.FindGameRoster(gameRosterId);
    }

    public GameRoster GetGameRosterByGameTeamIdAndPlayerNumber(int gameTeamId, string playerNumber)
    {
      return _contextService.FindGameRosterByPK2(gameTeamId, playerNumber);
    }
  }
}