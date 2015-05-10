'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('lo30PlayerStatsSeasonTeamController',
  [
    '$scope',
    'alertService',
    'externalLibService',
    'dataServicePlayerStatsSeasonTeam',
    function ($scope, alertService, externalLibService, dataServicePlayerStatsSeasonTeam) {
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
          playerStatsSeasonTeam: []
        };

        $scope.events = {
          playerStatsSeasonTeamProcessing: false,
          playerStatsSeasonTeamProcessed: false,
        };

        $scope.user = {
        };
      };

      $scope.getPlayerStatsSeasonTeam = function (playerId, playoffs) {
        var retrievedType = "PlayerStatsSeasonTeam";

        $scope.events.playerStatsSeasonTeamProcessing = true;
        $scope.events.playerStatsSeasonTeamProcessed = false;
        $scope.data.playerStatsSeasonTeam = [];

        dataServicePlayerStatsSeasonTeam.listByPlayerId(playerId).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item, index) {
                if (item.playoffs === playoffs) {
                  $scope.data.playerStatsSeasonTeam.push(item);
                }
              });

              $scope.events.playerStatsSeasonTeamProcessing = false;
              $scope.events.playerStatsSeasonTeamProcessed = true;

              alertService.successRetrieval(retrievedType, $scope.data.playerStatsSeasonTeam.length);
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
        $scope.getPlayerStatsSeasonTeam($scope.playerId, $scope.playoffs);
      };

      $scope.activate();
    }
  ]
);

