(function() {
    "use strict";

    angular.module("bazaar-app",
            [
                "ngResource",
                "ui.router",
                "angular-loading-bar",
                "ngStorage",
                "ngMessages",
                "ngImageLoad"
            ])
        .config(
        function (NgImageLoadServiceProvider, $locationProvider, $stateProvider, $urlRouterProvider) {
            NgImageLoadServiceProvider.setDefault("img/ring.gif");

            $locationProvider.hashPrefix("");
            $urlRouterProvider.otherwise("/");

            $stateProvider
                .state("home",
                    {
                        url: "/",
                        views: {
                            'main':
                            {
                                templateUrl: "angular/views/home.html",
                                controller: "home-controller",
                                controllerAs: "vm"
                            }
                        },
                        authorize: false,
                        guest: false
                })
                .state("search",
                    {
                        url: "/search?q",
                        views: {
                            'main':
                            {
                                templateUrl: "angular/views/search.html",
                                controller: "search-controller",
                                controllerAs: "vm"
                            }
                        },
                        authorize: false,
                        guest: false
                 })
                .state("login",
                    {
                        url: "/login",
                        views: {
                            'main':
                            {
                                templateUrl: "angular/views/login.html",
                                controller: "account-controller",
                                controllerAs: "vm"
                            }
                        },
                        authorize: false,
                        guest: true
                    })
                .state("register",
                    {
                        url: "/register",
                        views: {
                            'main':
                            {
                                templateUrl: "angular/views/register.html",
                                controller: "account-controller",
                                controllerAs: "vm"
                            }
                        },
                        authorize: false,
                        guest: true
                    })
                .state("ad",
                    {
                        url: "/ad/:id",
                        views: {
                            'main':
                            {
                                templateUrl: "angular/views/ad.html",
                                controller: "advertising-controller",
                                controllerAs: "vm"
                            }
                        },
                        authorize: false,
                        guest: false
                    })
                .state("publish",
                    {
                        url: "/publish",
                        views: {
                            'main':
                            {
                                templateUrl: "angular/views/publish.html",
                                controller: "advertising-controller",
                                controllerAs: "vm"
                            }
                        },
                        authorize: true,
                        guest: false
                    })
                .state("account",
                    {
                        url: "/account",
                        views: {
                            'main':
                            {
                                templateUrl: "angular/views/profile.html",
                                controller: "account-controller",
                                controllerAs: "vm"
                            }
                        },
                        authorize: true,
                        guest: false
                    });
        })
        .run(["$transitions", "account-service", function ($transitions, accountService) {
            $transitions.onStart({ }, function (transition) {
                var $state = transition.router.stateService;
                var $view = transition.to();

                if ($view.authorize && !accountService.isAuthenticated)
                    return $state.target("login");

                if ($view.guest && accountService.isAuthenticated)
                    return $state.target("home");

                return $state;
            });
        }]);
})();