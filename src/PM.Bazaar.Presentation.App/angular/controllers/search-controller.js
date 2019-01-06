(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .controller("search-controller", controller);

    controller.$inject = ["$state", "advertising-service"];

    function controller($state, advertisingService) {
        /* jshint validthis:true */
        var vm = this;

        vm.searching = false;

        vm.filter = {
            maxPrice: 0,
            minPrice: 5,
            order: 'maxPrice',
            page: 1,
            pageSize: 20,
            keywordSearch: $state.params.q,
            categories: []
        };

        vm.ads = [];
        vm.categories = [];

        vm.applyFilter = function () {
            vm.searching = true;

            advertisingService.search(vm.filter)
                .then(function (ads) {
                    vm.ads = ads;
                })
                .finally(function() {
                    vm.searching = false;
                });
        };

        vm.addCategoryFilter = function(id) {
            var pos = vm.filter.categories.indexOf(id);

            pos >= 0 ? vm.filter.categories.splice(pos, 1) : vm.filter.categories.push(id);
        };

        (function getCategories() {
            advertisingService.getCategories()
                .then(function(categories) {
                    vm.categories = vm.categories.concat(categories);
                });
        })();

        if ($state.params.q && $state.params.q.length >= 3)
            vm.applyFilter();
    }
})();
