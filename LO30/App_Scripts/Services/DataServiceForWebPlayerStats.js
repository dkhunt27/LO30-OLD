'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceForWebPlayerStats",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourceForWebPlayerStats = $resource(constApisUrl + '/forwebplayerstats');

      var getForWebPlayerStats = function () {
        return resourceForWebPlayerStats.query();
      };


      return {
        getForWebPlayerStats: getForWebPlayerStats
      };
    }
  ]
);

