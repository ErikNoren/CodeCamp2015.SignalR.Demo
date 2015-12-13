(function () {
    'use strict';

    angular
        .module('app')
        .controller('eventscontroller', eventscontroller);

    eventscontroller.$inject = ['$scope','eventsservice'];

    function eventscontroller($scope, eventsservice) {
        /* jshint validthis:true */
        var vm = this;
        vm.eventsItems = [];
        vm.refreshItems = function () { updateEvents(); };
        vm.eventsHub = {};

        activate();

        function activate() {
            updateEvents();

            vm.eventsHub = $.connection.eventsHub;

            vm.eventsHub.client.newEvent = function (newEvent) {
                 $scope.$apply(function() {
                   vm.eventsItems.push(newEvent);
                 });
            };

            vm.eventsHub.client.updatedEvent = function (updatedEvent) {
                $scope.$apply(function () {
                    removeEventById(updatedEvent.Id);
                    vm.eventsItems.push(updatedEvent);
                });
            };

            vm.eventsHub.client.deletedEvent = function (deletedEventId) {
                $scope.$apply(function () {
                    removeEventById(deletedEventId);
                });
            };

            $.connection.hub.start().done(function () {
            });
        }

        function removeEventById(eventId) {
            var results = vm.eventsItems.filter(function (obj) {
                return obj.Id === eventId;
            });

            for (var i = 0; i < results.length; i++) {
                vm.eventsItems.pop(results[i]);
            }
        }

        function updateEvents() {
            eventsservice.getData().then(function (response) {
                vm.eventsItems = response.data;
            });
        }
    }
})();
