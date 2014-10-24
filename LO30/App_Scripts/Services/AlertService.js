'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory(
  'alertService',
  [
    'toaster',
    function (toaster) {

      var error = function (body, title) {
        toaster.pop("error", title, body, 5000);
      };

      var info = function (body, title) {
        toaster.pop("info", title, body, 5000);
      };

      var success = function (body, title) {
        toaster.pop("success", title, body, 5000);
      };

      var warning = function (body, title) {
        toaster.pop("warning", title, body, 5000);
      };

      return {
        error: error,
        info: info,
        success: success,
        warning: warning
      };
    }
  ]
);
