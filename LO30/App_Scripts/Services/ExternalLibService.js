'use strict';

/* jshint -W117 */ //(remove the undefined warning)
lo30NgApp.factory(
  'externalLibService',
  [
    function () {

      /* global _, sjv */

      var _temp = _;
      var sjvtemp = sjv;

      return {
        _: _temp,
        sjv: sjvtemp
      };
    }
  ]
);