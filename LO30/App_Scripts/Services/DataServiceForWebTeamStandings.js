'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceForWebTeamStandings",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourceForWebTeamStandings = $resource(constApisUrl + '/forwebteamstandings');

      var getForWebTeamStandings = function () {
        return resourceForWebTeamStandings.query();
      };


      return {
        getForWebTeamStandings: getForWebTeamStandings
      };
    }
  ]
);

