'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServicePlayers",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourcePlayers = $resource(constApisUrl + '/players');
      var resourcePlayersSubSearch = $resource(constApisUrl + '/players/:position/:ratingMin/:ratingMax', { position: '@position', ratingMin: '@ratingMin', ratingMax: '@ratingMax' });
      var resourcePlayerByPlayerId = $resource(constApisUrl + '/players/:playerId', { playerId: '@playerId' });

      var listPlayers = function () {
        return resourcePlayers.query();
      };

      var listPlayersSubSearch = function (position, ratingMin, ratingMax) {
        return resourcePlayersSubSearch.query({ position: position, ratingMin: ratingMin, ratingMax: ratingMax });
      };
      
      var getPlayerByPlayerId = function (seasonTeamId, yyyymmdd, playerId) {
        return resourcePlayerByPlayerId.get({ playerId: playerId });
      };

      return {
        listPlayers: listPlayers,
        listPlayersSubSearch: listPlayersSubSearch,
        getPlayerByPlayerId: getPlayerByPlayerId
      };
    }
  ]
);

