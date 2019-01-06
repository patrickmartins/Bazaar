(function () {
    "use strict";

    angular
        .module("bazaar-app")
        .controller("advertising-controller", controller);

    controller.$inject = ["$anchorScroll", "$state", "$scope", "advertising-service", "account-service"];

    function controller($anchorScroll, $state, $scope, advertisingService, accountService) {
        /* jshint validthis:true */
        var vm = this;

        vm.model = {};
        vm.errors = [];

        vm.pictures = [];
        vm.categories = [];

        vm.question = {};
        vm.response = {};
        
        vm.accountService = accountService;

        vm.openPicture = function () {
            angular.element("#pictureFile").click();
        };

        vm.publish = function() {
            advertisingService.publish(vm.model)
                .then(function() {
                    $state.go("login");
                })
                .catch(function (error) {
                    vm.form.$submitted = false;
                    vm.errors = error;
                });
        };

        vm.ask = function (form) {
            advertisingService.ask($state.params.id, vm.question)
                .then(function () {
                    vm.question.date = (new Date()).toISOString();
                    vm.question.user = vm.accountService.user;

                    vm.model.questions.push(vm.question);

                    vm.question = {};

                    form.$setPristine();
                })
                .catch(function (error) {
                    vm.errors = error;
                })
                .finally(function () {
                    form.$submitted = false;
                });
        };

        vm.answer = function (form, question) {
            advertisingService.answer($state.params.id, question.id, vm.response)
                .then(function () {
                    vm.response.date = (new Date()).toISOString();
                    question.response = vm.response;

                    vm.response = {};
                })
                .catch(function (error) {
                    vm.errors = error;
                })
                .finally(function () {
                    form.$submitted = false;
                });
        };

        vm.openFormAnswer = function(question) {
            angular.forEach(vm.model.questions,
                function(q) {
                    q.answering = q.id === question.id;
                });
        };

        vm.clickAsk = function () {
            if (accountService.isAuthenticated) {
                angular.element("#description").focus();

                $anchorScroll.yOffset = 400;
                $anchorScroll("description");
            }
            else
                $state.go("login");
        };

        function loadAd(id) {
            advertisingService.getById(id)
                .then(function (ad) {
                    vm.model = ad;
                });
        }

        function configureInput() {
            var input = angular.element("#pictureFile");

            input.on("change", function (file) {
                if (file.target.files.length > 0) 
                    if ((file.target.files[0].size / 1024) / 1024 <= 2)
                        addPicture(file);
                    else
                        $scope.$apply(function() {
                            vm.errors = [{ description: 'O arquivo não deve ultrapassar 2 MB', source: 'Image' }];
                        });
            });
        }

        function getCategories() {
            advertisingService.getCategories()
                .then(function (categories) {
                    vm.categories = categories;
                });
        }
        
        function addPicture(file) {
            var input = file.target;

            if (input.files.length === 0)
                return;

            var reader = new FileReader();

            reader.onload = function () {
                var picture = { url: reader.result, loaded: false };

                $scope.$apply(function () {
                    vm.pictures.push(picture);
                });

                advertisingService.sendAdPicture(input.files[0])
                    .then(function(hash) {
                        if (!vm.model.pictures)
                            vm.model.pictures = [];

                        vm.model.pictures.push(hash);
                        picture.loaded = true;
                    })
                    .catch(function (error) {
                        vm.pictures.splice(vm.pictures.indexOf(picture), 1);
                        vm.errors = error;
                    });
            };

            reader.readAsDataURL(input.files[0]);
        }

        if ($state.params.id && parseInt($state.params.id) > 0)
            loadAd($state.params.id);
        else
        {
            configureInput();
            getCategories();
        }
    }
})();
