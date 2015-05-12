'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('lo30GoalieStatsSeasonTeamController',
  [
    '$scope',
    'alertService',
    'externalLibService',
    'dataServiceGoalieStatsSeasonTeam',
    function ($scope, alertService, externalLibService, dataServiceGoalieStatsSeasonTeam) {
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
          goalieStatsSeasonTeam: []
        };

        $scope.events = {
          goalieStatsSeasonTeamProcessing: false,
          goalieStatsSeasonTeamProcessed: false,
        };

        $scope.user = {
        };
      };

      $scope.getGoalieStatsSeasonTeam = function (playerId, playoffs) {
        var retrievedType = "GoalieStatsSeasonTeam";

        $scope.events.goalieStatsSeasonTeamProcessing = true;
        $scope.events.goalieStatsSeasonTeamProcessed = false;
        $scope.data.goalieStatsSeasonTeam = [];

        dataServiceGoalieStatsSeasonTeam.listByPlayerId(playerId).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item, index) {
                if (item.playoffs === playoffs) {
                  $scope.data.goalieStatsSeasonTeam.push(item);
                }
              });

              $scope.events.goalieStatsSeasonTeamProcessing = false;
              $scope.events.goalieStatsSeasonTeamProcessed = true;

              alertService.successRetrieval(retrievedType, $scope.data.goalieStatsSeasonTeam.length);
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
        $scope.getGoalieStatsSeasonTeam($scope.playerId, $scope.playoffs);
      };

      $scope.activate();
    }
  ]
);

