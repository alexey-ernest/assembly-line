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
                    'abstract': true,
                    template: '<div ui-view></div>',
                    controller: 'ProjectCreateCtrl',
                    data: {
                        pageTitle: 'New Employee at Melnikov Assembly Line Application',
                        roles: [userRoles.all]
                    }
                })
                .state('app.projectcreate.step1', {
                    url: '/employees/new/step1',
                    templateUrl: 'projects.create1.html',
                    controller: 'ProjectCreate1Ctrl',
                    data: {
                        pageTitle: 'New Employee at Melnikov Assembly Line Application',
                        roles: [userRoles.all]
                    }
                })
                .state('app.projectcreate.step2', {
                    url: '/employees/new/step2',
                    templateUrl: 'projects.create2.html',
                    controller: 'ProjectCreate2Ctrl',
                    data: {
                        pageTitle: 'New Employee at Melnikov Assembly Line Application',
                        roles: [userRoles.all]
                    }
                })
                .state('app.projectcreate.step3', {
                    url: '/employees/new/step3',
                    templateUrl: 'projects.create3.html',
                    controller: 'ProjectCreate3Ctrl',
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
            $scope.item = projectService.create({ name: 'New Project' });
        }
    ]);

    module.controller('ProjectCreate1Ctrl', [
        '$scope', '$state', 'projectService',
        function ($scope, $state, projectService) {

            $scope.next = function (form) {
                if (form.$invalid) {
                    return;
                }

                $state.go('^.projectcreate.step2');
            };

        }
    ]);

    module.controller('ProjectCreate2Ctrl', [
        '$scope', '$state', 'projectService',
        function ($scope, $state, projectService) {

            $scope.next = function (form) {
                if (form.$invalid) {
                    return;
                }

                $state.go('^.projectcreate.step3');
            };

            $scope.back = function (form) {
                if (form.$invalid) {
                    return;
                }

                $state.go('^.projectcreate.step1');
            };
        }
    ]);

    module.controller('ProjectCreate3Ctrl', [
       '$scope', '$state', 'projectService',
       function ($scope, $state, projectService) {

           $scope.isLoading = false;

           $scope.back = function (form) {
               if (form.$invalid) {
                   return;
               }

               $state.go('^.projectcreate.step2');
           };

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
       }
    ]);

})(window, window.angular);
