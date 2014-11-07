'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('scoreSheetsEntryController',
  [
    '$scope',
    'alertService',
    'dataServiceGames',
    'dataServiceGameTeams',
    'dataServiceGameRosters',
    'dataServiceTeamRosters',
    function ($scope, alertService, dataServiceGames, dataServiceGameTeams, dataServiceGameRosters, dataServiceTeamRosters) {

      $scope.initializeScopeVariables = function () {
        $scope.data = {
          games: [],
          gameIdSelected: -1,
          gameSelected: {},
          homeTeamName: "",
          homeTeamScore: 0,
          homeTeamPims: 0,
          awayTeamName: "",
          awayTeamScore: 0,
          awayTeamPims: 0,
          gameTeamHome: {},
          gameTeamAway: {},
          gameRosterHome: [],
          gameRosterAway: [],
          teamRosterHome: [],
          teamRosterAway: []
        };

        $scope.requests = {
          gamesLoaded: false,
          gameTeamHomeLoaded: false,
          gameTeamAwayLoaded: false,    
          gameRosterHomeLoaded: false,
          gameRosterAwayLoaded: false,
          teamRosterHomeLoaded: false,
          teamRosterAwayLoaded: false
        };

        $scope.user = {
          selectedGameId: false
        };
      }

      $scope.getGameTeam = function (gameId, homeTeam) {
        var retrievedType, gameTeamLoaded, gameTeam, teamName;

        if (homeTeam) {
          gameTeamLoaded = 'gameTeamHomeLoaded';
          gameTeam = 'gameTeamHome';
          retrievedType = "Home GameTeam";
          teamName = "homeTeamName";
        } else {
          gameTeamLoaded = 'gameTeamAwayLoaded';
          gameTeam = 'gameTeamAway';
          retrievedType = "Away GameTeam";
          teamName = "awayTeamName";
        }

        $scope.requests[gameTeamLoaded] = false;
        $scope.data[gameTeam] = [];
        $scope.data[teamName] = "";

        dataServiceGameTeams.getGameTeamByGameIdAndHomeTeam(gameId, homeTeam).$promise.then(
          function (result) {
            // service call on success
            if (result) {
              $scope.data[gameTeam] = result;
              $scope.data[teamName] = result.seasonTeam.team.teamShortName;
              $scope.requests[gameTeamLoaded] = true;

              alertService.successRetrieval(retrievedType, 1);
            } else {
              // results not successful
              alertService.errorRetrieval(retrievedType, result.reason);
            }
          }
        );
      };

      $scope.getTeamRosters = function (seasonTeamId, homeTeam) {
        var retrievedType, teamRostersLoaded, teamRosters;

        if (homeTeam) {
          teamRostersLoaded = 'teamRosterHomeLoaded';
          teamRosters = 'teamRosterHome';
          retrievedType = "Home TeamRoster";
        } else {
          teamRostersLoaded = 'teamRosterAwayLoaded';
          teamRosters = 'teamRosterAway';
          retrievedType = "Away TeamRoster";
        }

        $scope.requests[teamRostersLoaded] = false;
        $scope.data[teamRosters] = [];

        dataServiceTeamRosters.listTeamRostersBySeasonTeamIdAndYYYYMMDD(seasonTeamId, $scope.data.gameSelected.gameYYYYMMDD).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item, index) {
                $scope.data[teamRosters].push(item);
              });

              $scope.requests[teamRostersLoaded] = true;

              alertService.successRetrieval(retrievedType, $scope.data[teamRosters].length);
            } else {
              // results not successful
              alertService.errorRetrieval(retrievedType, result.reason);
            }
          }
        );

      };
      $scope.getGameRosters = function (gameId, homeTeam) {
        var retrievedType, gameRostersLoaded, gameRosters;

        if (homeTeam) {
          gameRostersLoaded = 'gameRosterHomeLoaded';
          gameRosters = 'gameRosterHome';
          retrievedType = "Home GameRoster";
        } else {
          gameRostersLoaded = 'gameRosterAwayLoaded';
          gameRosters = 'gameRosterAway';
          retrievedType = "Away GameRoster";
        }

        $scope.requests[gameRostersLoaded] = false;
        $scope.data[gameRosters] = [];

        dataServiceGameRosters.listGameRostersByGameIdAndHomeTeam(gameId, homeTeam).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item, index) {
                $scope.data[gameRosters].push(item);
              });

              $scope.requests[gameRostersLoaded] = true;

              alertService.successRetrieval(retrievedType, $scope.data[gameRosters].length);
            } else {
              // results not successful
              alertService.errorRetrieval(retrievedType, result.reason);
            }
          }
        );
      };

      $scope.getGames = function () {
        var retrievedType = "Games";

        $scope.initializeScopeVariables();

        dataServiceGames.listGames().$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.games.push(item);
              });

              $scope.requests.gamesLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.games.length);

              // TODO MOVE THIS TO A USER INPUT
              $scope.user.selectedGameId = true;
              $scope.data.gameIdSelected = 3200;
              $scope.data.gameSelected = _.find($scope.data.games, function (item) { return item.gameId === $scope.data.gameIdSelected });

            } else {
              // results not successful
              alertService.errorRetrieval(retrievedType, result.reason);
            }
          }
        );
      };

      $scope.setWatches = function () {
        $scope.$watch('data.gameIdSelected', function (val) {
          if (val > -1) {
            $scope.getGameTeam($scope.data.gameIdSelected, true);
            $scope.getGameTeam($scope.data.gameIdSelected, false);
            $scope.getGameRosters($scope.data.gameIdSelected, true);
            $scope.getGameRosters($scope.data.gameIdSelected, false);
          }
        }, true);

        $scope.$watch('requests.gameTeamHomeLoaded', function (val) {
          if (val === true) {
            $scope.getTeamRosters($scope.data.gameTeamHome.seasonTeamId, true);
          }
        }, true);

        $scope.$watch('requests.gameTeamAwayLoaded', function (val) {
          if (val === true) {
            $scope.getTeamRosters($scope.data.gameTeamAway.seasonTeamId, false);
          }
        }, true);
      }

      $scope.activate = function () {
        $scope.setWatches();
        $scope.getGames();
      };

      $scope.activate();
    }
  ]
);

