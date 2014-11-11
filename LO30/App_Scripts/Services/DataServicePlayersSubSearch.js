'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServicePlayersSubSearch",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourcePlayersSubSearch = $resource(constApisUrl + '/playersSubSearch/:position/:ratingMin/:ratingMax', { position: '@position', ratingMin: '@ratingMin', ratingMax: '@ratingMax' });

      var listPlayersSubSearch = function (position, ratingMin, ratingMax) {
        return resourcePlayersSubSearch.query({ position: position, ratingMin: ratingMin, ratingMax: ratingMax });
      };
      
      return {
        listPlayersSubSearch: listPlayersSubSearch
      };
    }
  ]
);

