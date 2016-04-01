(function (app) {
    'use strict';

    app.factory('membershipService', membershipService);

    membershipService.$inject = ['apiService', 'notificationService', '$http', '$base64', '$cookieStore', '$rootScope'];
    function membershipService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {
        return {
            login: login,
            register: register,
            saveCredentials: saveCredentials,
            removeCredentials: removeCredentials,
            isUserLoggedIn: isUserLoggedIn
        };

        function login(loginDto, completed) {
            apiService.post('/api/account/authenticate', loginDto,
                completed,
                function (response) {
                    notificationService.displayError(response.data);
                });
        };

        function register(registrationDto, completed) {
            apiService.post('/api/account/register', registrationDto,
                completed,
                function () {
                    notificationService.displayError('Registration failed. Try again.');
                });
        };

        function saveCredentials(user) {
            var membershipData = $base64.encode(user.username + ':' + user.password);
            $rootScope.repository = {
                loggedUser: {
                    username: user.username,
                    authdata: membershipData
                }
            };
            $http.defaults.headers.common['Authorization'] = 'Basic ' + membershipData;
            $cookieStore.put('repository', $rootScope.repository);
        };

        function removeCredentials() {
            $rootScope.repository = {};
            $cookieStore.remove('repository');
            $http.defaults.headers.common.Authorization = '';
        };

        function isUserLoggedIn() {
            return $rootScope.repository.loggedUser != null;
        };
    };

})(angular.module('common.core'));