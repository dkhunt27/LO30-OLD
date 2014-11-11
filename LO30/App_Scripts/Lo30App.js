var lo30NgApp = angular.module("lo30NgApp", ['ngRoute', 'ngResource', 'ngAnimate', 'ui.bootstrap','toaster']);

lo30NgApp.value("constApisUrl", "/api/v2");

lo30NgApp.config(
  [
    "$routeProvider",
    function ($routeProvider) {
      // Admin
      $routeProvider.when("/Admin/DataProcessing", {
        controller: "adminDataProcessingController",
        templateUrl: "/Templates/Admin/DataProcessing.html"
      });
      $routeProvider.when("/Admin/Settings", {
        controller: "adminSettingsController",
        templateUrl: "/Templates/Admin/Settings.html"
      });
      $routeProvider.when("/Admin/Test/PlayerSubSearch", {
        controller: "testPlayerSubSearchController",
        templateUrl: "/Templates/Admin/Test/PlayerSubSearch.html"
      });

      // Directives
      // Home

      // ScoreSheets
      $routeProvider.when("/ScoreSheets/Entry", {
        controller: "scoreSheetsEntryController",
        templateUrl: "/Templates/ScoreSheets/Entry.html"
      });

      // Standings
      $routeProvider.when("/Standings/RegularSeason", {
        controller: "standingsRegularSeasonController",
        templateUrl: "/Templates/Standings/RegularSeason.html"
      });
      $routeProvider.when("/Standings/Playoffs", {
        controller: "standingsPlayoffsController",
        templateUrl: "/Templates/Standings/Playoffs.html"
      });

      // Stats
      $routeProvider.when("/Stats/Players", {
        controller: "statsPlayersController",
        templateUrl: "/Templates/Stats/Players.html"
      });
      $routeProvider.when("/Stats/Goalies", {
        controller: "statsGoaliesController",
        templateUrl: "/Templates/Stats/Goalies.html"
      });



      $routeProvider.when("/News", {
        controller: "newsController",
        templateUrl: "/Templates/articlesView.html"
      });



      $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/Templates/Home/Index.html"
      });
    }
  ]
);