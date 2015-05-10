'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServicePlayers",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      // return multiple items
      var resource = $resource(constApisUrl + '/players');

      // return single item
      var resourceByPlayerId = $resource(constApisUrl + '/player/:playerId', { playerId: '@playerId' });

      var listAll = function () {
        return resource.query();
      };

      
      var getByPlayerId = function (playerId) {
        return resourceByPlayerId.get({ playerId: playerId });
      };

      return {
        listAll: listAll,
        getByPlayerId: getByPlayerId
      };
    }
  ]
);

