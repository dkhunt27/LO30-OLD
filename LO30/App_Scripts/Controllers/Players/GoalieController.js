'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('playersGoalieController',
  [
    '$scope',
    '$timeout',
    '$routeParams',
    'alertService',
    'dataServicePlayers',
    'dataServiceGoalieStatsGame',
    'dataServiceGoalieStatsCareer',
    'dataServiceGoalieStatsSeason',
    'dataServiceGoalieStatsSeasonTeam',
    function ($scope, $timeout, $routeParams, alertService, dataServicePlayers, dataServiceGoalieStatsGame, dataServiceGoalieStatsCareer, dataServiceGoalieStatsSeason, dataServiceGoalieStatsSeasonTeam) {

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
          goalieStatsCareer: [],
          goalieStatsSeason: [],
          goalieStatsSeasonTeam: [],
          goalieStatsGame: []
          
        };

        $scope.requests = {
          playerLoaded: false,
          goalieStatsCareerLoaded: false,
          goalieStatsSeasonLoaded: false,
          goalieStatsSeasonTeamLoaded: false,
          goalieStatsGameLoaded: false
        };

        $scope.user = {
        };
      };

      $scope.getGoalieStatsCareer = function (playerId) {
        var retrievedType = "GoalieStatsCareer";

        dataServiceGoalieStatsCareer.listByPlayerId(playerId).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.goalieStatsCareer.push(item);
              });

              $scope.requests.goalieStatsCareerLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.goalieStatsCareer.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getGoalieStatsSeason = function (playerId) {
        var retrievedType = "GoalieStatsSeason";

        dataServiceGoalieStatsSeason.listByPlayerId(playerId).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.goalieStatsSeason.push(item);
              });

              $scope.requests.goalieStatsSeasonLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.goalieStatsSeason.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getGoalieStatsSeasonTeam = function (playerId) {
        var retrievedType = "GoalieStatsSeasonTeam";

        dataServiceGoalieStatsSeasonTeam.listByPlayerId(playerId).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.goalieStatsSeasonTeam.push(item);
              });

              $scope.requests.goalieStatsSeasonTeamLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.goalieStatsSeasonTeam.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getGoalieStatsGame = function (playerId) {
        var retrievedType = "GoalieStatsGame";

        dataServiceGoalieStatsGame.listByPlayerId(playerId).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.goalieStatsGame.push(item);
              });

              $scope.requests.goalieStatsGameLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.goalieStatsGame.length);

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
        if ($routeParams.playerId === null) {
          //$scope.data.selectedPlayerId = 593;
          $scope.data.selectedPlayerId = 631;
        } else {
          $scope.data.selectedPlayerId = $routeParams.playerId;
        }

        $scope.getPlayer($scope.data.selectedPlayerId);
        //$scope.getGoalieStatsCareer($scope.data.selectedPlayerId);
        $scope.getGoalieStatsSeason($scope.data.selectedPlayerId);
        $scope.getGoalieStatsSeasonTeam($scope.data.selectedPlayerId);
        $scope.getGoalieStatsGame($scope.data.selectedPlayerId);
        $timeout(function () {
          $scope.sortAscOnly('game.gameYYYYMMDD');
        }, 0);  // using timeout so it fires when done rendering
      };

      $scope.activate();
    }
  ]
);