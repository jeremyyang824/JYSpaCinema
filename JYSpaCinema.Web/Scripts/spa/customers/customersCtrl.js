(function (app) {
    'use strict';

    app.controller('customersCtrl', customersCtrl);

    customersCtrl.$inject = ['$scope', '$modal', 'apiService', 'notificationService'];
    function customersCtrl($scope, $modal, apiService, notificationService) {

        $scope.pageClass = 'page-customers';
        $scope.loadingCustomers = false;

        $scope.page = 1;
        $scope.pageSize = 15;
        $scope.filterCustomers = '';

        $scope.totalCount = 0;
        $scope.Customers = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.openEditDialog = openEditDialog;

        function search(page) {
            $scope.page = page || 1;
            $scope.loadingCustomers = true;

            apiService.get('/api/customers/search/',
                //config
                {
                    params: {
                        page: $scope.page,
                        pageSize: $scope.pageSize,
                        filter: $scope.filterCustomers
                    }
                },
                //success
                function (result) {
                    $scope.Customers = result.data.Results;
                    $scope.totalCount = result.data.TotalItemCount;
                    $scope.loadingCustomers = false;

                    notificationService.displayInfo($scope.totalCount + ' customers found');
                },
                //failure
                function (response) {
                    notificationService.displayError(response.data);
                });
        };

        function clearSearch() {
            $scope.filterCustomers = '';
            search();
        };

        function openEditDialog(customer) {

        };
    };

})(angular.module('jySpaCinema'));