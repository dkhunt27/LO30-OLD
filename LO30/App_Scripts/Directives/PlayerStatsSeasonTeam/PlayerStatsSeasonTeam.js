'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.directive('lo30PlayerStatsSeasonTeam',
  [
    function () {
      return {
        restrict: 'E',
        templateUrl: "/Templates/Directives/PlayerStatsSeasonTeam.html",
        scope: {
          "playerId": "=",
          "playoffs": "="
        },
        controller: "lo30PlayerStatsSeasonTeamController",
        link: function (scope, element, attrs, controller) {
        }
      };
    }
  ]
);

