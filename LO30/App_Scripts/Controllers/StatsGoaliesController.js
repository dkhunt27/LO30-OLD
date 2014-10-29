'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.controller('statsGoaliesController',
  [
    '$scope',
    '$timeout',
    function ($scope, $timeout) {

      $scope.searchText = null;
      $scope.selectedTeam = null;
      $scope.selectedPosition = null;
      $scope.selectedLine = null;
      $scope.selectedSub = null;

      $scope.filterByTeam = function (team) {
        var existingSearch = "team:" + $scope.filterByTeamMapper($scope.selectedTeam);
        var newSearch = "team:" + $scope.filterByTeamMapper(team);

        if ($scope.selectedTeam) {
          // if selected populated, that means there was a search before...remove that first 
          $scope.removeExistingSearchFromSearch(existingSearch);
        }

        if ($scope.selectedTeam === team) {
          // if same thing was clicked again...already been remove from search...just reset vars
          $scope.selectedTeam = null;
        } else {
          // this is a new search, add to search
          $scope.prepSearchForNewSearch();
          $scope.addNewSearchToSearch(newSearch);
          $scope.selectedTeam = team;
        }

        $scope.resetSort();
      };

      $scope.filterByTeamMapper = function (team) {
        var teamMapped;
        // map filter keys from columns to object
        switch (team) {
          case "Bill Brown":
            teamMapped = "Bill Brown Auto Clinic";
            break;
          case "Hunt's Ace":
            teamMapped = "Hunt's Ace Hardware";
            break;
          case "LAB/PSI":
            teamMapped = "Liv. Auto Body/Phillips Service Ind";
            break;
          case "Zas Ent":
            teamMapped = "Zaschak Enterprises";
            break;
          case "DPKZ":
            teamMapped = "DeBrincat Padgett Kobliska Zick";
            break;
          case "Villanova":
            teamMapped = "Villanova Construction";
            break;
          case "Glover":
            teamMapped = "Jeff Glover Realtors";
            break;
          case "D&G":
            teamMapped = "D&G Heating & Cooling";
            break;
          default:
            teamMapped = team;
        }

        return teamMapped;
      };

      $scope.filterBySub = function (sub) {
        var existingSearch = "sub:" + $scope.filterBySubMapper($scope.selectedSub);
        var newSearch = "sub:" + $scope.filterBySubMapper(sub);

        if ($scope.selectedSub) {
          // if selected populated, that means there was a search before...remove that first 
          $scope.removeExistingSearchFromSearch(existingSearch);
        }

        if ($scope.selectedSub === sub) {
          // if same thing was clicked again...already been remove from search...just reset vars
          $scope.selectedSub = null;
        } else {
          // this is a new search, add to search
          $scope.prepSearchForNewSearch();
          $scope.addNewSearchToSearch(newSearch);
          $scope.selectedSub = sub;
        }

        $scope.resetSort();
      };

      $scope.filterBySubMapper = function (sub) {
        var subMapped;
        // map filter keys from columns to object
        switch (sub) {
          case "With":
            subMapped = "Y";
            break;
          case "Without":
            subMapped = "N";
            break;
          default:
            subMapped = sub;
        }

        return subMapped;
      };

      $scope.filterByLine = function (line) {
        var existingSearch = "line:" + $scope.selectedLine;
        var newSearch = "line:" + line;

        if ($scope.selectedLine) {
          // if selected populated, that means there was a search before...remove that first 
          $scope.removeExistingSearchFromSearch(existingSearch);
        }

        if ($scope.selectedLine === line) {
          // if same thing was clicked again...already been remove from search...just reset vars
          $scope.selectedLine = null;
        } else {
          // this is a new search, add to search
          $scope.prepSearchForNewSearch();
          $scope.addNewSearchToSearch(newSearch);
          $scope.selectedLine = line;
        }

        $scope.resetSort();
      };

      $scope.filterByPosition = function (position) {
        var existingSearch = "position:" + $scope.selectedPosition;
        var newSearch = "position:" + position;

        if ($scope.selectedPosition) {
          // if selected populated, that means there was a search before...remove that first 
          $scope.removeExistingSearchFromSearch(existingSearch);
        }

        if ($scope.selectedPosition === position) {
          // if same thing was clicked again...already been remove from search...just reset vars
          $scope.selectedPosition = null;
        } else {
          // this is a new search, add to search
          $scope.prepSearchForNewSearch();
          $scope.addNewSearchToSearch(newSearch);
          $scope.selectedPosition = position;
        }

        $scope.resetSort();
      };

      $scope.resetSort = function (newSearch) {
        //$scope.sortDescFirst('p');
      };

      $scope.addNewSearchToSearch = function (newSearch) {
        $scope.searchText = $scope.searchText + newSearch;
      };

      $scope.removeExistingSearchFromSearch = function (existingSearch) {
        $scope.searchText = $scope.searchText.replace(", " + existingSearch, "");
        $scope.searchText = $scope.searchText.replace(existingSearch + ", ", "");
        $scope.searchText = $scope.searchText.replace(existingSearch, "");
      };

      $scope.prepSearchForNewSearch = function () {
        if ($scope.searchText) {
          $scope.searchText = $scope.searchText + ", ";
        } else {
          $scope.searchText = "";
        }
      };

      $scope.sortAscFirst = function (column) {
        if ($scope.sortOn === column) {
          $scope.sortDirection = !$scope.sortDirection;
        } else {
          $scope.sortOn = column;
          $scope.sortDirection = false;
        }
      };

      $scope.sortDescFirst = function (column) {
        if ($scope.sortOn === column) {
          $scope.sortDirection = !$scope.sortDirection;
        } else {
          $scope.sortOn = column;
          $scope.sortDirection = true;
        }
      };

      $scope.removeSearch = function () {
        $scope.searchText = null;
        $scope.selectedTeam = null;
        $scope.selectedPosition = null;
        $scope.selectedLine = null;
        $scope.selectedSub = null;
      }

      $scope.activate = function () {
        $scope.calcWinPct();
        //$timeout(function () {
        $scope.sortAscFirst('gaa');
        $scope.filterBySub("Without");
        //},0);  // using timeout so it fires when done rendering
      };

      $scope.teams = [
        "Bill Brown",
      "Hunt's Ace",
      "LAB/PSI",
      "Zas Ent",
      "DPKZ",
      "Villanova",
      "Glover",
      "D&G"
      ];

      $scope.positions = [
        "F",
        "D",
        "G"
      ];

      $scope.subs = [
        "With",
        "Without",
      ];

      $scope.lines = [
        "1",
        "2",
        "3",
      ];

      $scope.calcWinPct = function () {
        angular.forEach($scope.goalieStats, function (item) {
          item.winPercent = item.w / item.gp;
        })
      }

      $scope.goalieStats = [
  {
    pid: 650,
    stidpf: 312,
    sid: 54,
    player: "Chris Ciszewski",
    team: "Hunt's Ace Hardware",
    sub: "Y",
    gp: 1,
    ga: 1,
    gaa: 1,
    so: 0,
    w: 1
  },
  {
    pid: 634,
    stidpf: 315,
    sid: 54,
    player: "Tony Brosky",
    team: "Liv. Auto Body/Phillips Service Ind",
    sub: "N",
    gp: 10,
    ga: 17,
    gaa: 1.7,
    so: 3,
    w: 7
  },
  {
    pid: 49,
    stidpf: 312,
    sid: 54,
    player: "John Camillo",
    team: "Hunt's Ace Hardware",
    sub: "N",
    gp: 7,
    ga: 15,
    gaa: 2.142857142857143,
    so: 2,
    w: 5
  },
  {
    pid: 682,
    stidpf: 311,
    sid: 54,
    player: "Scott Fassett",
    team: "Jeff Glover Realtors",
    sub: "N",
    gp: 8,
    ga: 18,
    gaa: 2.25,
    so: 0,
    w: 2
  },
  {
    pid: 676,
    stidpf: 312,
    sid: 54,
    player: "Mike Guider Jr",
    team: "Hunt's Ace Hardware",
    sub: "Y",
    gp: 2,
    ga: 5,
    gaa: 2.5,
    so: 0,
    w: 1
  },
  {
    pid: 678,
    stidpf: 308,
    sid: 54,
    player: "Mark Felker",
    team: "Bill Brown Auto Clinic",
    sub: "N",
    gp: 10,
    ga: 29,
    gaa: 2.9,
    so: 1,
    w: 6
  },
  {
    pid: 619,
    stidpf: 313,
    sid: 54,
    player: "Brad Villa",
    team: "D&G Heating & Cooling",
    sub: "N",
    gp: 10,
    ga: 29,
    gaa: 2.9,
    so: 0,
    w: 3
  },
  {
    pid: 177,
    stidpf: 310,
    sid: 54,
    player: "Steve Roth",
    team: "Zaschak Enterprises",
    sub: "N",
    gp: 11,
    ga: 33,
    gaa: 3,
    so: 0,
    w: 6
  },
  {
    pid: 678,
    stidpf: 312,
    sid: 54,
    player: "Mark Felker",
    team: "Hunt's Ace Hardware",
    sub: "Y",
    gp: 1,
    ga: 3,
    gaa: 3,
    so: 0,
    w: 0
  },
  {
    pid: 682,
    stidpf: 313,
    sid: 54,
    player: "Scott Fassett",
    team: "D&G Heating & Cooling",
    sub: "Y",
    gp: 1,
    ga: 3,
    gaa: 3,
    so: 0,
    w: 0
  },
  {
    pid: 688,
    stidpf: 309,
    sid: 54,
    player: "Lenny Domanke",
    team: "DeBrincat Padgett Kobliska Zick",
    sub: "Y",
    gp: 2,
    ga: 8,
    gaa: 4,
    so: 0,
    w: 0
  },
  {
    pid: 81,
    stidpf: 311,
    sid: 54,
    player: "Ron Gabon",
    team: "Jeff Glover Realtors",
    sub: "Y",
    gp: 1,
    ga: 4,
    gaa: 4,
    so: 0,
    w: 0
  },
  {
    pid: 737,
    stidpf: 309,
    sid: 54,
    player: "Brian Whetstone",
    team: "DeBrincat Padgett Kobliska Zick",
    sub: "N",
    gp: 9,
    ga: 42,
    gaa: 4.666666666666667,
    so: 0,
    w: 3
  },
  {
    pid: 749,
    stidpf: 314,
    sid: 54,
    player: "Jon Ameel",
    team: "Villanova Construction",
    sub: "N",
    gp: 11,
    ga: 60,
    gaa: 5.454545454545454,
    so: 0,
    w: 2
  }
      ];

      $scope.activate();
    }
  ]
);