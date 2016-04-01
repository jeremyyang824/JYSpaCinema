(function (app) {
    'use strict';
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', '$location', 'notificationService', '$rootScope'];
    function apiService($http, $location, notificationService, $rootScope) {
        return {
            get: get,
            post: post
        };

        function get(url, config, success, failure) {
            return $http.get(url, config)
                .then(function (result) {
                    //success callback
                    success(result);
                }, function (error) {
                    if (error.status == '401') {
                        notificationService.displayError('Authentication required.');
                        $rootScope.previousState = $location.path();
                        $location.path('/login');
                    } else if (failure != null) {
                        //failure callback
                        failure(error);
                    }
                });
        };

        function post(url, data, success, failure) {
            return $http.post(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == '401') {
                        notificationService.displayError('Authentication required.');
                        $rootScope.previousState = $location.path();
                        $location.path('/login');
                    } else if (failure != null) {
                        failure(error);
                    }
                });
        };
    };

})(angular.module('common.core'));