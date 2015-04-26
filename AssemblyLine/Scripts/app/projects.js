﻿(function (window, angular) {
    'use strict';

    var module = angular.module('assemblyLine.projects', [
        'constants',
        'ui.router',
        'services'
    ]);

    // Routes
    module.config([
        '$stateProvider', 'userRoles', function ($stateProvider, userRoles) {
            $stateProvider
                .state('app.projects', {
                    url: '/projects',
                    templateUrl: 'projects.html',
                    controller: 'ProjectsCtrl',
                    data: {
                        pageTitle: 'Projects at Melnikov Assembly Line Application',
                        roles: [userRoles.all]
                    }
                })
                .state('app.projectdetails', {
                    url: '/projects/{id:int}',
                    templateUrl: 'projects.details.html',
                    controller: 'ProjectDetailsCtrl',
                    data: {
                        pageTitle: 'Project Details at Melnikov Assembly Line Application',
                        roles: [userRoles.all]
                    }
                })
                .state('app.projectcreate', {
                    url: '/projects/new',
                    templateUrl: 'projects.create1.html',
                    controller: 'ProjectCreateCtrl',
                    data: {
                        pageTitle: 'New Employee at Melnikov Assembly Line Application',
                        roles: [userRoles.all]
                    }
                });
        }
    ]);

    // Controllers
    module.controller('ProjectsCtrl', [
        '$scope', '$state', 'projectService',
        function ($scope, $state, projectService) {

            // PROPERTIES
            $scope.items = [];
            $scope.filter = {
                orderBy: 'Created',
                orderByDesc: true,
                skip: 0,
                take: 20
            };

            $scope.isLoading = false;
            $scope.isAllLoaded = false;

            // PRIVATE METHODS
            function filterItems() {

                $scope.isLoading = true;
                return projectService.query($scope.filter)
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
                    });
            };

            function load() {
                $scope.isLoading = true;
                filterItems();
            }

            // METHODS
            $scope.nextPage = function () {
                if ($scope.isAllLoaded || $scope.isLoading) {
                    return;
                }

                $scope.filter.skip += $scope.filter.take;
                filterItems();
            };

            // INIT
            load();
        }
    ]);

    module.controller('ProjectDetailsCtrl', [
        '$scope', '$state', 'projectService', '$stateParams',
        function ($scope, $state, projectService, $stateParams) {

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
                    $state.go('^.projects');
                }, function (reason) {
                    throw new Error(reason);
                });
            }

            function load(id) {
                $scope.isLoading = true;
                projectService.get(id).then(function (data) {
                    $scope.item = data;
                    $scope.isLoading = false;
                }, function() {
                    $scope.isLoading = false;
                });
            }

            load($stateParams.id);
        }
    ]);

    module.controller('ProjectCreateCtrl', [
        '$scope', '$state', 'projectService',
        function ($scope, $state, projectService) {
            
        }
    ]);

    module.controller('ProjectCreate1Ctrl', [
        '$scope', '$state', 'projectService', 'vehicleService',
        function ($scope, $state, projectService, vehicleService) {

            $scope.item = projectService.create({ name: 'New Project' });
            $scope.vehicles = [];
            $scope.assemblyLinesNumber = 0;

            $scope.isLading = false;

            function initVehicle(item, vehicles) {
                if (item == null || item.vehicle == null) {
                    return;
                }

                for (var i = 0; i < vehicles.length; i++) {

                    if (item.vehicle.id !== vehicles[i].id) {
                        continue;
                    }

                    item.vehicle = vehicles[i];
                    break;
                }
            }

            function loadVehicles() {
                $scope.isLading = true;
                vehicleService.query().then(function (data) {
                    $scope.vehicles = data;
                    initVehicle($scope.item, data);
                    $scope.isLading = false;
                }, function () {
                    $scope.isLading = false;
                    throw new Error('Could not load vehicles');
                });
            }

            $scope.create = function (form, item) {
                if (form.$invalid) {
                    return;
                }

                item.$isLoading = true;
                item.$save().then(function () {
                    item.$isLoading = false;
                    $state.go('^.projects');
                }, function (reason) {
                    item.$isLoading = false;
                    throw new Error(reason);
                });

            };

            loadVehicles();
        }
    ]);

})(window, window.angular);
