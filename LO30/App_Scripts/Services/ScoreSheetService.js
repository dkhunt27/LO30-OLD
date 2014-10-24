'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("scoreSheetService",
  [
    "MOCK_SERVICES",
    "$resource",
    function (MOCK_SERVICES, $resource) {

      if (MOCK_SERVICES) {

      } else {
        var resource = $resource('/api/v1/scoreSheet');

        var postAction = function (model) {
          return resource.save({}, model);
        };

      }

      var postAction = function (model) {
        if (MOCK_SERVICES) {

        } else {
          return resource.save({}, model);
        }
      };

      return {
        postAction: postAction
      };
    }
  ]
);

