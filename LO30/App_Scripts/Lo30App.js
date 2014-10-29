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
      $routeProvider.when("/ScoreSheet", {
        controller: "scoreSheetController",
        templateUrl: "/Templates/ScoreSheetAngular.html"
      });
      $routeProvider.when("/Standings/RegularSeason", {
        controller: "standingsRegularSeasonController",
        templateUrl: "/Templates/Standings/RegularSeason.html"
      });
      $routeProvider.when("/Stats/Players", {
        controller: "statsPlayersController",
        templateUrl: "/Templates/Stats/Players.html"
      });
      $routeProvider.when("/Stats/Goalies", {
        controller: "statsGoaliesController",
        templateUrl: "/Templates/Stats/Goalies.html"
      });
      $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/Templates/Home/Index.html"
      });
    }
  ]
);