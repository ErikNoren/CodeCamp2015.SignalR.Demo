(function () {
    'use strict';

    angular
        .module('app')
        .controller('eventscontroller', eventscontroller);

    eventscontroller.$inject = ['eventsservice'];

    function eventscontroller(eventsservice) {
        /* jshint validthis:true */
        var vm = this;
        vm.eventsItems = [];
        vm.refreshItems = function () { updateEvents(); };
        vm.eventsHub = {};

        activate();

        function activate() {
            vm.eventsHub = $.connection.eventsHub;

            vm.eventsHub.client.newEvent = function () {
                updateEvents();
            };

            vm.eventsHub.client.updatedEvent = function () {
                updateEvents();
            };

            vm.eventsHub.client.deletedEvent = function () {
                updateEvents();
            };

            $.connection.hub.start().done(function () {
            });

            updateEvents();
        }

        function updateEvents() {
            eventsservice.getData().then(function (response) {
                vm.eventsItems = response.data;
            });
        }
    }
})();
