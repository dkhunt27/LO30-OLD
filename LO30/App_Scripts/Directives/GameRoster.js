'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.directive('lo30GameRoster',
  [
    function () {
      return {
        restrict: 'E',
        templateUrl: "/Templates/Directives/GameRoster.html",
        scope: {
          "gameId": "=",
          "homeTeam": "="
        },
        controller: [
          '$scope',
          'dataServiceGameRosters',
          function ($scope, dataServiceGameRosters) {
            $scope.data = {
              gameRosters: []
            };

            $scope.getGameRosters = function (gameId, homeTeam) {
              var retrievedType = "gameRoster";

              $scope.data.gameRosterLoaded = false;
              dataServiceGameRosters.getGameRostersByGameIdAndHomeTeam(gameId, homeTeam).$promise.then(
                function (result) {
                  // service call on success
                  if (result && result.length && result.length > 0) {

                    angular.forEach(result, function (item) {
                      $scope.data.gameRosters.push(item);
                    });

                    $scope.data.gameRosterLoaded = true;

                    alertMessage = _.template(alertMessageTemplateRetrievalSuccessful)({ retrievedType: retrievedType, retrievedLength: $scope.data.gameRosters.length });
                    alertService.info(alertMessage, alertTitleDataRetrievalSuccessful);
                  } else {
                    // results not successful
                    alertMessage = _.template(alertMessageTemplateRetrievalUnsuccessful)({ retrievedType: retrievedType, retrievedError: result.reason });
                    alertService.error(alertMessage, alertTitleDataRetrievalUnsuccessful);
                  }
                }
              );
            };

            $scope.activate = function () {
            };
          }
        ],
        link: function (scope, element, attrs, controller) {

          scope.activate();

          scope.$watch('gameId', function (val) {
            if (val) {
              scope.getGameRosters(scope.gameId, scope.homeTeam);
            }
          }, true);

        }
      };
    }
  ]
);

