(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .directive("croppie", croppie);

    function croppie() {
        var directive = {
            restrict: "AE",
            scope: {
                src: "=",
                ngModel: "=",
                options: "<"
            },

            link: link
        };
        return directive;

        function link(scope, element) {
            var options = angular.extend({
                    viewport: {
                        width: 200,
                        height: 200
                    }
                },
                scope.options);

            options.update = function () {
                c.result("blob").then(function (img) {
                    scope.$apply(function () {
                        scope.ngModel = img;
                    });
                });
            };

            var c = new Croppie(element[0], options);

            if (typeof scope.src !== "undefined")
                c.bind({
                    url: scope.src
                });

            scope.$watch("src",
                function (newValue) {
                    if (newValue) {
                        c.bind({
                            url: newValue
                        });
                    }
                });
        }
    }
})();