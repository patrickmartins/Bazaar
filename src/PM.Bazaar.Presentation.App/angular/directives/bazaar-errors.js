(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .directive("bazaarErrors", bazaar_errors);

    function bazaar_errors() {

        var directive = {
            restrict: "A",
            scope: {
                bazaarErrors: "="
            },
            controller: ["$scope", controller]
        };
        return directive;

        function controller(scope) {
            this.scope = scope;
        }
    }
})();