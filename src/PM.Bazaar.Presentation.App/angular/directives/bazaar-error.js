(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .directive("bazaarError", bazaar_error);

    function bazaar_error() {

        var directive = {
            link: link,
            restrict: "A",
            require: "^bazaarErrors",
            scope: {
                bazaarError: "="
            }
        };
        return directive;

        function link(scope, element, attr, outer) {
            outer.scope.$watch("bazaarErrors",
                function (errors) {
                    angular.forEach(errors,
                        function (error) {
                            if (error.source.toUpperCase() === scope.bazaarError.$name.toUpperCase()) {
                                element[0].textContent = error.description;
                                scope.bazaarError.$setValidity("bazaarValidation", false);
                            }
                        });
                });

            scope.$watch("bazaarError.$error.bazaarValidation",
                function (error) {
                    error ? element.show() : element.hide();
                });
        }
    }

})();