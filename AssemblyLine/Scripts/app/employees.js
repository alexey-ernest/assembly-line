﻿(function (window, angular) {
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
                        pageTitle: 'Employees at Melnikov Assembly Line Application',
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
            $scope.employees = [];
            $scope.employeesFilter = {
                orderBy: 'Created',
                orderByDesc: true,
                skip: 0,
                take: 15
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
                return employeeService.query($scope.employeesFilter)
                    .then(function (data) {
                        if (!$scope.employeesFilter.skip) {
                            $scope.employees = [];
                        }
                        $scope.employees = $scope.employees.concat(data);

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

})(window, window.angular);
