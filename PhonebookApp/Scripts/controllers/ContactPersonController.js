var ContactPersonController = function ($scope, $route,  ContactPersonService) {
    loadContactPersons();

    $scope.OpenAddNewContactModal = function () {
        $scope.modalTitle = 'Add New Contact';
        $scope.editMode = false;
        $scope.newContactPerson = {
            phonenumbers : []
        };
    };

    $scope.GenerateReport = function () {
        var promise = ContactPersonService.generateReport();
        promise.then(function (result) {
            var file = new Blob([result], { type: 'application/pdf' });
            var fileURL = URL.createObjectURL(file);
            window.open(fileURL);
        })

    }
    ;

   
    $scope.DeleteContact = function (contactPerson) {
        var promise = ContactPersonService.deleteContactPerson(contactPerson);
        promise.then(function (result) {
            angular.element('#myModal').modal('hide');
            loadContactPersons();
        });
        
    }
    ;

    $scope.EditContact = function (contactPerson) {
        $scope.modalTitle = 'Edit Contact';
        $scope.editMode = true;
        var promise = ContactPersonService.getContactPersonDetails(contactPerson.id);
        promise.then(function (result) {
            result.birthday = new Date(result.birthday);
            $scope.newContactPerson = result;
        });
    }
    ;

    $scope.SaveContactPerson = function (contactPerson) {
        var promise;
        if ($scope.editMode) {
           promise = ContactPersonService.updateContactPerson(contactPerson);
        } else {
          promise =  ContactPersonService.saveContactPerson(contactPerson);
        }
       
        promise.then(function (result) {
            angular.element('#myModal').modal('hide');
            loadContactPersons();
        });
        
    };

    $scope.RemovePhonenumberItem = function (row) {
        var index = $scope.newContactPerson.phonenumbers.indexOf(row);
        if (index !== -1) {
            $scope.newContactPerson.phonenumbers.splice(index, 1);
        }
    }



    function loadContactPersons() {
        var promise = ContactPersonService.getAllContactPersons();
        promise.then(function (result) {
            $scope.ContactPersons = result;
        });
    }

};

ContactPersonController.$inject = ['$scope', '$route', 'ContactPersonService'];