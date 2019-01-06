(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .service("account-service", account_service);

    account_service.$inject = ["$q", "bazaar-service", "$localStorage"];

    function account_service($q, bazaarService, $localStorage) {

        var getCurrentUser = function() {
            return $q(function(resolved, rejected) {
                bazaarService.account.me({},
                    function(data) {
                        resolved(data.value);
                    },
                    function(error) {
                        rejected(error);
                    });
            });
        };

        var service = {
            isAuthenticated: typeof $localStorage.token !== "undefined",

            user: null,

            login: function(model) {
                if (service.isAuthenticated)
                    return $q.resolve();

                return $q(function(resolved, rejected) {
                    bazaarService.account.login("grant_type=password&username=" + model.email + "&password=" + model.password,
                        function(data) {
                            $localStorage.token = data.access_token;
                            service.isAuthenticated = true;

                            getCurrentUser().then(function(user) {
                                service.user = user;
                                resolved();
                            });
                        },
                        function(error) {
                            if (error.status === 400)
                                rejected(error.data.error);
                            else
                                rejected(error.data.message);
                        }
                    );
                });
            },

            register: function(model) {
                if (service.isAuthenticated)
                    return $q.rejected();

                return $q(function (resolved, rejected) {
                    bazaarService.account.register(model,
                        function () {
                            resolved();
                        },
                        function (error) {
                            rejected(error.data.errors);
                        });
                });
            },

            changeAvatar: function (blob) {
                if (!service.isAuthenticated)
                    return $q.rejected();

                return $q(function (resolved, rejected) {
                    bazaarService.image.sendAvatar(blob,
                        function (data) {
                            bazaarService.account.changeAvatar("=" + data.hash,
                                function() {
                                    getCurrentUser().then(function(user) {
                                        service.user = user;
                                        resolved();
                                    });
                                },
                                function (error) {
                                    rejected(error);
                                });
                        },
                        function (error) {
                            rejected(error.data.errors);
                        });
                });
            },

            changePassword: function (model) {
                if (!service.isAuthenticated)
                    return $q.rejected();

                return $q(function (resolved, rejected) {
                    bazaarService.account.changePassword(model,
                        function () {
                            resolved();
                        },
                        function (error) {
                            rejected(error.data.errors);
                        });
                });
            },

            logout: function() {
                service.user = {};
                service.isAuthenticated = false;
                delete $localStorage.token;
            },

            getToken: function() {
                return $localStorage.token;
            }
        };

        getCurrentUser().then(
            function (user) {
                service.user = user;
            },
            function () {
                service.logout();
            }
        );

        return service;
    }
})();