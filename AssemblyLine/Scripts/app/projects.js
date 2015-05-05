(function (window, angular) {
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
                        pageTitle: 'Projects',
                        roles: [userRoles.all]
                    }
                })
                .state('app.projectdetails', {
                    url: '/projects/{id:int}',
                    templateUrl: 'projects.details.html',
                    controller: 'ProjectDetailsCtrl',
                    data: {
                        pageTitle: 'Project Details',
                        roles: [userRoles.all]
                    }
                })
                .state('app.projectline', {
                    url: '/projects/{pid:int}/lines/{id:int}',
                    templateUrl: 'projects.line.html',
                    controller: 'ProjectLineCtrl',
                    data: {
                        pageTitle: 'Project Assembly Line',
                        roles: [userRoles.all]
                    }
                })
                .state('app.projectcreate', {
                    url: '/projects/new',
                    templateUrl: 'projects.create.html',
                    controller: 'ProjectCreateCtrl',
                    data: {
                        pageTitle: 'New Project',
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

            $scope.statuses = {
                0: 'New',
                1: 'InProgress',
                2: 'Cancelled',
                3: 'Completed'
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
        '$scope', '$state', 'projectService', '$stateParams', 'vehicleService', 'projectLineService', 'projectCycleService',
        function ($scope, $state, projectService, $stateParams, vehicleService, projectLineService,projectCycleService) {

            $scope.item = null;
            $scope.vehicles = [];
            $scope.lines = [];
            $scope.isLoading = false;

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
                    initVehicle($scope.item, $scope.vehicles);
                    $scope.isLading = false;
                }, function () {
                    $scope.isLading = false;
                    throw new Error('Could not load vehicles');
                });
            }

            function loadLines(pid) {
                projectLineService.query(pid).then(function (data) {
                    $scope.lines = data;
                }, function() {
                    throw new Error('Could not load project assembly lines');
                });
            }

            $scope.update = function (form, item) {
                if (form.$invalid) {
                    return;
                }

                item.$isLoading = true;
                item.$update().then(function () {
                    initVehicle(item, $scope.vehicles);
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

            $scope.start = function(item) {
                projectCycleService.kickOff(item.id).then(function() {
                    $state.go('^.projects');
                }, function() {
                    throw new Error('Could not start the project.');
                });
            }

            function load(id) {
                $scope.isLoading = true;
                projectService.get(id).then(function (data) {
                    $scope.item = data;
                    initVehicle($scope.item, $scope.vehicles);
                    $scope.isLoading = false;
                }, function() {
                    $scope.isLoading = false;
                });
            }

            load($stateParams.id);
            loadVehicles();
            loadLines($stateParams.id);
        }
    ]);

    module.controller('ProjectLineCtrl', [
        '$scope', '$state', '$stateParams', 'projectLineService', 'employeeService',
        function ($scope, $state, $stateParams, projectLineService, employeeService) {

            $scope.item = null;
            $scope.pid = $stateParams.pid;
            $scope.employees = [];
            $scope.isLoading = false;

            function initTeam(team, employees) {
                if (team == null || employees == null) {
                    return;
                }

                if (team.manager) {
                    for (var i = 0; i < employees.length; i++) {
                        if (team.manager.id !== employees[i].id) {
                            continue;
                        }
                        team.manager = employees[i];
                        break;
                    }
                }

                for (var j = 0; j < team.engineers.length; j++) {
                    for (var k = 0; k < employees.length; k++) {
                        if (team.engineers[j].id !== employees[k].id) {
                            continue;
                        }
                        team.engineers[j] = employees[k];
                        break;
                    }
                }
            }

            $scope.update = function (form, item) {
                if (form.$invalid) {
                    return;
                }

                item.$isLoading = true;

                item.$update({ pid: $scope.pid }).then(function () {
                    item.$isLoading = false;

                    $scope.item = item;
                    initTeam(item.productionTeam, $scope.employees);
                    initTeam(item.procurementTeam, $scope.employees);

                }, function (reason) {
                    item.$isLoading = false;
                    throw new Error(reason);
                });

            };

            $scope.addProductionEngineer = function() {
                $scope.item.productionTeam.engineers.push({});
            }

            $scope.addProcurementTeamEngineer = function () {
                $scope.item.procurementTeam.engineers.push({});
            }

            function load(pid, id) {
                $scope.isLoading = true;
                projectLineService.get(pid, id).then(function (data) {
                    $scope.item = data;
                    if (!data.productionTeam) {
                        data.productionTeam = { engineers: [] };
                    } else {
                        initTeam(data.productionTeam, $scope.employees);
                    }
                    if (!data.procurementTeam) {
                        data.procurementTeam = { engineers: [] };
                    } else {
                        initTeam(data.procurementTeam, $scope.employees);
                    }

                    $scope.isLoading = false;
                }, function () {
                    $scope.isLoading = false;
                });
            }

            function loadEmployees() {
                employeeService.query().then(function(data) {
                    for (var i = 0; i < data.length; i++) {
                        data[i].displayName = data[i].firstName + ' ' + data[i].lastName;
                        if (data[i].post) {
                            data[i].displayName += ' (' + data[i].post + ')';
                        }
                    }
                    $scope.employees = data;
                    if ($scope.item) {
                        initTeam($scope.item.productionTeam, $scope.employees);
                        initTeam($scope.item.procurementTeam, $scope.employees);
                    }
                }, function() {
                    throw new Error('Could not load employees.');
                });
            }

            load($stateParams.pid, $stateParams.id);
            loadEmployees();
        }
    ]);

    module.controller('ProjectCreateCtrl', [
        '$scope', '$state', 'projectService', 'vehicleService',
        function ($scope, $state, projectService, vehicleService) {

            $scope.item = projectService.create({ name: 'New Project', vehicleNumber: 1, assemblyLinesNumber: 1 });
            $scope.vehicles = [];

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
