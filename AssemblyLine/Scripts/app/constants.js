(function (window, angular) {
    'use strict';

    var module = angular.module('constants', []);

    module.constant('authEvents', {
        sessionTimeout: 'auth-session-timeout',
        notAuthenticated: 'auth-not-authenticated',
        notAuthorized: 'auth-not-authorized',
        authorize: 'auth-authorize'
    });

    module.constant('userRoles', {
        all: '*',
        admins: 'Administrators',
        sales: 'Sales',
        projectDirectors: 'ProjectDirectors',
        engineers: 'Engineers'
    });

})(window, window.angular);
