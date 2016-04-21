(function (app) {
    'use strict';

    app.directive('customPager', function () {
        return {
            scope: {
                page: '@',          //当前页
                pagesCount: '@',    //总页码
                //totalCount: '@',    //总记录数
                searchFunc: '&'     //获取分页数据方法
            },
            replace: true,
            restrict: 'E',
            templateUrl: '/scripts/spa/directives/pager.html',
            controller: ['$scope', function ($scope) {
                $scope.search = function (i) {
                    if ($scope.searchFunc) {
                        $scope.searchFunc({ page: i });
                    }
                };

                $scope.range = function () {
                    if (!$scope.pagesCount)
                        return [];

                    var step = 2,
                        doubleStep = step * 2,
                        start = Math.max($scope.page - step, 1),
                        end = Math.min(start + 1 + doubleStep, $scope.pagesCount);

                    var ret = [];
                    for (var i = start; i <= end; i++) {
                        ret.push(i);
                    }
                    return ret;
                };

            }]
        };
    });

})(angular.module('common.ui'));