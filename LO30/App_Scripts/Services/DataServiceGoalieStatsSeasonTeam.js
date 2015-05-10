'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceGoalieStatsSeasonTeam",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      // return multiple items
      var resourceQuery = $resource(constApisUrl + '/goalieStatsSeasonTeam/:playerId/:seasonId', { playerId: '@playerId', seasonId: '@seasonId' });

      // return single item
      var resourceGet = $resource(constApisUrl + '/goalieStatSeasonTeam/:playerId/:seasonTeamId', { playerId: '@playerId', seasonTeamId: '@seasonTeamId' });

      var listAll = function () {
        return resourceQuery.query();
      };

      var listByPlayerId = function (playerId) {
        return resourceQuery.query({ playerId: playerId });
      };

      var listByPlayerIdSeasonId = function (playerId, seasonId) {
        return resourceQuery.query({ playerId: playerId, seasonId: seasonId });
      };
      
      var getByPlayerIdSeasonTeamId = function (playerId, seasonTeamId) {
        return resourceGet.get({ playerId: playerId, seasonTeamId: seasonTeamId });
      };

      return {
        listAll: listAll,
        listByPlayerId: listByPlayerId,
        listByPlayerIdSeasonId: listByPlayerIdSeasonId,
        getByPlayerIdSeasonTeamId: getByPlayerIdSeasonTeamId
      };
    }
  ]
);

