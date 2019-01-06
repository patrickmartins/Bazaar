(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .controller("home-controller", controller);

    controller.$inject = ["advertising-service"];

    function controller(advertisingService) {
        /* jshint validthis:true */
        var vm = this;

        vm.ads = [];

        (function getAdsMostRecent() {
            advertisingService.getMostRecent(8)
                .then(function (ads) {
                    vm.ads = ads;
                });
        })();
    }
})();
