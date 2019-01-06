(function () {
    "use strict";

    angular.module("bazaar-app")
        .factory("bazaar-service", bazaar_service);

    bazaar_service.$inject = ["$resource"];

    function bazaar_service($resource) {
        var baseUrl = "/api/";

        var factory = {
            ad: $resource("",
                {},
                {
                    byId: {
                        url: baseUrl + "ad/:id",
                        method: "GET",
                        isArray: false
                    },

                    search: {
                        url: baseUrl + "ad/search",
                        method: "GET",
                        isArray: true
                    },

                    getMostRecent: {
                        url: baseUrl + "ad/most-recent/:number",
                        method: "GET",
                        isArray: true
                    },

                    publish: {
                        url: baseUrl + "ad/",
                        method: "POST"
                    },

                    ask: {
                        url: baseUrl + "ad/:idAd/ask",
                        method: "POST"
                    },

                    answer: {
                        url: baseUrl + "ad/:idAd/question/:idQuestion/answer",
                        method: "POST"
                    }
                }
            ),

            category: $resource("",
                {},
                {
                    all: {
                        url: baseUrl + "category",
                        method: "GET",
                        isArray: true
                    }
                }
            ),

            image: $resource("",
                {},
                {
                    sendAdPicture: {
                        url: baseUrl + "image/upload-picture-ad",
                        method: "POST",
                        headers: { 'Content-Type': undefined },
                        transformRequest: function (file) {
                            var form = new FormData();
                            form.append(file.name, file);

                            return form;
                        },
                        transformResponse: function (data, headers, status) {
                            if (status !== 200)
                                return angular.fromJson(data);

                            return {
                                hash: data.replace(/[\"]/g, "")
                            };
                        }
                    },

                    sendAvatar: {
                        url: baseUrl + "image/upload-avatar",
                        method: "POST",
                        headers: { 'Content-Type': undefined },
                        transformRequest: function (blob) {
                            var form = new FormData();
                            form.append("file", blob);

                            return form;
                        },
                        transformResponse: function (data, headers, status) {
                            if (status !== 200)
                                return angular.fromJson(data);

                            return  {
                                hash : data.replace(/[\"]/g, "")
                            };
                        }
                    }
                }
            ),

            account: $resource("",
                {},
                {
                    me: {
                        url: baseUrl + "account/me",
                        method: "GET",
                        isArray: false
                    },

                    login: {
                        url: "../oauth/token",
                        method: "POST",
                        headers: { 'Content-Type': "application/x-www-form-urlencoded" }
                    },

                    register: {
                        url: baseUrl + "account/register",
                        method: "POST"
                    },

                    changePassword: {
                        url: baseUrl + "account/change-password",
                        method: "POST"
                    },

                    changeAvatar: {
                        url: baseUrl + "account/change-avatar",
                        method: "POST",
                        headers: { 'Content-Type': "application/x-www-form-urlencoded" }
                    }
                }
            )
        };

        return factory;
    }
})();