'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("dataServiceTeamRosters",
  [
    "constApisUrl",
    "$resource",
    function (constApisUrl, $resource) {

      var resourceTeamRosters = $resource(constApisUrl + '/teamRosters');
      var resourceTeamRostersBySeasonTeamIdAndYYYYMMDD = $resource(constApisUrl + '/teamRosters/:seasonTeamId/:yyyymmdd', { seasonTeamId: '@seasonTeamId', yyyymmdd: '@yyyymmdd' });
      var resourceTeamRosterBySeasonTeamIdPlayerIdAndYYYYMMDD = $resource(constApisUrl + '/teamRoster/:seasonTeamId/:yyyymmdd/:playerId', { seasonTeamId: '@seasonTeamId', yyyymmdd: '@yyyymmdd', playerId: '@playerId' });

      var getTeamRosters = function () {
        return resourceTeamRosters.query();
      };

      var getTeamRostersBySeasonTeamIdAndYYYYMMDD = function (seasonTeamId, yyyymmdd) {
        return resourceTeamRostersBySeasonTeamIdAndYYYYMMDD.query({ seasonTeamId: seasonTeamId, yyyymmdd: yyyymmdd });
      };
      
      var getTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId = function (seasonTeamId, yyyymmdd, playerId) {
        return resourceTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId.get({ seasonTeamId: seasonTeamId, yyyymmdd: yyyymmdd, playerId: playerId });
      };

      return {
        getTeamRosters: getTeamRosters,
        getTeamRostersBySeasonTeamIdAndYYYYMMDD: getTeamRostersBySeasonTeamIdAndYYYYMMDD,
        getTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId: getTeamRosterBySeasonTeamIdYYYYMMDDAndPlayerId
      };
    }
  ]
);

