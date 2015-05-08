(function(window, angular) {
    'use strict';

    var module = angular.module('services.auth', []);

    module.factory('authService', [
        'user', 'userRoles', function(user, userRoles) {

            return {
                isAuthenticated: function() {
                    return !!user.id;
                },
                isAuthorized: function(authorizedRoles) {
                    if (!angular.isArray(authorizedRoles)) {
                        authorizedRoles = [authorizedRoles];
                    }
                    if (authorizedRoles.indexOf(userRoles.all) !== -1) {
                        return true;
                    }

                    var roles = user.roles || [];
                    if (!service.isAuthenticated() || !roles.length) {
                        return false;
                    }
                    
                    for (var i = 0; i < roles.length; i++) {
                        if (authorizedRoles.indexOf(roles[i]) === -1) {
                            continue;
                        }
                        return true;
                    }

                    return false;
                }
            };
        }
    ]);

    module.factory('authServiceInterceptor', [
        '$rootScope', '$q', 'authEvents', function($rootScope, $q, authEvents) {
            return {
                responseError: function(response) {
                    if (response.status === 401) {
                        $rootScope.$broadcast(authEvents.notAuthenticated, response);
                    }
                    if (response.status === 403) {
                        $rootScope.$broadcast(authEvents.notAuthorized, response);
                    }
                    if (response.status === 419 || response.status === 440) {
                        $rootScope.$broadcast(authEvents.sessionTimeout, response);
                    }

                    return $q.reject(response);
                }
            };
        }
    ]).config([
        "$httpProvider", function($httpProvider) {
            $httpProvider.interceptors.push('authServiceInterceptor');
        }
    ]);

})(window, window.angular);
