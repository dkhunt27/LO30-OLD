var lo30NgApp = angular.module("lo30NgApp", ['ngRoute', 'ngResource', 'ngAnimate', 'ui.bootstrap','toaster']);

lo30NgApp.value("constApisUrl", "/api/v1");

lo30NgApp.config(
  [
    "$routeProvider",
    function ($routeProvider) {
      $routeProvider.when("/News", {
        controller: "newsController",
        templateUrl: "/Templates/articlesView.html" 
      });
      $routeProvider.when("/DataProcessing", {
        controller: "adminDataProcessingController",
        templateUrl: "/Templates/AdminDataProcessingAngular.html"
      });
      $routeProvider.when("/", {
        controller: "scoreSheetController",
        templateUrl: "/Templates/ScoreSheetAngular.html"
      });
    }
  ]
);