(function (app) {
    'use strict';

    app.controller('customersCtrl', customersCtrl);

    customersCtrl.$inject = ['$scope', '$uibModal', 'apiService', 'notificationService'];
    function customersCtrl($scope, $uibModal, apiService, notificationService) {

        $scope.pageClass = 'page-customers';
        $scope.loadingCustomers = false;

        $scope.filterCustomers = '';
        $scope.page = 1;
        $scope.pageSize = 4;
        $scope.totalItemCount = 0;
        $scope.Customers = [];

        $scope.search = search;
        $scope.pageChanged = pageChanged;

        $scope.loadCustomers = loadCustomers;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function loadCustomers(page, successCallback) {
            $scope.page = page || 1;
            $scope.loadingCustomers = true;

            var searchCond = {
                page: $scope.page,
                pageSize: $scope.pageSize
            };
            if ($scope.filterCustomers)
                searchCond["filter"] = $scope.filterCustomers;

            apiService.get('/api/customers/search/',
                //config
                {
                    params: searchCond
                },
                //success
                function (result) {
                    $scope.Customers = result.data.Results;
                    $scope.totalItemCount = result.data.TotalItemCount;
                    $scope.loadingCustomers = false;

                    if (successCallback && typeof (successCallback) == 'function') {
                        successCallback(result.data);
                    }
                },
                //failure
                function (response) {
                    notificationService.displayError(response.data);
                });
        };

        function search() {
            loadCustomers(1, function (data) {
                notificationService.displayInfo(data.TotalItemCount + ' customers found');
            });
        }

        function pageChanged() {
            loadCustomers($scope.page);
        }

        function clearSearch() {
            $scope.filterCustomers = '';
            search();
        };

        function openEditDialog(customer) {
            $scope.EditedCustomer = customer;
            $uibModal.open({
                animation: true,
                templateUrl: '/scripts/spa/customers/editCustomerModal.html',
                controller: 'customerEditCtrl',
                scope: $scope,
                size: 'lg'
            })
            .result
            .then(function ($scope) {
                clearSearch();
            }, function () {
            });
        };

        $scope.loadCustomers(1);
    };

})(angular.module('jySpaCinema'));