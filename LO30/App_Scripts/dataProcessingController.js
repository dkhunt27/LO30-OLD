var dataProcessingModule = angular.module("ngDataProcessingModule", ['ngRoute', 'ngResource']);

dataProcessingModule.config(["$routeProvider", function ($routeProvider) {
  $routeProvider.when("/", {
    controller: "dataProcessingController",
    templateUrl: "/Templates/dataProcessing.html"
  });
  $routeProvider.otherwise({ redirectTo: "/" });
}]);

dataProcessingModule.factory("dataProcessingService", ["$resource", function ($resource) {

  var resource = $resource('/api/v1/dataProcessing');

  var postAction = function (model) {
    return resource.save({}, model);
  };

  return {
    postAction: postAction
  };
}]);

dataProcessingModule.controller('dataProcessingController', ['$scope', 'dataProcessingService', function ($scope, dataProcessingService) {
  $scope.dataModel = {
    action: "n/a",
    seasonId: 54,
    playoff: false,
    startingGameId: 3200,
    endingGameId: 3227
  };

  $scope.processCount = {
    processScoreSheetEntries: -1
  }

  $scope.processScoreSheetEntries = function () {
    $scope.dataModel.action = 'ProcessScoreSheetEntries';
    dataProcessingService.postAction($scope.dataModel).$promise
      .then(function (result) {
        // success
        $scope.processCount.processScoreSheetEntries = result.results;
      },
      function (err) {
        // error
        alert("could not process score sheet entries:" + err.message);
      })
      .then(function () {
        alert("here")
      });
  };
}]);

