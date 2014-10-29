'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('scoreSheetController',
  [
    '$scope',
    'alertService',
    'dataServiceGames',
    'dataServiceGameRosters',
    function ($scope, alertService, dataServiceGames, dataServiceGameRosters) {

      var alertTitleDataRetrievalSuccessful = "Data Retrieval Successful";
      var alertTitleDataRetrievalUnsuccessful = "Data Retrieval Unsuccessful";
      var alertMessageTemplateRetrievalSuccessful = "Retrieved <%=retrievedType%>, Length: <%=retrievedLength%>";
      var alertMessageTemplateRetrievalUnsuccessful = "Received following error trying to retrieve <%=retrievedType%>. Error:<%=retrievedError%>";
      var alertMessage;


      $scope.initializeScopeVariables = function () {
        $scope.data = {
          games: [],
          gameIdSelected: -1,
          gameSelected: {},
          homeTeamName: "",
          homeTeamScore: 0,
          homeTeamPims: 0,
          awayTeamName: "",
          awayTeamScore: 0,
          awayTeamPims: 0,
          gameRosterHome: [],
          gameRosterAway: []
        };

        $scope.requests = {
          gamesLoaded: false,
          gameRosterHomeLoaded: false,
          gameRosterAwayLoaded: false
        };

        $scope.user = {
          selectedGameId: false
        };
      }


      $scope.getGameRosters = function (gameId, homeTeam) {
        var retrievedType, gameRostersLoaded, gameRosters, teamName;

        if (homeTeam) {
          gameRostersLoaded = 'gameRosterHomeLoaded';
          gameRosters = 'gameRosterHome';
          retrievedType = "Home GameRoster";
          teamName = "homeTeamName";
        } else {
          gameRostersLoaded = 'gameRosterAwayLoaded';
          gameRosters = 'gameRosterAway';
          retrievedType = "Away GameRoster";
          teamName = "awayTeamName";
        }

        $scope.requests[gameRostersLoaded] = false;
        $scope.data[gameRosters] = [];
        $scope.data[teamName] = "";

        dataServiceGameRosters.getGameRostersByGameIdAndHomeTeam(gameId, homeTeam).$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item, index) {
                $scope.data[gameRosters].push(item);

                if (index === 0) {
                  $scope.data[teamName] = item.gameTeam.seasonTeam.team.teamShortName;
                }
              });

              $scope.requests[gameRostersLoaded] = true;

              alertMessage = _.template(alertMessageTemplateRetrievalSuccessful)({ retrievedType: retrievedType, retrievedLength: $scope.data[gameRosters].length });
              alertService.info(alertMessage, alertTitleDataRetrievalSuccessful);
            } else {
              // results not successful
              alertMessage = _.template(alertMessageTemplateRetrievalUnsuccessful)({ retrievedType: retrievedType, retrievedError: result.reason });
              alertService.error(alertMessage, alertTitleDataRetrievalUnsuccessful);
            }
          }
        );
      };

      $scope.getGames = function () {
        var retrievedType = "Games";

        $scope.initializeScopeVariables();

        dataServiceGames.getGames().$promise.then(
          function (result) {
            // service call on success
            if (result && result.length && result.length > 0) {

              angular.forEach(result, function (item) {
                $scope.data.games.push(item);
              });

              $scope.requests.gamesLoaded = true;

              alertMessage = _.template(alertMessageTemplateRetrievalSuccessful)({ retrievedType: retrievedType, retrievedLength: $scope.data.games.length });
              alertService.info(alertMessage, alertTitleDataRetrievalSuccessful);


              // TODO MOVE THIS TO A USER INPUT
              $scope.user.selectedGameId = true;
              $scope.data.gameIdSelected = 3200;
              $scope.data.gameSelected = _.find($scope.data.games, function (item) { return item.gameId === $scope.data.gameIdSelected });

            } else {
              // results not successful
              alertMessage = _.template(alertMessageTemplateRetrievalUnsuccessful)({ retrievedType: retrievedType, retrievedError: result.reason });
              alertService.error(alertMessage, alertTitleDataRetrievalUnsuccessful);
            }
          }
        );
      };

      $scope.setWatches = function () {
        $scope.$watch('data.gameIdSelected', function (val) {
          if (val) {
            $scope.getGameRosters($scope.data.gameIdSelected, true);
            $scope.getGameRosters($scope.data.gameIdSelected, false);
          }
        }, true);
      }

      $scope.activate = function () {
        $scope.setWatches();
        $scope.getGames();
      };

      $scope.activate();
    }
  ]
);

