'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceGameOutcomes",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourceGameOutcomes = $resource(constApisUrl + '/gameOutcomes/:fullDetail', { fullDetail: '@fullDetail' });
      var resourceGameOutcomesByGameId = $resource(constApisUrl + '/gameOutcomes/:gameId/:fullDetail', { gameId: '@gameId', fullDetail: '@fullDetail' });
      var resourceGameOutcomesBySeasonTeamId = $resource(constApisUrl + '/gameOutcomesBySeasonTeam/:seasonId/:playoffs/:seasonTeamId/:fullDetail', { seasonId: '@seasonId', playoffs: '@playoffs', seasonTeamId: '@seasonTeamId', fullDetail: '@fullDetail' });

      var resourceGameOutcomeByGameIdAndHomeTeam = $resource(constApisUrl + '/gameOutcome/:gameId/:homeTeam/:fullDetail', { gameId: '@gameId', homeTeam: '@homeTeam', fullDetail: '@fullDetail' });

      var listGameOutcomes = function (fullDetail) {
        return resourceGameOutcomes.query({ fullDetail: fullDetail });
      };

      var listGameOutcomesByGameId = function (gameId, fullDetail) {
        return resourceGameOutcomesByGameId.query({ gameId: gameId, fullDetail: fullDetail });
      };

      var getGameOutcomeByGameIdAndHomeTeam = function (gameId, homeTeam, fullDetail) {
        return resourceGameOutcomesByGameIdAndHomeTeam.get({ gameId: gameId, homeTeam: homeTeam, fullDetail: fullDetail });
      };

      var listGameOutcomesBySeasonTeamId = function (seasonId, playoffs, seasonTeamId, fullDetail) {
        return resourceGameOutcomesBySeasonTeamId.query({ seasonId: seasonId, playoffs: playoffs, seasonTeamId: seasonTeamId, fullDetail: fullDetail });
      };

      return {
        listGameOutcomes: listGameOutcomes,
        listGameOutcomesByGameId: listGameOutcomesByGameId,
        getGameOutcomeByGameIdAndHomeTeam: getGameOutcomeByGameIdAndHomeTeam,
        listGameOutcomesBySeasonTeamId: listGameOutcomesBySeasonTeamId
      };
    }
  ]
);

