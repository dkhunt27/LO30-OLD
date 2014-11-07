'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceForWebGoalieStats",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourceForWebGoalieStats = $resource(constApisUrl + '/forWebGoalieStats');

      var listForWebGoalieStats = function () {
        return resourceForWebGoalieStats.query();
      };

      return {
        listForWebGoalieStats: listForWebGoalieStats
      };
    }
  ]
);

