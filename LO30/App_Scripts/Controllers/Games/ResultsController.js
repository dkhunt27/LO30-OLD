'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('gamesResultsController',
  [
    '$scope',
    '$timeout',
    '$routeParams',
    'alertService',
    'dataServiceGameOutcomes',
    'dataServiceForWebTeamStandings',
    function ($scope, $timeout, $routeParams, alertService, dataServiceGameOutcomes, dataServiceForWebTeamStandings) {

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
          selectedSeasonId: -1,
          selectedPlayoffs: false,
          selectedSeasonTeamId: -1,
          game: {},
          gameOutcomes: [],
          teamStandings: [],
          teamStandingsDataGoodThru: "n/a"
        };

        $scope.requests = {
          gameLoaded: false,
          gameOutcomeLoaded: false
        };

        $scope.user = {
        };
      };

      $scope.getGameOutcomes = function (seasonId, playoffs, seasonTeamId) {
        var retrievedType = "GameOutcomes";

        var fullDetail = true;
        dataServiceGameOutcomes.listGameOutcomesBySeasonTeamId(seasonId, playoffs, seasonTeamId, fullDetail).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                item.gameTeam.game.gameDate = moment(item.gameTeam.game.gameYYYYMMDD, "YYYYMMDD");
                $scope.data.gameOutcomes.push(item);
              });

              $scope.requests.gameOutcomesLoaded = true;

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

      $scope.getForWebTeamStandings = function (seasonId, playoffs, seasonTeamId) {
        var retrievedType = "ForWebTeamStandings";

        dataServiceForWebTeamStandings.listForWebTeamStandings(seasonId, playoffs).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                if (item.stid.toString() === seasonTeamId) {
                  $scope.data.teamStandings.push(item);
                }
              });

              $scope.requests.teamStandingsLoaded = true;

              alertService.successRetrieval(retrievedType, $scope.data.teamStandings.length);

            } else {
              // results not successful
              alertService.errorRetrieval(retrievedType, result.reason);
            }
          }
        );
      };

      $scope.getForWebTeamStandingsGoodThru = function (seasonId) {
        var retrievedType = "ForWebTeamStandingsGoodThru";

        dataServiceForWebTeamStandings.getForWebTeamStandingsDataGoodThru(seasonId).$promise.then(
          function (result) {
            // service call on success
            if (result && result.gt) {

              $scope.data.teamStandingsDataGoodThru = result.gt;
              $scope.requests.teamStandingsDataGoodThruLoaded = true;

              alertService.successRetrieval("TeamStandingsGoodThru", 1);

            } else {
              // results not successful
              alertService.errorRetrieval("TeamStandingsGoodThru", result.reason);
            }
          }
        );
      };

      $scope.setWatches = function () {
      };

      $scope.activate = function () {
        $scope.initializeScopeVariables();
        $scope.setWatches();

        //TODO make this a user selection
        if ($routeParams.seasonId === null) {
          $scope.data.selectedSeasonId = 54;
          $scope.data.selectedPlayoffs = false;
          $scope.data.selectedSeasonTeamId = 308;
        } else {
          $scope.data.selectedSeasonId = $routeParams.seasonId;
          $scope.data.selectedPlayoffs = $routeParams.playoffs;
          $scope.data.selectedSeasonTeamId = $routeParams.seasonTeamId;
        }

        $scope.getGameOutcomes($scope.data.selectedSeasonId, $scope.data.selectedPlayoffs, $scope.data.selectedSeasonTeamId);
        $scope.getForWebTeamStandings($scope.data.selectedSeasonId, $scope.data.selectedPlayoffs, $scope.data.selectedSeasonTeamId);
        $scope.getForWebTeamStandingsGoodThru($scope.data.selectedSeasonId);
        $timeout(function () {
        }, 0);  // using timeout so it fires when done rendering
      };

      $scope.activate();
    }
  ]
);