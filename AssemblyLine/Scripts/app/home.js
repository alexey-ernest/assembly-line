﻿(function (window, angular) {
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
                });
        }
    ]);

    // Controllers
    module.controller('HomeCtrl', [
        '$scope', '$state',
        function ($scope, $state) {

            
        }
    ]);

})(window, window.angular);
