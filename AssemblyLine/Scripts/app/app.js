(function(window, angular) {
    'use strict';

    var app = angular.module('assemblyLine', [
        'ui.router',
        'infinite-scroll',
        'constants',
        'directives',
        'services',
        'assemblyLine.home',
        'assemblyLine.projects',
        'assemblyLine.employees',
        'assemblyLine.vehicles',
        'assemblyLine.lines'
    ]);

    // Configurable Options
    app.constant('user', {
        id: null,
        roles: []
    });
    app.constant('loginUrl', '/account/login');

    // Third party libraries
    app.value('jQuery', window.$);

    // Config
    app.config([
        '$urlRouterProvider', '$locationProvider', '$stateProvider',
        function($urlRouterProvider, $locationProvider, $stateProvider) {

            // routes
            $stateProvider
                .state('app', {
                    'abstract': true,
                    template: '<div ui-view></div>'
                });

            // html5 routing without #
            $urlRouterProvider.otherwise('/');
            $locationProvider.html5Mode(true);
        }
    ]);

    // Main application controller
    app.controller('AppCtrl', [
        '$rootScope', 'authEvents', '$window', 'loginUrl', 'user', 'authService',
        function($rootScope, authEvents, $window, loginUrl, user, authService) {

            // GlOBAL SCOPE PROPERTIES
            $rootScope.fromState = null;
            $rootScope.fromStateParams = null;

            // SYSTEM EVENTS
            $rootScope.$on('$stateChangeStart', function(event, next) {
                var authorizedRoles = next.data.roles;
                if (!authService.isAuthorized(authorizedRoles)) {
                    event.preventDefault();
                    if (authService.isAuthenticated()) {
                        // user is not allowed
                        $rootScope.$broadcast(authEvents.notAuthorized);
                    } else {
                        // user is not logged in
                        $rootScope.$broadcast(authEvents.notAuthenticated);
                    }
                }
            });

            $rootScope.$on('$stateChangeSuccess', function(event, toState, toParams, from, fromParams) {
                if (angular.isDefined(toState.data) && angular.isDefined(toState.data.pageTitle)) {
                    $rootScope.pageTitle = toState.data.pageTitle + ' - Melnikov Assembly Line Application';
                }

                // maintaining state data
                $rootScope.fromState = from.name;
                $rootScope.fromStateParams = fromParams;
            });

            // AUTH EVENTS
            function gotoLogin() {
                $window.location.href = loginUrl;
            }

            $rootScope.$on(authEvents.authorize, function() {
                gotoLogin();
            });
            $rootScope.$on(authEvents.notAuthenticated, function() {
                gotoLogin();
            });
            $rootScope.$on(authEvents.notAuthorized, function() {
                gotoLogin();
            });
            $rootScope.$on(authEvents.sessionTimeout, function() {
                gotoLogin();
            });

        }
    ]);

})(window, window.angular);
