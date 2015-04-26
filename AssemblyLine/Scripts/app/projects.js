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
                        pageTitle: 'Projects at Assembly Line Application',
                        roles: [userRoles.all]
                    }
                });
        }
    ]);

    // Controllers
    module.controller('ProjectsCtrl', [
        '$scope', '$state', 
        function ($scope, $state) {

            
        }
    ]);

})(window, window.angular);
