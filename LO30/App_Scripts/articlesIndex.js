var articlesIndexModule = angular.module("articlesIndex", ['ngRoute']);

articlesIndexModule.config(["$routeProvider", function ($routeProvider) {
  $routeProvider.when("/", {
    controller: "articlesController",
    templateUrl: "/Templates/articlesView.html"
  });


  $routeProvider.otherwise({ redirectTo: "/" });
}]);

articlesIndexModule.factory("dataService", ["$http", "$q", function ($http, $q) {

  var _articles = [];
  var _isInit = false;

  var _isReady = function () {
    return _isInit;
  };

  var _getArticles = function () {

    var deferred = $q.defer();

    $http.get("/api/v1/articles")
      .then(function (result) {
        // Successful
        angular.copy(result.data, _articles);
        _isInit = true;
        deferred.resolve();
      },
      function () {
        // Error
        deferred.reject();
      });

    return deferred.promise;
  };

  var _addArticle = function (newArticle) {
    var deferred = $q.defer();

    $http.post("/api/v1/articles", newArticle)
     .then(function (result) {
       // success
       var newlyCreatedArticle = result.data;
       _articles.splice(0, 0, newlyCreatedArticle);
       deferred.resolve(newlyCreatedArticle);
     },
     function () {
       // error
       deferred.reject();
     });

    return deferred.promise;
  };

  function _findArticle(id) {
    var found = null;

    $.each(_articles, function (i, item) {
      if (item.id === id) {
        found = item;
        return false;
      }
    });

    return found;
  }

  var _getArticleById = function (id) {
    var deferred = $q.defer();

    if (_isReady()) {
      var article = _findArticle(id);
      if (article) {
        deferred.resolve(article);
      } else {
        deferred.reject();
      }
    } else {
      _getArticles()
        .then(function () {
          // success
          var article = _findArticle(id);
          if (article) {
            deferred.resolve(article);
          } else {
            deferred.reject();
          }
        },
        function () {
          // error
          deferred.reject();
        });
    }

    return deferred.promise;
  };

  return {
    articles: _articles,
    getArticles: _getArticles,
    addArticle: _addArticle,
    isReady: _isReady,
    getArticleById: _getArticleById,
  };
}]);

var articlesController = ["$scope", "$http", "dataService",
  function ($scope, $http, dataService) {
    $scope.data = dataService;
    $scope.articles = [];
    $scope.isBusy = false;

    if (dataService.isReady() === false) {
      $scope.isBusy = true;

      dataService.getArticles()
        .then(function () {
          // success
          angular.copy(dataService.articles, $scope.articles);
        },
        function () {
          // error
          alert("could not load articles");
        })
        .then(function () {
          $scope.isBusy = false;
        });
    }
  }];

