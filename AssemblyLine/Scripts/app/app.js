(function (window, angular) {
    'use strict';

    var module = angular.module('assemblyLine', [
        'constants',
        'ui.router',
        'assemblyLine.home',
        'assemblyLine.projects',
        'assemblyLine.employees'
    ]);

    // Third party libraries
    module.constant('jQuery', window.$);

    // Config
    module.config([
        '$urlRouterProvider', '$locationProvider', function ($urlRouterProvider, $locationProvider) {
            // html5 routing without #
            $urlRouterProvider.otherwise('/');
            $locationProvider.html5Mode(true);
        }
    ]);

    // Routes
    module.config([
        '$stateProvider', function ($stateProvider) {
            $stateProvider
                .state('app', {
                    "abstract": true,
                    template: '<div ui-view></div>'
                });
        }
    ]);

    // Main application controller
    module.controller('AppCtrl', [
        '$rootScope', '$state', '$timeout',
        function ($rootScope, $state, $timeout) {

            // GlOBAL SCOPE PROPERTIES
            $rootScope.fromState = null;
            $rootScope.fromStateParams = null;

            // SYSTEM EVENTS
            $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, from, fromParams) {
                if (angular.isDefined(toState.data) && angular.isDefined(toState.data.pageTitle)) {
                    $rootScope.pageTitle = toState.data.pageTitle;
                }

                // maintaining state transition data
                $rootScope.fromState = from.name;
                $rootScope.fromStateParams = fromParams;
            });


            $timeout(function() {
                $state.go('app.home');
            });

        }
    ]);

})(window, window.angular);
