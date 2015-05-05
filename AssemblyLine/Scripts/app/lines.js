(function (window, angular) {
    'use strict';

    var module = angular.module('assemblyLine.lines', [
        'constants',
        'ui.router',
        'services'
    ]);

    // Routes
    module.config([
        '$stateProvider', 'userRoles', function ($stateProvider, userRoles) {
            $stateProvider
                .state('app.lines', {
                    url: '/lines',
                    templateUrl: 'lines.html',
                    controller: 'LinesCtrl',
                    data: {
                        pageTitle: 'Assembly Lines',
                        roles: [userRoles.all]
                    }
                })
                .state('app.linedetails', {
                    url: '/lines/{id:int}',
                    templateUrl: 'lines.details.html',
                    controller: 'LineDetailsCtrl',
                    data: {
                        pageTitle: 'Assembly Line Details',
                        roles: [userRoles.all]
                    }
                })
                .state('app.linecreate', {
                    url: '/lines/new',
                    templateUrl: 'lines.create.html',
                    controller: 'LineCreateCtrl',
                    data: {
                        pageTitle: 'New Assembly Line',
                        roles: [userRoles.all]
                    }
                });
        }
    ]);

    // Controllers
    module.controller('LinesCtrl', [
        '$scope', '$state', 'lineService',
        function ($scope, $state, lineService) {

            // PROPERTIES
            $scope.items = [];
            $scope.filter = {
                orderBy: 'Name',
                orderByDesc: false,
                skip: 0,
                take: 20
            };

            $scope.isLoading = false;
            $scope.isAllLoaded = false;

            function filterItems() {

                $scope.isLoading = true;
                return lineService.query($scope.filter)
                    .then(function (data) {
                        if (data.length < $scope.filter.take) {
                            $scope.isAllLoaded = true;
                        } else {
                            $scope.isAllLoaded = false;
                        }

                        if (!$scope.filter.skip) {
                            $scope.items = [];
                        }
                        $scope.items = $scope.items.concat(data);

                        $scope.isLoading = false;
                    }, function () {
                        $scope.isLoading = false;
                        throw new Error();
                    });
            };

            $scope.nextPage = function () {
                if ($scope.isAllLoaded || $scope.isLoading) {
                    return;
                }

                $scope.filter.skip += $scope.filter.take;
                filterItems();
            };

            function load() {
                $scope.isLoading = true;
                filterItems();
            }

            // INIT
            load();
        }
    ]);

    module.controller('LineDetailsCtrl', [
        '$scope', '$state', 'lineService', '$stateParams',
        function ($scope, $state, lineService, $stateParams) {

            $scope.item = null;
            $scope.isLoading = false;

            $scope.update = function (form, item) {
                if (form.$invalid) {
                    return;
                }

                item.$isLoading = true;
                item.$update().then(function () {
                    item.$isLoading = false;
                }, function (reason) {
                    item.$isLoading = false;
                    throw new Error(reason);
                });

            };
            
            $scope.delete = function (item) {
                item.$delete().then(function () {
                    $state.go('^.lines');
                }, function (reason) {
                    throw new Error(reason);
                });
            }

            function load(id) {
                $scope.isLoading = true;
                lineService.get(id).then(function(data) {
                    $scope.item = data;
                    $scope.isLoading = false;
                }, function() {
                    $scope.isLoading = false;
                });
            }

            load($stateParams.id);
        }
    ]);

    module.controller('LineCreateCtrl', [
        '$scope', '$state', 'lineService',
        function ($scope, $state, lineService) {

            $scope.item = lineService.create({ name: 'New Assembly Line' });
            $scope.isLoading = false;

            $scope.create = function (form, item) {
                if (form.$invalid) {
                    return;
                }

                item.$isLoading = true;
                item.$save().then(function () {
                    item.$isLoading = false;
                    $state.go('^.lines');
                }, function (reason) {
                    item.$isLoading = false;
                    throw new Error(reason);
                });

            };
        }
    ]);

})(window, window.angular);
