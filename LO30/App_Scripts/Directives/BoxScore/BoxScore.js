'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.directive('lo30BoxScore',
  [
    function () {
      return {
        restrict: 'E',
        templateUrl: "/Templates/Directives/BoxScore.html",
        scope: {
          "gameId": "="
        },
        controller: "lo30BoxScoreController",
        link: function (scope, element, attrs, controller) {

          scope.activate();
        }
      };
    }
  ]
);

