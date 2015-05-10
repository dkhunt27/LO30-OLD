'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServicePlayerStatsCareer",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      // return multiple items
      var resourceQuery = $resource(constApisUrl + '/playerStatsCareer/:playerId');

      // return single item
      var resourceGet = $resource(constApisUrl + '/playerStatCareer/:playerId/:playoffs', { playerId: '@playerId', playoffs: '@playoffs' });

      var listAll = function () {
        return resourceQuery.query();
      };

      var listByPlayerId = function (playerId) {
        return resourceQuery.query({ playerId: playerId });
      };

      var getByPlayerIdPlayoffs = function (playerId, playoffs) {
        return resourceGet.get({ playerId: playerId, playoffs: playoffs });
      };

      return {
        listAll: listAll,
        listByPlayerId: listByPlayerId,
        getByPlayerIdPlayoffs: getByPlayerIdPlayoffs
      };
    }
  ]
);

