(function (app) {
    'use strict';
    app.controller('customerEditCtrl', customerEditCtrl);

    customerEditCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];
    function customerEditCtrl($scope, $uibModalInstance, $timeout, apiService, notificationService) {
        //debugger;

        $scope.cancelEdit = cancelEdit;
        $scope.updateCustomer = updateCustomer;

        //日历设置
        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            startingDay: 1,
            showWeeks: false,
            isOpened: false
        };

        function cancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss('cancel');
        }

        function updateCustomer(isValid) {
            if (!isValid) {
                notificationService.displayError('Validation failed.');
                return;
            }

            apiService.post(
                '/api/customers/update/',
                $scope.EditedCustomer,
                //success
                function (result) {
                    notificationService.displaySuccess($scope.EditedCustomer.FirstName + ' ' + $scope.EditedCustomer.LastName + ' 更新成功！');
                    $uibModalInstance.close('ok');
                },
                //failure
                function (response) {
                    notificationService.displayError(response.data);
                });
        }

        function openDatePicker($event) {
            //$event.preventDefault();
            //$event.stopPropagation();

            $timeout(function () {
                $scope.dateOptions.isOpened = true;
            });
        }
    };

})(angular.module('jySpaCinema'));