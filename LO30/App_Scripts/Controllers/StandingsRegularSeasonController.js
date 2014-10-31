'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('standingsRegularSeasonController',
  [
    '$scope',
    '$timeout',
    function ($scope, $timeout) {
      $scope.standings = [
        {
          seasonTeamId: 312,
          playoff: false,
          rank: 1,
          games: 11,
          wins: 7,
          losses: 3,
          ties: 1,
          points: 15,
          goalsFor: 41,
          goalsAgainst: 24,
          penaltyMinutes: 18,
          seasonTeam: {
            seasonTeamId: 312,
            seasonId: 54,
            teamId: 246,
            season: {
              seasonId: 54,
              seasonName: "2014 - 2015",
              isCurrentSeason: true,
              startDate: null,
              endDate: null
            },
            team: {
              teamId: 246,
              teamShortName: "Hunt's Ace",
              teamLongName: "Hunt's Ace Hardware",
              coachId: null,
              sponsorId: null,
              coach: null,
              sponsor: null
            }
          }
        },
        {
          seasonTeamId: 315,
          playoff: false,
          rank: 2,
          games: 10,
          wins: 7,
          losses: 2,
          ties: 1,
          points: 15,
          goalsFor: 34,
          goalsAgainst: 17,
          penaltyMinutes: 22,
          seasonTeam: {
            seasonTeamId: 315,
            seasonId: 54,
            teamId: 324,
            season: {
              seasonId: 54,
              seasonName: "2014 - 2015",
              isCurrentSeason: true,
              startDate: null,
              endDate: null
            },
            team: {
              teamId: 324,
              teamShortName: "LAB/PSI",
              teamLongName: "Liv. Auto Body/Phillips Service Ind",
              coachId: null,
              sponsorId: null,
              coach: null,
              sponsor: null
            }
          }
        },
        {
          seasonTeamId: 308,
          playoff: false,
          rank: 3,
          games: 10,
          wins: 6,
          losses: 3,
          ties: 1,
          points: 13,
          goalsFor: 42,
          goalsAgainst: 29,
          penaltyMinutes: 19,
          seasonTeam: {
            seasonTeamId: 308,
            seasonId: 54,
            teamId: 317,
            season: {
              seasonId: 54,
              seasonName: "2014 - 2015",
              isCurrentSeason: true,
              startDate: null,
              endDate: null
            },
            team: {
              teamId: 317,
              teamShortName: "Bill Brown",
              teamLongName: "Bill Brown Auto Clinic",
              coachId: null,
              sponsorId: null,
              coach: null,
              sponsor: null
            }
          }
        },
        {
          seasonTeamId: 310,
          playoff: false,
          rank: 4,
          games: 11,
          wins: 6,
          losses: 4,
          ties: 1,
          points: 13,
          goalsFor: 39,
          goalsAgainst: 33,
          penaltyMinutes: 33,
          seasonTeam: {
            seasonTeamId: 310,
            seasonId: 54,
            teamId: 254,
            season: {
              seasonId: 54,
              seasonName: "2014 - 2015",
              isCurrentSeason: true,
              startDate: null,
              endDate: null
            },
            team: {
              teamId: 254,
              teamShortName: "Zas Ent",
              teamLongName: "Zaschak Enterprises",
              coachId: null,
              sponsorId: null,
              coach: null,
              sponsor: null
            }
          }
        },
        {
          seasonTeamId: 313,
          playoff: false,
          rank: 5,
          games: 11,
          wins: 3,
          losses: 6,
          ties: 2,
          points: 8,
          goalsFor: 29,
          goalsAgainst: 32,
          penaltyMinutes: 24,
          seasonTeam: {
            seasonTeamId: 313,
            seasonId: 54,
            teamId: 18,
            season: {
              seasonId: 54,
              seasonName: "2014 - 2015",
              isCurrentSeason: true,
              startDate: null,
              endDate: null
            },
            team: {
              teamId: 18,
              teamShortName: "D&G",
              teamLongName: "D&G Heating & Cooling",
              coachId: null,
              sponsorId: null,
              coach: null,
              sponsor: null
            }
          }
        },
        {
          seasonTeamId: 309,
          playoff: false,
          rank: 6,
          games: 11,
          wins: 3,
          losses: 6,
          ties: 2,
          points: 8,
          goalsFor: 30,
          goalsAgainst: 50,
          penaltyMinutes: 18,
          seasonTeam: {
            seasonTeamId: 309,
            seasonId: 54,
            teamId: 292,
            season: {
              seasonId: 54,
              seasonName: "2014 - 2015",
              isCurrentSeason: true,
              startDate: null,
              endDate: null
            },
            team: {
              teamId: 292,
              teamShortName: "DPKZ",
              teamLongName: "DeBrincat Padgett Kobliska Zick",
              coachId: null,
              sponsorId: null,
              coach: null,
              sponsor: null
            }
          }
        },
        {
          seasonTeamId: 311,
          playoff: false,
          rank: 7,
          games: 9,
          wins: 2,
          losses: 3,
          ties: 4,
          points: 8,
          goalsFor: 21,
          goalsAgainst: 22,
          penaltyMinutes: 23,
          seasonTeam: {
            seasonTeamId: 311,
            seasonId: 54,
            teamId: 316,
            season: {
              seasonId: 54,
              seasonName: "2014 - 2015",
              isCurrentSeason: true,
              startDate: null,
              endDate: null
            },
            team: {
              teamId: 316,
              teamShortName: "Glover",
              teamLongName: "Jeff Glover Realtors",
              coachId: null,
              sponsorId: null,
              coach: null,
              sponsor: null
            }
          }
        },
        {
          seasonTeamId: 314,
          playoff: false,
          rank: 8,
          games: 11,
          wins: 2,
          losses: 9,
          ties: 0,
          points: 4,
          goalsFor: 31,
          goalsAgainst: 60,
          penaltyMinutes: 32,
          seasonTeam: {
            seasonTeamId: 314,
            seasonId: 54,
            teamId: 323,
            season: {
              seasonId: 54,
              seasonName: "2014 - 2015",
              isCurrentSeason: true,
              startDate: null,
              endDate: null
            },
            team: {
              teamId: 323,
              teamShortName: "Villanova",
              teamLongName: "Villanova Construction",
              coachId: null,
              sponsorId: null,
              coach: null,
              sponsor: null
            }
          }
        }
      ];

      $scope.calcWinPct = function () {
        angular.forEach($scope.standings, function (item) {
          item.regSeasonWinPercent = item.wins / item.games;
        })
      }

      $scope.calcWinPct();

      $scope.sortAscOnly = function (column) {
        $scope.sortOn = column;
        $scope.sortDirection = false;
      };

      $scope.sortDescOnly = function (column) {
        $scope.sortOn = column;
        $scope.sortDirection = true;
      };

      $scope.setWatches = function () {
      };

      $scope.activate = function () {
        $scope.setWatches();
        $timeout(function () {
          $scope.sortDescOnly('points');
        }, 0);  // using timeout so it fires when done rendering
      };

      $scope.activate();
    }
  ]
);