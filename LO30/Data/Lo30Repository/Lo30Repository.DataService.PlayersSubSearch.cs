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
    public List<PlayerSubSearch> GetPlayersSubSearch(string position, string ratingMin, string ratingMax)
    {
      int ratingMinPrimary = -1;
      int ratingMinSecondary = -1;
      int ratingMaxPrimary = -1;
      int ratingMaxSecondary = -1;
      _lo30DataService.ConvertCombinedRatingToPrimarySecondary(ratingMin, out ratingMinPrimary, out ratingMinSecondary);
      _lo30DataService.ConvertCombinedRatingToPrimarySecondary(ratingMax, out ratingMaxPrimary, out ratingMaxSecondary);
      
      var players = _ctx.Players.ToList();

      var todayYYYYMMDD = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
      var currentSeasonId = _contextService.FindSeasonWithIsCurrentSeason(isCurrentSeason: true).SeasonId;

      var playersSubSearch = new List<PlayerSubSearch>();

      foreach (var player in players)
      {
        var playerRating = _contextService.FindPlayerRatingWithYYYYMMDD(player.PlayerId, position, currentSeasonId, todayYYYYMMDD);

        bool errorIfNotFound = false;
        bool errorIfMoreThanOneFound = true;
        bool populateFully = true;

        var teamRoster = _contextService.FindTeamRosterWithYYYYMMDD(player.PlayerId, todayYYYYMMDD, errorIfNotFound, errorIfMoreThanOneFound, populateFully);

        string teamName = null;
        if (teamRoster != null)
        {
          teamName = teamRoster.SeasonTeam.Team.TeamShortName;
        }


        if (ratingMinPrimary <= playerRating.RatingPrimary && ratingMinSecondary <= playerRating.RatingSecondary &&
            playerRating.RatingPrimary <= ratingMaxPrimary && playerRating.RatingSecondary <= ratingMaxSecondary)
        {
          var playerSubSearch = new PlayerSubSearch()
          {
            PlayerId = player.PlayerId,
            FirstName = player.FirstName,
            LastName = player.LastName,
            Suffix = player.Suffix,
            Position = position,
            TeamName = teamName,
            Rating = _lo30DataService.ConvertRatingPrimarySecondaryToCombined(playerRating.RatingPrimary, playerRating.RatingSecondary)
          };

          playersSubSearch.Add(playerSubSearch);
        }
      }

      return playersSubSearch;
    }
  }
}