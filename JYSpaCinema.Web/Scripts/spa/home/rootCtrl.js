(function (app) {
    'use strict';

    app.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['$scope', '$location', '$rootScope'];
    function rootCtrl($scope, $location, $rootScope) {

        $scope.userData = {};
        $scope.logout = function () {
            $location.path('#/');
            displayUserInfo();
        };

        function displayUserInfo() {
            $scope.userData.isUserLoggedIn = true;
            if ($scope.userData.isUserLoggedIn) {
                $scope.username = 'jeremyyang';
            }
        };

        displayUserInfo();
    }

})(angular.module('jySpaCinema'));