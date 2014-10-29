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
            $scope.activate = function () {
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

