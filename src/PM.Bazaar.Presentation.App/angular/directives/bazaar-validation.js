(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .directive("bazaarValidation", bazaar_validation);
    
    function bazaar_validation() {

        var directive = {
            link: link,
            require: "ngModel",
            restrict: "A"
        };

        return directive;

        function link(scope, element, attrs, model) {
            model.$validators.bazaarValidation =
                function () {
                    model.$setValidity("bazaarValidation", true);
                    return true;
                };
        }
    }

})();