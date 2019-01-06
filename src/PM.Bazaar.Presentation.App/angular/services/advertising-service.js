(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .factory("advertising-service", advertising_service);

    advertising_service.$inject = ["$q", "bazaar-service", "account-service"];

    function advertising_service($q, bazaarService, accountService) {
        var service = {
            search: function (filters) {
                return $q(function (resolved, rejected) {
                    bazaarService.ad.search(filters,
                        function (data) {
                            resolved(data);
                        },
                        function (error) {
                            rejected(error);
                        });
                });
            },
            getMostRecent: function(number) {
                return $q(function(resolved, rejected) {
                    bazaarService.ad.getMostRecent({ number: number },
                        function(data) {
                            resolved(data);
                        },
                        function(error) {
                            rejected(error);
                        });
                });
            },
            getById: function (idAd) {
                return $q(function (resolved, rejected) {
                    bazaarService.ad.byId({ id:idAd },
                        function (ad) {
                            resolved(ad.value);
                        },
                        function (error) {
                            rejected(error);
                        });
                });
            },
            publish: function (ad) {
                if (!accountService.isAuthenticated)
                    return $q.rejected();

                return $q(function (resolved, rejected) {
                    bazaarService.ad.publish(ad,
                        function () {
                            resolved();
                        },
                        function (error) {
                            rejected(error.data.errors);
                        });
                });
            },
            ask: function (idAd, question) {
                if (!accountService.isAuthenticated)
                    return $q.rejected();

                return $q(function (resolved, rejected) {
                    bazaarService.ad.ask({ idAd: idAd }, question,
                        function () {
                            resolved();
                        },
                        function (error) {
                            rejected(error.data.errors);
                        });
                });
            },
            answer: function (idAd, idQuestion, response) {
                if (!accountService.isAuthenticated)
                    return $q.rejected();

                return $q(function (resolved, rejected) {
                    bazaarService.ad.answer({ idAd: idAd, idQuestion: idQuestion }, response,
                        function () {
                            resolved();
                        },
                        function (error) {
                            rejected(error.data.errors);
                        });
                });
            },
            sendAdPicture: function(blob) {
                if (!accountService.isAuthenticated)
                    return $q.rejected();

                return $q(function (resolved, rejected) {
                    bazaarService.image.sendAdPicture(blob,
                        function (data) {
                            resolved(data.hash);
                        },
                        function (error) {
                            rejected(error.data.errors);
                        });
                });
            },
            getCategories: function () {
                return $q(function (resolved, rejected) {
                    bazaarService.category.all({ },
                        function (data) {
                            resolved(data);
                        },
                        function (error) {
                            rejected(error);
                        });
                });
            }
        };

        return service;
    }
})();