(function(window, angular) {
    'use strict';

    var module = angular.module('services', ['ngResource']);

    module.factory('employeeService', [
        '$resource', '$q', function($resource, $q) {

            var url = '/api/employees';
            var resource = $resource(url + '/:id',
            { id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                create: function(options) {
                    return new resource({ firstName: options.firstName, lastName: options.lastName });
                },
                get: function (id) {
                    var deferred = $q.defer();

                    resource.get({ id: id }, function (data) {
                        deferred.resolve(data);
                    }, function () {
                        deferred.reject();
                    });

                    return deferred.promise;
                },
                query: function(filter) {

                    // OData params
                    var params = {};

                    var filterExpr = null;
                    var filterParts = [];

                    if (filter.name != null && filter.name !== '') {
                        filterParts.push("substringof('" + filter.name + "',FirstName) or substringof('" + filter.name  + "',LastName)");
                    }
                    if (filterParts.length > 0) {
                        filterExpr = filterParts.join(' and ');
                    }
                    if (filterExpr) {
                        params['$filter'] = filterExpr;
                    }

                    if (filter.orderBy !== null && filter.orderBy !== '') {
                        var order = filter.orderByDesc ? 'desc' : 'asc';
                        params['$orderby'] = filter.orderBy + ' ' + order;
                    }

                    if (filter.skip !== null && filter.skip !== '') {
                        params['$skip'] = filter.skip;
                    }

                    if (filter.take != null && filter.take !== '') {
                        params['$top'] = filter.take;
                    }

                    var deferred = $q.defer();

                    resource.query(params, function (data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

})(window, window.angular);
