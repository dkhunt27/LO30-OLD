'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('lo30PlayerStatsCareerController',
  [
    '$scope',
    'alertService',
    'externalLibService',
    'dataServicePlayerStatsCareer',
    'dataServicePlayerStatsSeason',
    function ($scope, alertService, externalLibService, dataServicePlayerStatsCareer, dataServicePlayerStatsSeason) {
      var _ = externalLibService._;

      $scope.sortAscFirst = function (column) {
        if ($scope.sortOn === column) {
          $scope.sortDirection = !$scope.sortDirection;
        } else {
          $scope.sortOn = column;
          $scope.sortDirection = false;
        }
      };

      $scope.sortDescFirst = function (column) {
        if ($scope.sortOn === column) {
          $scope.sortDirection = !$scope.sortDirection;
        } else {
          $scope.sortOn = column;
          $scope.sortDirection = true;
        }
      };

      $scope.sortAscOnly = function (column) {
        $scope.sortOn = column;
        $scope.sortDirection = false;
      };

      $scope.sortDescOnly = function (column) {
        $scope.sortOn = column;
        $scope.sortDirection = true;
      };

      $scope.initializeScopeVariables = function () {

        $scope.data = {
          playerStatsCareer: {}
        };

        $scope.events = {
          playerStatsCareerProcessing: false,
          playerStatsCareerProcessed: false,
        };
      };

      $scope.getPlayerStatsCareer = function (playerId, playoffs) {
        var retrievedType = "PlayerStatsCareer";

        $scope.events.playerStatsCareerProcessing = true;
        $scope.events.playerStatsCareerProcessed = false;
        $scope.data.playerStatsCareer = {};

        dataServicePlayerStatsCareer.getByPlayerIdPlayoffs(playerId, playoffs).$promise.then(
          function (result) {
            // service call on success
            if (result) {

              $scope.data.playerStatsCareer = result;

              $scope.events.playerStatsCareerProcessing = false;
              $scope.events.playerStatsCareerProcessed = true;

              alertService.successRetrieval(retrievedType, 1);
            } else {
              // results not successful
              alertService.errorRetrieval(retrievedType, result.reason);
            }
          }
        );
      };

      $scope.getPlayerStatsCareerViaSeasons = function (playerId, playoffs) {
        var retrievedType = "PlayerStatsCareerViaSeasons";

        $scope.events.playerStatsCareerProcessing = true;
        $scope.events.playerStatsCareerProcessed = false;
        $scope.data.playerStatsCareer = {};

        dataServicePlayerStatsSeason.listByPlayerId(playerId).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              var career = {
                playerId: playerId,
                playoffs: playoffs,
                games: 0,
                goals: 0,
                assists: 0,
                points: 0,
                penaltyMinutes: 0,
                powerPlayGoals: 0,
                shortHandedGoals: 0,
                GameWinningGoals: 0
              };

              angular.forEach(result, function (item, index) {
                if (item.playoffs == playoffs) {
                  career.games = career.games + item.games;
                  career.goals = career.goals + item.goals;
                  career.assists = career.assists + item.assists;
                  career.points = career.points + item.points;
                  career.penaltyMinutes = career.penaltyMinutes + item.penaltyMinutes;
                  career.powerPlayGoals = career.powerPlayGoals + item.powerPlayGoals;
                  career.shortHandedGoals = career.shortHandedGoals + item.shortHandedGoals;
                  career.GameWinningGoals = career.GameWinningGoals + item.GameWinningGoals;
                }
              });

              $scope.data.playerStatsCareer = career;

              $scope.events.playerStatsCareerProcessing = false;
              $scope.events.playerStatsCareerProcessed = true;

              alertService.successRetrieval(retrievedType, 1);
            } else {
              // results not successful
              alertService.errorRetrieval(retrievedType, result.reason);
            }
          }
        );
      };

      $scope.setWatches = function () {
      };

      $scope.activate = function () {
        $scope.initializeScopeVariables();
        $scope.setWatches();
        //$scope.getPlayerStatsCareer($scope.playerId, $scope.playoffs);
        $scope.getPlayerStatsCareerViaSeasons($scope.playerId, $scope.playoffs);
      };

      $scope.activate();
    }
  ]
);

