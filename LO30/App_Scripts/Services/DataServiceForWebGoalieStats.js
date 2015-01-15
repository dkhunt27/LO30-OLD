'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceForWebGoalieStats",
  [
    "constApisUrl",
    "$resource",
    "$http",
    function (constApisUrl, $resource, $http) {

      var resourceForWebGoalieStats = $resource(constApisUrl + '/forwebgoaliestats');
      var forWebGoalieStatsDataGoodThruUrl = constApisUrl + '/forwebgoaliestatsdatagoodthru';

      var listForWebGoalieStats = function () {
        return resourceForWebGoalieStats.query();
      };

      var getForWebGoalieStatsDataGoodThru = function () {

        return $http.get(forWebGoalieStatsDataGoodThruUrl);
      };

      return {
        listForWebGoalieStats: listForWebGoalieStats,
        getForWebGoalieStatsDataGoodThru: getForWebGoalieStatsDataGoodThru
      };
    }
  ]
);

