'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.directive('lo30GameRosterPlayer',
  [
    function () {
      return {
        restrict: 'E',
        templateUrl: "/Templates/Directives/GameRosterPlayer.html",
        scope: {
          "teamGameRoster": "="
        },
        controller: "lo30GameRosterPlayerController",
        link: function (scope, element, attrs, controller) {

          scope.activate();
        }
      };
    }
  ]
);

