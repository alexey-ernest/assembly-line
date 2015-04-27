(function (window, angular) {
    'use strict';

    var module = angular.module('assemblyLine.home', [
        'constants',
        'ui.router',
        'services'
    ]);

    // Routes
    module.config([
        '$stateProvider', 'userRoles', function ($stateProvider, userRoles) {
            $stateProvider
                .state('app.home', {
                    url: '/',
                    templateUrl: 'home.html',
                    controller: 'HomeCtrl',
                    data: {
                        pageTitle: 'Melnikov Assembly Line Application',
                        roles: [userRoles.all]
                    }
                })
                .state('app.home.lines', {
                    url: 'dashboard/lines',
                    templateUrl: 'home.lines.html',
                    controller: 'HomeLinesCtrl',
                    data: {
                        pageTitle: 'Assembly Lines Status on Melnikov Assembly Line Application',
                        roles: [userRoles.all]
                    }
                });
        }
    ]);

    // Controllers
    module.controller('HomeCtrl', [
        '$scope', '$state',
        function ($scope, $state) {

            
        }
    ]);

    module.controller('HomeLinesCtrl', [
        '$scope', '$state', 'projectService', 'projectLineService', 'dashboardService',
        function ($scope, $state, projectService, projectLineService, dashboardService) {

            $scope.projects = [];
            $scope.isLoading = false;

            $scope.milestoneStatuses = {
                0: 'NotStarted',
                1: 'InProgress',
                2: 'Cancelled',
                3: 'Completed'
            };

            function loadProjectStatuses() {
                $scope.isLoading = true;
                dashboardService.queryProjectStatuses({ active: true, orderBy: 'Created', orderByDesc: true }).then(function(data) {
                    $scope.projects = data;
                    $scope.isLoading = false;
                }, function() {
                    $scope.isLoading = false;
                    throw new Error('Could not load project statuses.');
                });
            }

            loadProjectStatuses();
        }
    ]);

})(window, window.angular);
