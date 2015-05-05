(function (window, angular) {
    'use strict';

    var module = angular.module('assemblyLine.vehicles', [
        'constants',
        'ui.router',
        'services'
    ]);

    // Routes
    module.config([
        '$stateProvider', 'userRoles', function ($stateProvider, userRoles) {
            $stateProvider
                .state('app.vehicles', {
                    url: '/vehicles',
                    templateUrl: 'vehicles.html',
                    controller: 'VehiclesCtrl',
                    data: {
                        pageTitle: 'Vehicles',
                        roles: [userRoles.all]
                    }
                })
                .state('app.vehicledetails', {
                    url: '/vehicles/{id:int}',
                    templateUrl: 'vehicles.details.html',
                    controller: 'VehicleDetailsCtrl',
                    data: {
                        pageTitle: 'Vehicle Details',
                        roles: [userRoles.all]
                    }
                })
                .state('app.vehiclecreate', {
                    url: '/vehicles/new',
                    templateUrl: 'vehicles.create.html',
                    controller: 'VehicleCreateCtrl',
                    data: {
                        pageTitle: 'New Vehicle',
                        roles: [userRoles.all]
                    }
                });
        }
    ]);

    // Controllers
    module.controller('VehiclesCtrl', [
        '$scope', '$state', 'vehicleService',
        function ($scope, $state, vehicleService) {

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
                return vehicleService.query($scope.filter)
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

    module.controller('VehicleDetailsCtrl', [
        '$scope', '$state', 'vehicleService', '$stateParams',
        function ($scope, $state, vehicleService, $stateParams) {

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
                    $state.go('^.vehicles');
                }, function (reason) {
                    throw new Error(reason);
                });
            }

            function load(id) {
                $scope.isLoading = true;
                vehicleService.get(id).then(function(data) {
                    $scope.item = data;
                    $scope.isLoading = false;
                }, function() {
                    $scope.isLoading = false;
                });
            }

            load($stateParams.id);
        }
    ]);

    module.controller('VehicleCreateCtrl', [
        '$scope', '$state', 'vehicleService',
        function ($scope, $state, vehicleService) {

            $scope.item = vehicleService.create({ name: 'New Vehicle' });
            $scope.isLoading = false;

            $scope.create = function (form, item) {
                if (form.$invalid) {
                    return;
                }

                item.$isLoading = true;
                item.$save().then(function () {
                    item.$isLoading = false;
                    $state.go('^.vehicles');
                }, function (reason) {
                    item.$isLoading = false;
                    throw new Error(reason);
                });

            };
        }
    ]);

})(window, window.angular);
