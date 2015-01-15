'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('lo30BoxScoreController',
  [
    '$scope',
    '$timeout',
    '$routeParams',
    'alertService',
    'dataServiceGames',
    'dataServiceGameOutcomes',
    'dataServiceGameScores',
    function ($scope, $timeout, $routeParams, alertService, dataServiceGames, dataServiceGameOutcomes, dataServiceGameScores) {

      $scope.initializeScopeVariables = function () {

        $scope.data = {
          selectedGameId: -1,
          game: {},
          gameOutcomes: [],
          gameScores: [],
          gameResults: []
        };

        $scope.events = {
          gameLoaded: false,
          gameOutcomeLoaded: false,
          gameScoresLoaded: false,
          gameResultsProcessed: false
        };

      };

      $scope.getGame = function (gameId) {
        var retrievedType = "Game";
        dataServiceGames.getGameByGameId(gameId).$promise.then(
          function (result) {
            if (result) {
              $scope.data.game = result;
              $scope.events.gameLoaded = true;
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

      $scope.getGameOutcomes = function (gameId) {
        var retrievedType = "GameOutcomes";
        var fullDetail = true;
        dataServiceGameOutcomes.listGameOutcomesByGameId(gameId, fullDetail).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.gameOutcomes.push(item);
              });

              $scope.events.gameOutcomesLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.gameOutcomes.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.getGameScores = function (gameId) {
        var retrievedType = "GameScores";
        var fullDetail = false;
        dataServiceGameScores.listGameScoresByGameId(gameId, fullDetail).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.gameScores.push(item);
              });

              $scope.events.gameScoresLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.gameScores.length);

            } else {
              alertService.warningRetrieval(retrievedType, "No results returned");
            }
          },
          function (err) {
            alertService.errorRetrieval(retrievedType, err.message);
          }
        );
      };

      $scope.processGameResults = function () {
        var gameResults = [];

        var gameOutcomeHomeTeam = _.find($scope.data.gameOutcomes, function (item) { return item.gameTeam.homeTeam === true; });
        var gameScorePeriod1HomeTeam = _.find($scope.data.gameScores, function (item) { return item.gameTeam.homeTeam === true && item.period === 1; });
        var gameScorePeriod2HomeTeam = _.find($scope.data.gameScores, function (item) { return item.gameTeam.homeTeam === true && item.period === 2; });
        var gameScorePeriod3HomeTeam = _.find($scope.data.gameScores, function (item) { return item.gameTeam.homeTeam === true && item.period === 3; });
        var gameScorePeriod4HomeTeam = _.find($scope.data.gameScores, function (item) { return item.gameTeam.homeTeam === true && item.period === 4; });
        var gameOutcomeAwayTeam = _.find($scope.data.gameOutcomes, function(item) { return item.gameTeam.homeTeam === false;});
        var gameScorePeriod1AwayTeam = _.find($scope.data.gameScores, function (item) { return item.gameTeam.homeTeam === false && item.period === 1; });
        var gameScorePeriod2AwayTeam = _.find($scope.data.gameScores, function (item) { return item.gameTeam.homeTeam === false && item.period === 2; });
        var gameScorePeriod3AwayTeam = _.find($scope.data.gameScores, function (item) { return item.gameTeam.homeTeam === false && item.period === 3; });
        var gameScorePeriod4AwayTeam = _.find($scope.data.gameScores, function (item) { return item.gameTeam.homeTeam === false && item.period === 4; });

        var gameResultHomeTeam = {
          gameId: gameOutcomeHomeTeam.gameTeam.gameId,
          gameDateTime: gameOutcomeHomeTeam.gameTeam.game.gameDateTime,
          gameTeamId: gameOutcomeHomeTeam.gameTeamId,
          teamShortName: gameOutcomeHomeTeam.gameTeam.seasonTeam.team.teamShortName,
          teamLongName: gameOutcomeHomeTeam.gameTeam.seasonTeam.team.teamLongName,
          homeTeam: gameOutcomeHomeTeam.gameTeam.homeTeam,
          outcome: gameOutcomeHomeTeam.outcome,
          period1: gameScorePeriod1HomeTeam.score,
          period2: gameScorePeriod2HomeTeam.score,
          period3: gameScorePeriod3HomeTeam.score,
          period4: gameScorePeriod4HomeTeam.score,
          final: gameOutcomeHomeTeam.goalsFor
        }

        var gameResultAwayTeam = {
          gameId: gameOutcomeAwayTeam.gameTeam.gameId,
          gameDateTime: gameOutcomeAwayTeam.gameTeam.game.gameDateTime,
          gameTeamId: gameOutcomeAwayTeam.gameTeamId,
          teamShortName: gameOutcomeAwayTeam.gameTeam.seasonTeam.team.teamShortName,
          teamLongName: gameOutcomeAwayTeam.gameTeam.seasonTeam.team.teamLongName,
          homeTeam: gameOutcomeAwayTeam.gameTeam.homeTeam,
          outcome: gameOutcomeAwayTeam.outcome,
          period1: gameScorePeriod1AwayTeam.score,
          period2: gameScorePeriod2AwayTeam.score,
          period3: gameScorePeriod3AwayTeam.score,
          period4: gameScorePeriod4AwayTeam.score,
          final: gameOutcomeAwayTeam.goalsFor
        }

        gameResults.push(gameResultHomeTeam);
        gameResults.push(gameResultAwayTeam);

        $scope.data.gameResults = gameResults;
        $scope.events.gameResultsProcessed = true;
      };

      $scope.setWatches = function () {
        $scope.$watch('events.gameOutcomesLoaded', function (newVal, oldVal) {
          if (newVal && newVal !== oldVal) {
            // only process if gameScores also loaded
            if (newVal === true && $scope.events.gameScoresLoaded === true) {
              $scope.processGameResults();
            }
          }
        }, true);

        $scope.$watch('events.gameScoresLoaded', function (newVal, oldVal) {
          if (newVal && newVal !== oldVal) {
            // only process if gameOutcomes also loaded
            if (newVal === true && $scope.events.gameOutcomesLoaded === true) {
              $scope.processGameResults();
            }
          }
        }, true);
      };

      $scope.activate = function () {
        $scope.initializeScopeVariables();
        $scope.setWatches();

        //TODO make this a user selection
        if ($scope.gameId === null) {
          $scope.data.selectedGameId = 3299;
        } else {
          $scope.data.selectedGameId = $scope.gameId;
        }

        $scope.getGame($scope.data.selectedGameId);
        $scope.getGameOutcomes($scope.data.selectedGameId);
        $scope.getGameScores($scope.data.selectedGameId);
        $timeout(function () {
        }, 0);  // using timeout so it fires when done rendering
      };

      $scope.activate();
    }
  ]
);

