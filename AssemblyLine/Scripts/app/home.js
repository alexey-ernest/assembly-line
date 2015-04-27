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
        '$scope', '$state', 'projectService', 'projectLineService',
        function ($scope, $state, projectService, projectLineService) {

            $scope.projects = [];
            $scope.lines = {};

            $scope.milestones = [];
            $scope.milestoneStatuses = {
                0: 'NotStarted',
                1: 'InProgress',
                2: 'Cancelled',
                3: 'Completed'
            };

            function loadProjectLines(id) {
                projectLineService.query(id, { orderBy: 'Name' }).then(function(data) {
                    $scope.lines[id] = data;
                    if (!$scope.milestones.length && data.length > 0) {
                        $scope.milestones = data[0].milestones;
                    }
                }, function() {
                    throw new Error('Could not load assembly lines.');
                });
            }

            function loadLines(projects) {
                for (var i = 0; i < projects.length; i++) {
                    loadProjectLines(projects[i].id);
                }
            }

            function loadActiveProjects() {
                projectService.query({ active: true }).then(function(data) {
                    $scope.projects = data;
                    loadLines(data);
                }, function() {
                    throw new Error('Could not load projects.');
                });
            }

            loadActiveProjects();
        }
    ]);

})(window, window.angular);
