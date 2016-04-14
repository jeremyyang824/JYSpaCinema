(function (app) {
    'use strict';

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location'];
    function registerCtrl($scope, membershipService, notificationService, $rootScope, $location) {
        $scope.pageClass = 'page-login';
        $scope.register = register;
        $scope.user = {};

        function register(isValid) {
            if (!isValid) {
                notificationService.displayError('Validation failed.');
                return;
            }

            membershipService.register($scope.user, function (result) {
                if (result.data.success) {
                    membershipService.saveCredentials($scope.user);
                    notificationService.displaySuccess('您好！' + $scope.user.username);
                    $scope.displayUserInfo();
                    $location.path('/');
                } else {
                    notificationService.displayError('Registration failed. Try again.');
                }
            });
        }
    }

})(angular.module('common.core'));