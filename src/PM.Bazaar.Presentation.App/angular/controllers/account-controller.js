(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .controller("account-controller", controller);

    controller.$inject = ["$scope", "account-service", "$state"];

    function controller($scope, accountService, $state) {
        /* jshint validthis:true */
        var vm = this;

        vm.errors = [];
        vm.model = {};
        vm.service = accountService;

        vm.login = function () {
            vm.service.login(vm.model)
                .then(function () {
                    $state.go("home");
                })
                .catch(function(error) {
                    vm.form.$submitted = false;
                    vm.error = error;
                });
        };

        vm.register = function () {
            vm.service.register(vm.model)
                .then(function () {
                    vm.login(vm.model.email, vm.model.password);
                })
                .catch(function (error) {
                    vm.form.$submitted = false;
                    vm.errors = error;
                });
        };

        vm.openPicture = function () {
            angular.element("#pictureFile").click();
        };

        vm.changePassword = function () {
            vm.service.changePassword(vm.model)
                .then(function () {

                })
                .catch(function (error) {
                    vm.form.$submitted = false;
                    vm.errors = error;
                });
        };

        vm.changeAvatar = function () {
            if (typeof vm.model.newimage === "undefined")
                return;

            vm.service.changeAvatar(vm.model.newimage)
                .catch(function (error) {
                    vm.errors = error;
                });
        };

        function openModal(file) {
            var input = file.target;

            var reader = new FileReader();

            reader.onload = function () {
                angular.element("#croppmodal").modal("show");

                setTimeout(function () {
                        $scope.$apply(function () {
                            angular.element("#image").val(input.value);
                            vm.model.image = reader.result;
                        });
                    },
                    500);
            };

            reader.readAsDataURL(input.files[0]);

        };

        function configureInput() {
            var input = angular.element("#pictureFile");

            input.on("change", function (file) {
                if (file.target.files.length > 0)
                    openModal(file);
            });
        }

        configureInput();
    }
})();
