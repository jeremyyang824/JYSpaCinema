(function (app) {
    'use strict';

    app.controller('customersRegCtrl', ['$scope', '$location', 'apiService', 'notificationService',
        function customersRegCtrl($scope, $location, apiService, notificationService) {

            $scope.newCustomer = {};
            $scope.submission = {
                successMessages: [],
                errorMessages: []
            };

            $scope.registerCustomer = registerCustomer;

            //日历设置
            $scope.openDatePicker = openDatePicker;
            $scope.dateOptions = {
                startingDay: 1,
                showWeeks: false,
                isOpened: false
            };

            function registerCustomer(isValid) {
                if (!isValid) {
                    debugger;
                    notificationService.displayError('Validation failed.');
                    return;
                }

                $scope.submission.errorMessages = [];
                $scope.submission.successMessages = [];

                apiService.post(
                    '/api/customers/register/',
                    $scope.newCustomer,
                    function (result) {
                        debugger;
                        //info
                        var customerRegistered = result.data;
                        $scope.submission.successMessages.push(customerRegistered.LastName + ' has been successfully registed');
                        $scope.submission.successMessages.push('Check ' + customerRegistered.IdentityCard + ' for reference number');
                        //reset
                        $scope.newCustomer = {};    
                    },
                    function (response) {
                        //notificationService.displayError(response.data);
                        $scope.submission.errorMessages = response.data.errors;
                    });
            };

            function openDatePicker() {
                $scope.dateOptions.isOpened = true;
            };
        }]);

})(angular.module('jySpaCinema'));
