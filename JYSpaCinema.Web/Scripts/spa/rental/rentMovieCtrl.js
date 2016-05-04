(function (app) {
    'use strict';
    app.controller('rentalMovieCtrl', ['$scope', '$uibModalInstance', '$location', 'apiService', 'notificationService',
        function ($scope, $uibModalInstance, $location, apiService, notificationService) {

            $scope.Title = $scope.movie.Title;
            $scope.stockItems = [];
            $scope.selectedCustomer = -1;
            $scope.isEnabled = false;

            $scope.loadStockItems = loadStockItems;
            $scope.selectCustomer = selectCustomer;
            $scope.selectionChanged = selectionChanged;

            $scope.rentMovie = rentMovie;
            $scope.cancelRental = cancelRental;

            function loadStockItems() {
                apiService.get('/api/movies/stocks/' + $scope.movie.ID, null,
                    function (result) {
                        $scope.stockItems = result.data;
                        $scope.selectedStockItem = $scope.stockItems[0].ID;
                    },
                    function (response) {
                        notificationService.displayError(response.data);
                    });

            };

            function selectCustomer($item) {
                if ($item) {
                    $scope.selectedCustomer = $item.originalObject.ID;
                    $scope.isEnabled = true;
                } else {
                    $scope.selectedCustomer = -1;
                    $scope.isEnabled = false;
                }
            };

            function selectionChanged($item) {

            };

            function rentMovie() {
                apiService.post('/api/rentals/rent/' + $scope.selectedCustomer + '/' + $scope.selectedStockItem, null,
                    function (result) {
                        notificationService.displaySuccess('Rental completed successfully');
                        $uibModalInstance.close();
                    },
                    function (response) {
                        notificationService.displayError(response.data.Message);
                    });
            };

            function cancelRental() {
                $scope.stockItems = [];
                $scope.selectedCustomer = -1;
                $scope.isEnabled = false;
                $uibModalInstance.dismiss();
            };

            loadStockItems();
        }]);
})(angular.module('jySpaCinema'))