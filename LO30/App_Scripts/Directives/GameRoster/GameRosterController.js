'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('gameRosterController',
  [
    '$scope',
    'externalLibService',
    function ($scope, externalLibService) {
      var _ = externalLibService._;

      $scope.initializeScopeVariables = function () {
        // from directive binding
        // teamRoster: [],
        // gameRoster: [],
        // homeTeam: false,

        $scope.data = {
          goalies: [],
          line1: [],
          line2: [],
          line3: [],
          teamGameRoster: []
        };

        $scope.events = {
          teamRosterLoaded: false,
          gameRosterLoaded: false,
          teamGameRosterProcessing: false,
          teamGameRosterProcessed: false,
        };

        $scope.user = {
        };
      };

      $scope.getGoalies = function () {
        $scope.data.goalies = _.filter($scope.data.teamGameRoster, function (item) { return item.rostered.position === 'G'; });
        return;
      };

      $scope.getLine1 = function () {
        $scope.data.line1 = _.filter($scope.data.teamGameRoster, function (item) { return item.rostered.position !== 'G' && item.rostered.line === 1; });
        return;
      };

      $scope.getLine2 = function () {
        $scope.data.line2 = _.filter($scope.data.teamGameRoster, function (item) { return item.rostered.position !== 'G' && item.rostered.line === 2; });
        return;
      };

      $scope.getLine3 = function () {
        $scope.data.line3 = _.filter($scope.data.teamGameRoster, function (item) { return item.rostered.position !== 'G' && item.rostered.line === 3; });
        return;
      };

      $scope.processTeamAndGameRosters = function () {
        // loop through each team roster and add them to the teamGameRoster
        // and then match to the game roster (if exists) and determine who is in/out and who subbed for who
        $scope.events.teamGameRosterProcessing = true;
        $scope.events.teamGameRosterProcessed = false;

        $scope.teamRoster.forEach(function (teamRosterItem) {
          var teamGameRoster = {
            rosteredDidNotPlay: true,
            rostered: {},
            rosteredSubbedFor: false,
            subbed: []
          };

          // set the teamRoster
          teamGameRoster.rostered = teamRosterItem;

          //first determine if the rostered player was in/out
          var rosteredIn = _.find($scope.gameRoster, function (item) { return item.playerId === teamRosterItem.playerId; });

          if (rosteredIn) {
            teamGameRoster.rosteredDidNotPlay = false;
          }

          //next see if rostered player was also subbed for 
          //  FYI rosteredDidNotPlay and rosteredSubbedFor are not mutually exclusive...they can be:
          //  (true/true) rostered player didn't play and there is a sub for him
          //  (true/false) rostered player didn't play and there was no sub for him
          //  (false/true) rostered player played but was late, got hurt, had to leave early, etc and someone subbed for him
          //  (false/false) rostered player played no one subbed for him


          // subbed is an array because it is possible that the sub didn't play the entire game, maybe he was covering for late arriving other sub, maybe he got hurt, had to leave early

          var subbedFor = _.filter($scope.gameRoster, function (item) {
            return item.subbingForPlayerId === teamRosterItem.playerId;
          });

          if (subbedFor && subbedFor.length && subbedFor.length > 0) {
            teamGameRoster.rosteredSubbedFor = true;
            teamGameRoster.subbed = subbedFor;
          }

          $scope.data.teamGameRoster.push(teamGameRoster);
        });

        $scope.events.teamGameRosterProcessing = false;
        $scope.events.teamGameRosterProcessed = true;
      };

      $scope.setWatches = function () {
        $scope.$watch('teamRoster', function (newVal, oldVal) {
          if (newVal && newVal.length && newVal.length > 0) {
            $scope.events.teamRosterLoaded = true;
          }
        }, true);

        $scope.$watch('gameRoster', function (newVal, oldVal) {
          if (newVal && newVal.length && newVal.length > 0) {
            if (!_.isEqual(newVal, oldVal)) {
              $scope.events.gameRosterLoaded = true;
            }
          }
        }, true);

        $scope.$watch('events.teamRosterLoaded', function (newVal, oldVal) {
          if (newVal === true) {
            if ($scope.events.gameRosterLoaded === true) {
              if ($scope.events.teamGameRosterProcessing === false && $scope.events.teamGameRosterProcessed === false) {
                $scope.processTeamAndGameRosters();
                $scope.getGoalies();
                $scope.getLine1();
                $scope.getLine2();
                $scope.getLine3();
              }
            }
          }
        }, true);

        $scope.$watch('events.gameRosterLoaded', function (newVal, oldVal) {
          if (newVal === true) {
            if ($scope.events.teamRosterLoaded === true) {
              if ($scope.events.teamGameRosterProcessing === false && $scope.events.teamGameRosterProcessed === false) {
                $scope.processTeamAndGameRosters();
                $scope.getGoalies();
                $scope.getLine1();
                $scope.getLine2();
                $scope.getLine3();
              }
            }
          }
        }, true);
      };

      $scope.activate = function () {
        $scope.initializeScopeVariables();
        $scope.setWatches();
      };

      $scope.activate();
    }
  ]
);

