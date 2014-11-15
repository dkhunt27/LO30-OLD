using LO30.Data.Objects;
using LO30.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LO30.Data
{
  public partial class Lo30RepositoryMock
  {
    public List<PlayerSubSearch> GetPlayersSubSearch(string position, string ratingMin, string ratingMax)
    {
      int ratingMinPrimary = -1;
      int ratingMinSecondary = -1;
      int ratingMaxPrimary = -1;
      int ratingMaxSecondary = -1;
      _lo30DataService.ConvertCombinedRatingToPrimarySecondary(ratingMin, out ratingMinPrimary, out ratingMinSecondary);
      _lo30DataService.ConvertCombinedRatingToPrimarySecondary(ratingMax, out ratingMaxPrimary, out ratingMaxSecondary);

      var players = _players.ToList();

      var todayYYYYMMDD = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
      var currentSeasonId = _seasons.Where(x=>x.IsCurrentSeason == true).FirstOrDefault().SeasonId;

      var playersSubSearch = new List<PlayerSubSearch>();

      foreach (var player in players)
      {
        var playerRating = _playerRatings.Where(x => x.SeasonId == currentSeasonId &&
                                          x.PlayerId == player.PlayerId &&
                                          (x.Position == position || x.Position == "X") &&
                                          x.StartYYYYMMDD <= todayYYYYMMDD &&
                                          x.EndYYYYMMDD >= todayYYYYMMDD)
                                    .FirstOrDefault();

        var teamRoster = _teamRosters.Where(x => x.SeasonTeam.SeasonId == currentSeasonId &&
                                  x.PlayerId == player.PlayerId &&
                                  x.Position == position &&
                                  x.StartYYYYMMDD <= todayYYYYMMDD &&
                                  x.EndYYYYMMDD >= todayYYYYMMDD)
                            .FirstOrDefault();

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