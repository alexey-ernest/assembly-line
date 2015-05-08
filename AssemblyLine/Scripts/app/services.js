(function(window, angular) {
    'use strict';

    var module = angular.module('services', [
        'services.auth',
        'services.exceptions',
        'ngResource'
    ]);

    function buildODataQueryParams(filter, customParams) {

        customParams = customParams || {};

        var params = {};

        // standard params
        if (filter.orderBy) {
            var order = filter.orderByDesc ? 'desc' : 'asc';
            params['$orderby'] = filter.orderBy + ' ' + order;
            delete filter.orderBy;
            delete filter.orderByDesc;
        }

        if (filter.skip) {
            params['$skip'] = filter.skip;
            delete filter.skip;
        }

        if (filter.take) {
            params['$top'] = filter.take;
            delete filter.take;
        }

        // custom params
        var filterParts = [];
        for (var param in customParams) {
            if (!filter.hasOwnProperty(param)) continue;
            filterParts.push(customParams[param].replace('%', filter[param]));
        }

        if (filterParts.length > 0) {
            var filterExpr = filterParts.join(' and ');
            params['$filter'] = filterExpr;
        }

        return params;
    }

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
                    return new resource(options);
                },
                get: function(id) {
                    var deferred = $q.defer();

                    resource.get({ id: id }, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                },
                query: function(filter) {

                    filter = filter || {};
                    var params = buildODataQueryParams(filter, { 'name': "substringof('%',FirstName) or substringof('%',LastName)" });

                    var deferred = $q.defer();
                    resource.query(params, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('vehicleService', [
        '$resource', '$q', function($resource, $q) {

            var url = '/api/vehicles';
            var resource = $resource(url + '/:id',
            { id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                create: function(options) {
                    return new resource(options);
                },
                get: function(id) {
                    var deferred = $q.defer();

                    resource.get({ id: id }, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                },
                query: function(filter) {

                    filter = filter || {};
                    var params = buildODataQueryParams(filter, { 'name': "substringof('%',Name)" });

                    var deferred = $q.defer();
                    resource.query(params, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('lineService', [
        '$resource', '$q', function($resource, $q) {

            var url = '/api/lines';
            var resource = $resource(url + '/:id',
            { id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                create: function(options) {
                    return new resource(options);
                },
                get: function(id) {
                    var deferred = $q.defer();

                    resource.get({ id: id }, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                },
                query: function(filter) {

                    filter = filter || {};
                    var params = buildODataQueryParams(filter, { 'name': "substringof('%',Name)" });

                    var deferred = $q.defer();
                    resource.query(params, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('projectService', [
        '$resource', '$q', function($resource, $q) {

            var url = '/api/projects';
            var resource = $resource(url + '/:id',
            { id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                create: function(options) {
                    return new resource(options);
                },
                get: function(id) {
                    var deferred = $q.defer();

                    resource.get({ id: id }, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                },
                query: function(filter) {

                    filter = filter || {};
                    var params = buildODataQueryParams(filter, { 'name': "substringof('%',Name)", 'active': "Status eq 'InProgress'" });

                    var deferred = $q.defer();
                    resource.query(params, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('projectLineService', [
        '$resource', '$q', function($resource, $q) {

            var resource = $resource('/api/projects/:pid/lines/:id',
            { pid: '@pid', id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                get: function(pid, id) {
                    var deferred = $q.defer();

                    resource.get({ pid: pid, id: id }, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                },
                query: function(pid, filter) {

                    filter = filter || {};
                    var params = buildODataQueryParams(filter);
                    params.pid = pid;

                    var deferred = $q.defer();
                    resource.query(params, function(data) {
                        deferred.resolve(data);
                    }, function() {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('projectCycleService', [
        '$resource', '$q', function($resource, $q) {

            var resource = $resource('/api/projects/:id/cycle', { id: '@id' });

            return {
                kickOff: function(id) {

                    var deferred = $q.defer();

                    resource.save({ id: id }, function() {
                        deferred.resolve();
                    }, function() {
                        deferred.reject();
                    });
                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('dashboardService', [
        '$resource', '$q', function($resource, $q) {

            return {
                queryProjectStatuses: function(filter) {

                    var resource = $resource('/api/dashboard/projects');

                    filter = filter || {};
                    var params = buildODataQueryParams(filter, { 'active': "Status eq 'InProgress'" });

                    var deferred = $q.defer();
                    resource.query(params, function(data) {
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
