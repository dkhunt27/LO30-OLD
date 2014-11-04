'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('standingsRegularSeasonController',
  [
    '$scope',
    '$timeout',
    'alertService',
    'dataServiceForWebTeamStandings',
    function ($scope, $timeout, alertService, dataServiceForWebTeamStandings) {

      var alertTitleDataRetrievalSuccessful = "Data Retrieval Successful";
      var alertTitleDataRetrievalUnsuccessful = "Data Retrieval Unsuccessful";
      var alertMessageTemplateRetrievalSuccessful = "Retrieved <%=retrievedType%>, Length: <%=retrievedLength%>";
      var alertMessageTemplateRetrievalUnsuccessful = "Received following error trying to retrieve <%=retrievedType%>. Error:<%=retrievedError%>";
      var alertMessage;

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

        dataServiceForWebTeamStandings.getForWebTeamStandings().$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.teamStandings.push(item);
              });

              $scope.requests.teamStandingsLoaded = true;

              alertMessage = _.template(alertMessageTemplateRetrievalSuccessful)({ retrievedType: retrievedType, retrievedLength: $scope.data.teamStandings.length });
              alertService.info(alertMessage, alertTitleDataRetrievalSuccessful);

            } else {
              // results not successful
              alertMessage = _.template(alertMessageTemplateRetrievalUnsuccessful)({ retrievedType: retrievedType, retrievedError: result.reason });
              alertService.error(alertMessage, alertTitleDataRetrievalUnsuccessful);
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