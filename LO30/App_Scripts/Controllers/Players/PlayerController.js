'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('playersPlayerController',
  [
    '$scope',
    '$timeout',
    'alertService',
    'dataServicePlayers',
    'dataServicePlayerStatsGame',
    'dataServicePlayerStatsCareer',
    'dataServicePlayerStatsSeason',
    'dataServicePlayerStatsSeasonTeam',
    function ($scope, $timeout, alertService, dataServicePlayers, dataServicePlayerStatsGame, dataServicePlayerStatsCareer, dataServicePlayerStatsSeason, dataServicePlayerStatsSeasonTeam) {

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
          selectedPlayerId: -1,
          selectedSeasonId: -1,
          player: {},
          playerStatsCareer: [],
          playerStatsSeason: [],
          playerStatsSeasonTeam: [],
          playerStatsGame: []
          
        };

        $scope.requests = {
          playerLoaded: false,
          playerStatsCareerLoaded: false,
          playerStatsSeasonLoaded: false,
          playerStatsSeasonTeamLoaded: false,
          playerStatsGameLoaded: false
        };

        $scope.user = {
        };
      };

      $scope.getPlayerStatsCareer = function (playerId) {
        var retrievedType = "PlayerStatsCareer";

        dataServicePlayerStatsCareer.listByPlayerId(playerId).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.playerStatsCareer.push(item);
              });

              $scope.requests.playerStatsCareerLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.playerStatsCareer.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getPlayerStatsSeason = function (playerId) {
        var retrievedType = "PlayerStatsSeason";

        dataServicePlayerStatsSeason.listByPlayerId(playerId).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.playerStatsSeason.push(item);
              });

              $scope.requests.playerStatsSeasonLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.playerStatsSeason.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getPlayerStatsSeasonTeam = function (playerId) {
        var retrievedType = "PlayerStatsSeasonTeam";

        dataServicePlayerStatsSeasonTeam.listByPlayerId(playerId).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.playerStatsSeasonTeam.push(item);
              });

              $scope.requests.playerStatsSeasonTeamLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.playerStatsSeasonTeam.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getPlayerStatsGame = function (playerId) {
        var retrievedType = "PlayerStatsGame";

        dataServicePlayerStatsGame.listByPlayerId(playerId).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.playerStatsGame.push(item);
              });

              $scope.requests.playerStatsGameLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.playerStatsGame.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getPlayer = function (playerId) {
        var retrievedType = "Player";

        dataServicePlayers.getByPlayerId(playerId).$promise.then(
          function (result) {
            if (result) {

              $scope.data.player = result;
              $scope.requests.playerLoaded = true;
              alertService.successRetrieval(retrievedType, 1);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getAnotherPlayer = function () {

        $scope.initializeScopeVariables();

        //TODO make this a user selection
        $scope.data.selectedPlayerId = 593;
        $scope.getPlayer($scope.data.selectedPlayerId);
      };

      $scope.setWatches = function () {
      };

      $scope.activate = function () {
        $scope.initializeScopeVariables();
        $scope.setWatches();

        //TODO make this a user selection
        //$scope.data.selectedPlayerId = 593;
        $scope.data.selectedPlayerId = 631;

        $scope.getPlayer($scope.data.selectedPlayerId);
        $scope.getPlayerStatsCareer($scope.data.selectedPlayerId);
        $scope.getPlayerStatsSeason($scope.data.selectedPlayerId);
        $scope.getPlayerStatsSeasonTeam($scope.data.selectedPlayerId);
        $scope.getPlayerStatsGame($scope.data.selectedPlayerId);
        $timeout(function () {
          //$scope.sortDescOnly('p');
        }, 0);  // using timeout so it fires when done rendering
      };

      $scope.activate();
    }
  ]
);