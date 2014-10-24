'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceGames",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourceGames = $resource(constApisUrl + '/games');
      var resourceGameByGameId = $resource(constApisUrl + '/games/:gameId', {gameId: '@gameId'});

      var getGames = function () {
        return resourceGames.query();
      };

      var getGameByGameId = function (gameId) {
        return resourceGameByGameId.get({ gameId: gameId });
      };

      return {
        getGames: getGames,
        getGameByGameId: getGameByGameId
      };
    }
  ]
);

