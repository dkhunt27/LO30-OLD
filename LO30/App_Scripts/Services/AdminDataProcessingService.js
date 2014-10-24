'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory("adminDataProcessingService",
  [
    "$resource",
    function ($resource) {

      var resource = $resource('/api/v1/dataProcessing');

      var postAction = function (model) {
        return resource.save({}, model);
      };

      return {
        postAction: postAction
      };
    }
  ]
);

