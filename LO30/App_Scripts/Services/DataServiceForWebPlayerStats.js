'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceForWebPlayerStats",
  [
    "constApisUrl",
    "$resource",
    "$http",
    function (constApisUrl, $resource, $http) {

      var resourceForWebPlayerStats = $resource(constApisUrl + '/forwebplayerstats');
      var forWebPlayerStatsDataGoodThruUrl = constApisUrl + '/forwebplayerstatsdatagoodthru';

      var listForWebPlayerStats = function () {
        return resourceForWebPlayerStats.query();
      };

      var getForWebPlayerStatsDataGoodThru = function () {
        return $http.get(forWebPlayerStatsDataGoodThruUrl);
      };

      return {
        listForWebPlayerStats: listForWebPlayerStats,
        getForWebPlayerStatsDataGoodThru: getForWebPlayerStatsDataGoodThru
      };
    }
  ]
);

