var lo30NgApp = angular.module("lo30NgApp", ['ngRoute', 'ngResource', 'ngAnimate', 'ui.bootstrap', 'toaster', 'angularMoment']);

lo30NgApp.value("constApisUrl", "/api/v1");

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
      $routeProvider.when("/Admin/Test/BoxScore", {
        controller: "testBoxScoreController",
        templateUrl: "/Templates/Admin/Test/BoxScore.html"
      });
      $routeProvider.when("/Admin/Test/PlayerSubSearch", {
        controller: "testPlayerSubSearchController",
        templateUrl: "/Templates/Admin/Test/PlayerSubSearch.html"
      });

      // Directives

      // Games
      $routeProvider.when("/Games/BoxScores/:gameId", {
        controller: "gamesBoxScoresController",
        templateUrl: "/Templates/Games/BoxScores.html"
      });
      $routeProvider.when("/Games/Results/:seasonId/:playoffs/:seasonTeamId", {
        controller: "gamesResultsController",
        templateUrl: "/Templates/Games/Results.html"
      });

      // Home

      // Players
      $routeProvider.when("/Players/Player", {
        controller: "playersPlayerController",
        templateUrl: "/Templates/Players/Player.html"
      });
      $routeProvider.when("/Players/Player/:playerId", {
        controller: "playersPlayerController",
        templateUrl: "/Templates/Players/Player.html"
      });
      $routeProvider.when("/Players/Player/:playerId/:seasonId", {
        controller: "playersPlayerController",
        templateUrl: "/Templates/Players/Player.html"
      });
      $routeProvider.when("/Players/Goalie", {
        controller: "playersPlayerController",
        templateUrl: "/Templates/Players/Player.html"
      });
      $routeProvider.when("/Players/Goalie/:playerId", {
        controller: "playersGoalieController",
        templateUrl: "/Templates/Players/Goalie.html"
      });

      // ScoreSheets
      $routeProvider.when("/Games/BoxScores", {
        controller: "gamesBoxScoresController",
        templateUrl: "/Templates/Games/BoxScores.html"
      });
      $routeProvider.when("/ScoreSheets/Entry/:gameId", {
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
      $routeProvider.when("/Stats/Players/:seasonId/:playoffs", {
        controller: "statsPlayersController",
        templateUrl: "/Templates/Stats/Players.html"
      });
      $routeProvider.when("/Stats/Goalies/:seasonId/:playoffs", {
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