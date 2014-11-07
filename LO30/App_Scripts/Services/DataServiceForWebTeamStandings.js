﻿'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceForWebTeamStandings",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourceForWebTeamStandings = $resource(constApisUrl + '/forWebTeamStandings');

      var listForWebTeamStandings = function () {
        return resourceForWebTeamStandings.query();
      };

      return {
        listForWebTeamStandings: listForWebTeamStandings
      };
    }
  ]
);

