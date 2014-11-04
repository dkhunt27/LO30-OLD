'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.directive('lo30GameRoster',
  [
    function () {
      return {
        restrict: 'E',
        templateUrl: "/Templates/Directives/GameRoster.html",
        scope: {
          "gameRoster": "=",
          "homeTeam": "="
        },
        controller: [
          '$scope',
          function ($scope) {
            $scope.getGoalies = function () {
              $scope.data.goalies = _.filter($scope.gameRoster, function (item) { return item.goalie === true; });
              return;
            };

            $scope.getLine1 = function () {
              $scope.data.line1 = _.filter($scope.gameRoster, function (item) { return item.goalie === false && item.line === 1; });
              return;
            };

            $scope.getLine2 = function () {
              $scope.data.line2 = _.filter($scope.gameRoster, function (item) { return item.goalie === true && item.line === 2; });
              return;
            };

            $scope.getLine3 = function () {
              $scope.data.line3 = _.filter($scope.gameRoster, function (item) { return item.goalie === true && item.line === 3; });
              return;
            };

            $scope.activate = function () {
              $scope.getGoalies();
              $scope.getLine1();
              $scope.getLine2();
              $scope.getLine3();
            };
          }
        ],
        link: function (scope, element, attrs, controller) {

          scope.activate();
        }
      };
    }
  ]
);

