(function (app) {
    'use strict';

    app.controller('movieDetailsCtrl', ['$scope', '$location', '$routeParams', '$uibModal', 'apiService', 'notificationService',
        function ($scope, $location, $routeParams, $uibModal, apiService, notificationService) {

            $scope.pageClass = 'page-movies';
            $scope.movie = {};
            $scope.loadingMovie = false;

            $scope.loadingRentals = true;
            $scope.isReadOnly = true;
            $scope.rentalHistory = [];
            $scope.openRentDialog = openRentDialog;

            $scope.returnMovie = returnMovie;
            $scope.getStatusColor = getStatusColor;
            $scope.clearSearch = clearSearch;
            $scope.isBorrowed = isBorrowed;

            function loadMovieDetails() {
                loadMovie();
                loadRentalHistory();
            };

            function loadMovie() {
                $scope.loadingMovie = true;
                apiService.get('/api/movies/details/' + $routeParams.id, null,
                    function (result) {
                        $scope.movie = result.data;
                        $scope.loadingMovie = false;
                    },
                    function (response) {
                        notificationService.displayError(response.data);
                    });
            };

            function loadRentalHistory() {
                $scope.loadingRentals = true;
                apiService.get('/api/rentals/' + $routeParams.id + '/rentalhistory', null,
                    function (result) {
                        $scope.rentalHistory = result.data;
                        $scope.loadingRentals = false;
                    },
                    function (response) {
                        notificationService.displayError(response.data);
                    });
            };

            function isBorrowed(rental) {
                if (!rental)
                    return false;
                return rental.Status === 'Borrowed';
            };

            function clearSearch() {
                $scope.filterRentals = '';
            };

            function getStatusColor(status) {
                if (status === 'Borrowed')
                    return 'red';
                else
                    return 'green';
            };

            function returnMovie(rentalId) {
                apiService.post('/api/rentals/return/' + rentalId, null,
                    function (result) {
                        notificationService.displaySuccess('Movie returned to JYSpaCinema succeesfully');
                        loadMovieDetails();
                    },
                    function (response) {
                        notificationService.displayError(response.data);
                    });
            };

            function openRentDialog() {
                $uibModal.open({
                    animation: true,
                    templateUrl: 'scripts/spa/rental/rentMovieModal.html',
                    controller: 'rentalMovieCtrl',
                    scope: $scope,
                    size: 'lg'
                })
                .result
                .then(function ($scope) {
                    loadMovieDetails();
                }, function () {
                });
            };

            loadMovieDetails();
        }]);

})(angular.module('jySpaCinema'));