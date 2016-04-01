(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$location', 'membershipService', '$rootScope'];
    function rootCtrl($scope, $location, membershipService, $rootScope) {

        $scope.userData = {};

        $scope.logout = function () {
            membershipService.removeCredentials();
            $location.path('#/');
            displayUserInfo();
        };

        function displayUserInfo() {
            $scope.userData.isUserLoggedIn = membershipService.isUserLoggedIn();
            if ($scope.userData.isUserLoggedIn) {
                $scope.username = $rootScope.repository.loggedUser.username;
            }
        };

        displayUserInfo();
    }

})(angular.module('jySpaCinema'));