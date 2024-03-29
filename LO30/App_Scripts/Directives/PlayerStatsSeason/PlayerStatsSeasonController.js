﻿'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('lo30PlayerStatsSeasonController',
  [
    '$scope',
    'alertService',
    'externalLibService',
    'dataServicePlayerStatsSeason',
    function ($scope, alertService, externalLibService, dataServicePlayerStatsSeason) {
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
          playerStatsSeason: []
        };

        $scope.events = {
          playerStatsSeasonProcessing: false,
          playerStatsSeasonProcessed: false,
        };

        $scope.user = {
        };
      };

      $scope.getPlayerStatsSeason = function (playerId, seasonId) {
        var retrievedType = "PlayerStatsSeason";

        $scope.events.playerStatsSeasonProcessing = true;
        $scope.events.playerStatsSeasonProcessed = false;
        $scope.data.playerStatsSeason = [];

        dataServicePlayerStatsSeason.listByPlayerIdSeasonId(playerId, seasonId).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item, index) {
                var name;
                if (item.playoffs === true) {
                  name = "Playoffs"
                } else {
                  name = "Regular Season"
                }

                if (item.sub === true) {
                  name = name + "*";
                } 

                item.name = name;

                $scope.data.playerStatsSeason.push(item);
              });

              $scope.events.playerStatsSeasonProcessing = false;
              $scope.events.playerStatsSeasonProcessed = true;

              alertService.successRetrieval(retrievedType, $scope.data.playerStatsSeason.length);
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
        $scope.getPlayerStatsSeason($scope.playerId, $scope.seasonId);
      };

      $scope.activate();
    }
  ]
);

