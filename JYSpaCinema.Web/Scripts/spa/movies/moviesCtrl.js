(function (app) {
    'use strict';

    app.controller('moviesCtrl', ['$scope', 'apiService', 'notificationService',
        function ($scope, apiService, notificationService) {
            //debugger;
            $scope.pageClass = 'page-movies';
            $scope.loadingMovies = false;
            
            $scope.filterMovies = '';
            $scope.page = 1;
            $scope.pageSize = 12;
            $scope.totalItemCount = 0;
            $scope.Movies = [];

            $scope.search = search;
            $scope.clearSearch = clearSearch;
            $scope.pageChanged = pageChanged;

            function search() {
                loadMovies(1, function (data) {
                    notificationService.displayInfo(data.TotalItemCount + ' movies found');
                });
            }

            function clearSearch() {
                $scope.filterMovies = '';
                search();
            }

            function loadMovies(page, successCallback) {
                $scope.loadingMovies = true;

                var searchCond = {
                    page: $scope.page,
                    pageSize: $scope.pageSize
                };
                if ($scope.filterMovies)
                    searchCond["filter"] = $scope.filterMovies;

                apiService.get('/api/movies/search/',
                    {
                        params: searchCond
                    },
                    function(result) {
                        $scope.Movies = result.data.Results;
                        $scope.totalItemCount = result.data.TotalItemCount;
                        $scope.loadingMovies = false;

                        if (successCallback && typeof (successCallback) == 'function') {
                            successCallback(result.data);
                        }
                    },
                    function (response) {
                        notificationService.displayError(response.data);
                    });
            };

            function pageChanged() {
                loadMovies($scope.page);
            }

            $scope.search();

        }]);

})(angular.module('jySpaCinema'));