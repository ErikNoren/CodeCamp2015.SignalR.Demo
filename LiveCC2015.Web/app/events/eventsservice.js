(function () {
    'use strict';

    angular
        .module('app')
        .factory('eventsservice', eventsservice);

    eventsservice.$inject = ['$http'];

    function eventsservice($http) {
        var service = {
            getData: getData
        };

        return service;

        function getData() {
            return $http.get("/api/Events");
        }
    }
})();