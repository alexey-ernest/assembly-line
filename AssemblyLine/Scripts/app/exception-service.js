(function(window, angular) {
    'use strict';

    var module = angular.module('services.exceptions', []);

    module.factory('$exceptionHandler', [
        function() {
            return function(exception) {

                var message = exception.message;
                if (message === '[object Object]') {
                    message = 'Unknown Error';
                }

                alert(message);
            };
        }
    ]);

})(window, window.angular);