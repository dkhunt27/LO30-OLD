'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.directive('lo30GoalieStatsSeasonTeam',
  [
    function () {
      return {
        restrict: 'E',
        templateUrl: "/Templates/Directives/GoalieStatsSeasonTeam.html",
        scope: {
          "playerId": "=",
          "playoffs": "="
        },
        controller: "lo30GoalieStatsSeasonTeamController",
        link: function (scope, element, attrs, controller) {
        }
      };
    }
  ]
);

