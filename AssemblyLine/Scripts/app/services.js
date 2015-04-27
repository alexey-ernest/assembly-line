(function(window, angular) {
    'use strict';

    var module = angular.module('services', ['ngResource']);

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

                    filter = filter || {};

                    // OData params
                    var params = {};

                    var filterExpr = null;
                    var filterParts = [];

                    if (filter.name) {
                        filterParts.push("substringof('" + filter.name + "',FirstName) or substringof('" + filter.name  + "',LastName)");
                    }
                    if (filterParts.length > 0) {
                        filterExpr = filterParts.join(' and ');
                    }
                    if (filterExpr) {
                        params['$filter'] = filterExpr;
                    }

                    if (filter.orderBy) {
                        var order = filter.orderByDesc ? 'desc' : 'asc';
                        params['$orderby'] = filter.orderBy + ' ' + order;
                    }

                    if (filter.skip) {
                        params['$skip'] = filter.skip;
                    }

                    if (filter.take) {
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

    module.factory('vehicleService', [
        '$resource', '$q', function ($resource, $q) {

            var url = '/api/vehicles';
            var resource = $resource(url + '/:id',
            { id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                create: function (options) {
                    return new resource(options);
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
                query: function (filter) {

                    filter = filter || {};

                    // OData params
                    var params = {};

                    var filterExpr = null;
                    var filterParts = [];

                    if (filter.name) {
                        filterParts.push("substringof('" + filter.name + "',Name)");
                    }
                    if (filterParts.length > 0) {
                        filterExpr = filterParts.join(' and ');
                    }
                    if (filterExpr) {
                        params['$filter'] = filterExpr;
                    }

                    if (filter.orderBy) {
                        var order = filter.orderByDesc ? 'desc' : 'asc';
                        params['$orderby'] = filter.orderBy + ' ' + order;
                    }

                    if (filter.skip) {
                        params['$skip'] = filter.skip;
                    }

                    if (filter.take) {
                        params['$top'] = filter.take;
                    }

                    var deferred = $q.defer();

                    resource.query(params, function (data) {
                        deferred.resolve(data);
                    }, function () {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('lineService', [
        '$resource', '$q', function ($resource, $q) {

            var url = '/api/lines';
            var resource = $resource(url + '/:id',
            { id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                create: function (options) {
                    return new resource(options);
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
                query: function (filter) {

                    filter = filter || {};

                    // OData params
                    var params = {};

                    var filterExpr = null;
                    var filterParts = [];

                    if (filter.name) {
                        filterParts.push("substringof('" + filter.name + "',Name)");
                    }
                    if (filterParts.length > 0) {
                        filterExpr = filterParts.join(' and ');
                    }
                    if (filterExpr) {
                        params['$filter'] = filterExpr;
                    }

                    if (filter.orderBy) {
                        var order = filter.orderByDesc ? 'desc' : 'asc';
                        params['$orderby'] = filter.orderBy + ' ' + order;
                    }

                    if (filter.skip) {
                        params['$skip'] = filter.skip;
                    }

                    if (filter.take) {
                        params['$top'] = filter.take;
                    }

                    var deferred = $q.defer();

                    resource.query(params, function (data) {
                        deferred.resolve(data);
                    }, function () {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('projectService', [
        '$resource', '$q', function ($resource, $q) {

            var url = '/api/projects';
            var resource = $resource(url + '/:id',
            { id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                create: function (options) {
                    return new resource(options);
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
                query: function (filter) {

                    filter = filter || {};

                    // OData params
                    var params = {};

                    var filterExpr = null;
                    var filterParts = [];

                    if (filter.name) {
                        filterParts.push("substringof('" + filter.name + "',Name)");
                    }
                    if (filterParts.length > 0) {
                        filterExpr = filterParts.join(' and ');
                    }
                    if (filterExpr) {
                        params['$filter'] = filterExpr;
                    }

                    if (filter.orderBy) {
                        var order = filter.orderByDesc ? 'desc' : 'asc';
                        params['$orderby'] = filter.orderBy + ' ' + order;
                    }

                    if (filter.skip) {
                        params['$skip'] = filter.skip;
                    }

                    if (filter.take) {
                        params['$top'] = filter.take;
                    }

                    var deferred = $q.defer();

                    resource.query(params, function (data) {
                        deferred.resolve(data);
                    }, function () {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('projectLineService', [
        '$resource', '$q', function ($resource, $q) {

            var resource = $resource('/api/projects/:pid/lines/:id',
            { pid: '@pid', id: '@id' },
            {
                update: { method: 'PUT' }
            });


            return {
                get: function (pid, id) {
                    var deferred = $q.defer();

                    resource.get({ pid: pid, id: id }, function (data) {
                        deferred.resolve(data);
                    }, function () {
                        deferred.reject();
                    });

                    return deferred.promise;
                },
                query: function (pid, filter) {

                    filter = filter || {};

                    // OData params
                    var params = { pid: pid };

                    var filterExpr = null;
                    var filterParts = [];

                    if (filterParts.length > 0) {
                        filterExpr = filterParts.join(' and ');
                    }
                    if (filterExpr) {
                        params['$filter'] = filterExpr;
                    }

                    if (filter.orderBy) {
                        var order = filter.orderByDesc ? 'desc' : 'asc';
                        params['$orderby'] = filter.orderBy + ' ' + order;
                    }

                    if (filter.skip) {
                        params['$skip'] = filter.skip;
                    }

                    if (filter.take) {
                        params['$top'] = filter.take;
                    }

                    var deferred = $q.defer();

                    resource.query(params, function (data) {
                        deferred.resolve(data);
                    }, function () {
                        deferred.reject();
                    });

                    return deferred.promise;
                }
            };
        }
    ]);

    module.factory('projectCycleService', [
        '$resource', '$q', function ($resource, $q) {

            var resource = $resource('/api/projects/:id/cycle', { id: '@id' });

            return {
                kickOff: function (id) {

                    var deferred = $q.defer();

                    resource.save({id: id}, function () {
                        deferred.resolve();
                    }, function () {
                        deferred.reject();
                    });
                    return deferred.promise;
                }
            };
        }
    ]);

})(window, window.angular);
