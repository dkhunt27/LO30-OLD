'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('gameRosterPlayerController',
  [
    '$scope',
    'externalLibService',
    function ($scope, externalLibService) {
      var _ = externalLibService._;

      $scope.initializeScopeVariables = function () {
        // from directive binding
        // teamGameRoster: {}

        $scope.data = {
        };

        $scope.events = {
          rosteredProcessed: false,
          subbedProcessed: false,
          gameRosterPlayerProcessing: false,
          gameRosterPlayerProcessed: false,
        };

        $scope.user = {
          clickedAddSub: false
        };
      };

      $scope.toggleRosteredSubbedFor = function (teamGameRoster) {
        if (teamGameRoster.rosteredSubbedFor === true) {
          $scope.user.clickedAddSub = true;
        } else {
          $scope.user.clickedAddSub = false;
        }
      }

      $scope.setWatches = function () {
        $scope.$watch('user.clickedAddSub', function (newVal, oldVal) {
          if (newVal && newVal.length && newVal.length > 0) {
            $scope.events.teamRosterLoaded = true;
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

