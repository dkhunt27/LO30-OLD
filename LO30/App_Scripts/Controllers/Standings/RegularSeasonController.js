'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('standingsRegularSeasonController',
  [
    '$scope',
    '$timeout',
    'alertService',
    'dataServiceForWebTeamStandings',
    function ($scope, $timeout, alertService, dataServiceForWebTeamStandings) {

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
          teamStandings: []
        };

        $scope.requests = {
          teamStandingsLoaded: false
        };

        $scope.user = {
        };
      };

      $scope.getForWebTeamStandings = function () {
        var retrievedType = "TeamStandings";

        $scope.initializeScopeVariables();

        dataServiceForWebTeamStandings.listForWebTeamStandings().$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.teamStandings.push(item);
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

      $scope.setWatches = function () {
      };

      $scope.activate = function () {
        $scope.initializeScopeVariables();
        $scope.setWatches();
        $scope.getForWebTeamStandings();
        $timeout(function () {
          $scope.sortAscOnly('rank');
        }, 0);  // using timeout so it fires when done rendering
      };

      $scope.activate();
    }
  ]
);