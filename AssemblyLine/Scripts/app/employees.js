(function (window, angular) {
    'use strict';

    var module = angular.module('assemblyLine.employees', [
        'constants',
        'ui.router',
        'services'
    ]);

    // Routes
    module.config([
        '$stateProvider', 'userRoles', function ($stateProvider, userRoles) {
            $stateProvider
                .state('app.employees', {
                    url: '/employees',
                    templateUrl: 'employees.html',
                    controller: 'EmployeesCtrl',
                    data: {
                        pageTitle: 'Employees',
                        roles: [userRoles.all]
                    }
                })
                .state('app.employeedetails', {
                    url: '/employees/{id:int}',
                    templateUrl: 'employees.details.html',
                    controller: 'EmployeeDetailsCtrl',
                    data: {
                        pageTitle: 'Employee Details',
                        roles: [userRoles.all]
                    }
                })
                .state('app.employeecreate', {
                    url: '/employees/new',
                    templateUrl: 'employees.create.html',
                    controller: 'EmployeeCreateCtrl',
                    data: {
                        pageTitle: 'New Employee',
                        roles: [userRoles.all]
                    }
                });
        }
    ]);

    // Controllers
    module.controller('EmployeesCtrl', [
        '$scope', '$state', 'employeeService',
        function ($scope, $state, employeeService) {

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

            // METHODS
            $scope.nextPage = function () {
                if ($scope.isAllLoaded || $scope.isLoading) {
                    return;
                }

                $scope.filter.skip += $scope.filter.take;
                filterItems();
            };


            // PRIVATE METHODS
            function filterItems() {

                $scope.isLoading = true;
                return employeeService.query($scope.filter)
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
                    }, function() {
                        $scope.isLoading = false;
                    });
            };

            function load() {
                $scope.isLoading = true;
                filterItems();
            }

            // INIT
            load();
        }
    ]);

    module.controller('EmployeeDetailsCtrl', [
        '$scope', '$state', 'employeeService', '$stateParams',
        function ($scope, $state, employeeService, $stateParams) {

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
                    $state.go('^.employees');
                }, function (reason) {
                    throw new Error(reason);
                });
            }

            function load(id) {
                $scope.isLoading = true;
                employeeService.get(id).then(function(data) {
                    $scope.item = data;
                    $scope.isLoading = false;
                }, function() {
                    $scope.isLoading = false;
                });
            }

            load($stateParams.id);
        }
    ]);

    module.controller('EmployeeCreateCtrl', [
        '$scope', '$state', 'employeeService',
        function ($scope, $state, employeeService) {

            $scope.item = employeeService.create({ firstName: 'New Employee', lastName: 'Smith' });
            $scope.isLoading = false;

            $scope.create = function (form, item) {
                if (form.$invalid) {
                    return;
                }

                item.$isLoading = true;
                item.$save().then(function () {
                    item.$isLoading = false;
                    $state.go('^.employees');
                }, function (reason) {
                    item.$isLoading = false;
                    throw new Error(reason);
                });

            };
        }
    ]);

})(window, window.angular);
