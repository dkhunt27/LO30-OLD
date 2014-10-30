'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceForWebGoalieStats",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourceForWebGoalieStats = $resource(constApisUrl + '/forwebgoaliestats');

      var getForWebGoalieStats = function () {
        return resourceForWebGoalieStats.query();
      };


      return {
        getForWebGoalieStats: getForWebGoalieStats
      };
    }
  ]
);

