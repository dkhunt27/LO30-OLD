'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.directive('lo30GameRoster',
  [
    function () {
      return {
        restrict: 'E',
        templateUrl: "/Templates/Directives/GameRoster.html",
        scope: {
          "teamRoster": "=",
          "gameRoster": "=",
          "homeTeam": "=",
          "locked": "="
        },
        controller: "lo30GameRosterController",
        link: function (scope, element, attrs, controller) {
        }
      };
    }
  ]
);

