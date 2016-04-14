(function (app) {
    'use strict';
    app.controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', 'membershipService', 'notificationService', '$rootScope', '$location'];
    function loginCtrl($scope, membershipService, notificationService, $rootScope, $location) {
        $scope.pageClass = 'page-login';
        $scope.user = {};

        $scope.login = function (isValid) {
            if (!isValid) {
                notificationService.displayError('Validation failed.');
                return;
            }

            membershipService.login($scope.user, function (result) {
                //loginCompleted
                if (result.data.success) {
                    membershipService.saveCredentials($scope.user);
                    notificationService.displaySuccess('您好！' + $scope.user.username);
                    $scope.displayUserInfo();
                    if ($rootScope.previousState)
                        $location.path($rootScope.previousState);
                    else
                        $location.path('/');
                } else {
                    notificationService.displayError('Login failed. Try again.');
                }
            });
        };
    };
})(angular.module('common.core'));