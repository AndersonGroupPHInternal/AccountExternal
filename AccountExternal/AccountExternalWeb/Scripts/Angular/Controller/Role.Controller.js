(function () {
    'use strict';

    angular
        .module('App')
        .controller('RoleController', RoleController);

    RoleController.$inject = ['$filter', '$window', 'RoleService'];

    function RoleController($filter, $window, RoleService) {
        var vm = this;

        vm.CredentialId;
        vm.RoleId;

        vm.AssignedRoles = [];
        vm.Roles = [];

        vm.Delete = Delete;

        vm.GoToUpdatePage = GoToUpdatePage;
        vm.Initialise = Initialise;
        vm.UpdateRole = UpdateRole;

        function GoToUpdatePage(roleId) {
            $window.location.href = '../Role/Update/' + roleId;
        }
        function UpdateRole(role) {
            role.AssignedRoles = $filter('filter')(vm.Roles, { RoleId: role.RoleId })[0];
        }
        function Initialise(credentialId) {
            vm.CredentialId = credentialId;
            Read();
            if (credentialId != undefined)
                ReadAssignedRole();
        }   

        function Read() {
            RoleService.Read()
                .then(function (response) {
                    vm.Roles = response.data;
                })
                .catch(function (data, status) {
                    new PNotify({
                        title: status,
                        text: data,
                        type: 'error',
                        hide: true,
                        addclass: "stack-bottomright"
                    });

                });
        }

        function ReadAssignedRole() {
            RoleService.ReadAssignedRole(vm.CredentialId)
                .then(function (response) {
                    vm.AssignedRoles = response.data;
                })
                .catch(function (data, status) {
                    new PNotify({
                        title: status,
                        text: data,
                        type: 'error',
                        hide: true,
                        addclass: "stack-bottomright"
                    });

                });
        }

        function Delete(roleId) {
            RoleService.Delete(roleId)
                .then(function (response) {
                    Read();
                })
                .catch(function (data, status) {
                    new PNotify({
                        title: status,
                        text: data,
                        type: 'error',
                        hide: true,
                        addclass: "stack-bottomright"
                    });
                });
        }
    }
})();