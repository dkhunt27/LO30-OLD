'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceForWebTeamStandings",
  [
    "constApisUrl",
    "$resource",
    "$http",
    function (constApisUrl, $resource, $http) {

      var resourceForWebTeamStandings = $resource(constApisUrl + '/forwebteamstandings');
      var foWebTeamStandingsDataGoodThruUrl = constApisUrl + '/forwebteamstandingsdatagoodthru';

      var listForWebTeamStandings = function () {
        return resourceForWebTeamStandings.query();
      };

      var getForWebTeamStandingsDataGoodThru = function () {
        return $http.get(foWebTeamStandingsDataGoodThruUrl);
      };

      return {
        listForWebTeamStandings: listForWebTeamStandings,
        getForWebTeamStandingsDataGoodThru: getForWebTeamStandingsDataGoodThru
      };
    }
  ]
);

