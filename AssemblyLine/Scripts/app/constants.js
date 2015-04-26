(function (window, angular) {
    'use strict';

    var module = angular.module('constants', []);

    module.constant('userRoles', {
        all: '*',
        admin: 'Administrator',
        projectDirector: 'ProjectDirector',
        teamManager: 'TeamManager',
        engineer: 'Engineer'
    });

})(window, window.angular);