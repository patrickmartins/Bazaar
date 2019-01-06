(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .controller("navbar-controller", controller);

    controller.$inject = ["$state", "account-service"];

    function controller($state, accountService) {
        /* jshint validthis:true */
        var vm = this;

        vm.accountService = accountService;

        vm.logout = function () {
            vm.accountService.logout();
            $state.go("home");
        };

        vm.search = function(a) {
            $state.go("search", { q: a });
        };
    }
})();
