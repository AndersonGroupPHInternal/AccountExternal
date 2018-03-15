(function () {
    'use strict';

    angular
        .module('App')
        .controller('CredentialController', CredentialController);

    CredentialController.$inject = ['$filter', '$window', 'CredentialService', 'RoleService'];

    function CredentialController($filter, $window, CredentialService, RoleService) {
        var vm = this;

       
        vm.Credentials = [];
         
        vm.GoToUpdatePage = GoToUpdatePage;
        vm.Initialise = Initialise;
        
        vm.Delete = Delete;
        
        function GoToUpdatePage(credentialId) {
            $window.location.href = '../Credential/Update/' + credentialId;
        } 

        function Initialise() {
            Read();
        }
        
        function Read() {
            CredentialService.Read()
                .then(function (response) {
                    vm.Credentials = response.data;
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

        function Delete(credentialId) {
            CredentialService.Delete(credentialId)
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