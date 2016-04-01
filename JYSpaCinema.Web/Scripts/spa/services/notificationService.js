﻿(function (app) {
    'use strict';

    app.factory('notificationService', function () {
        toastr.options = {
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 3000,
            "extendedTimeOut": 1000
        };

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        };

        function displaySuccess(message) {
            toastr.success(message);
        };

        function displayError(error) {
            if (Array.isArray(error)) {
                error.forEach(function (err) {
                    toastr.error(err);
                });
            } else {
                toastr.error(error);
            }
        };

        function displayWarning(message) {
            toastr.warning(message);
        };

        function displayInfo(message) {
            toastr.info(message);
        };
    });

})(angular.module('common.core'));