(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .factory("interceptor", interceptor);

    interceptor.$inject = ["$q", "$injector"];

    function interceptor($q, $injector) {
        var factory = {
            request: function (config) {
                var service = $injector.get("account-service");

                if (service.isAuthenticated)
                    config.headers["Authorization"] = "Bearer " + service.getToken();

                return config;
            }
        };

        return factory;
    }

    angular
        .module("bazaar-app")
        .config(function ($httpProvider) {
            $httpProvider.interceptors.push("interceptor");
        });
})();