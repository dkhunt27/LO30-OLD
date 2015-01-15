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

      $scope.initializeScopeVariables = function () {

        $scope.data = {
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

      $scope.getGameOutcomes = function (seasonTeamId) {
        var retrievedType = "GameOutcomes";

        var fullDetail = true;
        dataServiceGameOutcomes.listGameOutcomesBySeasonTeamId(seasonTeamId, fullDetail).$promise.then(
          function (result) {
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
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

      $scope.getForWebTeamStandings = function (seasonTeamId) {
        var retrievedType = "ForWebTeamStandings";

        dataServiceForWebTeamStandings.listForWebTeamStandings().$promise.then(
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

      $scope.getForWebTeamStandingsGoodThru = function () {
        var retrievedType = "ForWebTeamStandingsGoodThru";

        dataServiceForWebTeamStandings.getForWebTeamStandingsDataGoodThru().then(
          function (result) {
            // service call on success
            if (result && result.data) {

              $scope.data.teamStandingsDataGoodThru = result.data.replace(/\"/g, "");  // TODO figure out why its has double "s
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
        if ($routeParams.gameId === null) {
          $scope.data.selectedSeasonTeamId = 308;
        } else {
          $scope.data.selectedSeasonTeamId = $routeParams.seasonTeamId;
        }

        $scope.getGameOutcomes($scope.data.selectedSeasonTeamId);
        $scope.getForWebTeamStandings($scope.data.selectedSeasonTeamId);
        $scope.getForWebTeamStandingsGoodThru();
        $timeout(function () {
        }, 0);  // using timeout so it fires when done rendering
      };

      $scope.activate();
    }
  ]
);