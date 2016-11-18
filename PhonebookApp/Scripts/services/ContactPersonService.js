var ContactPersonService = function ($http, $q, $route) {

    this.generateReport = function () {
        var result = $q.defer();
        $http({
            method: 'POST',
            url: 'api/ContactPerson/generate_report',
            headers: { 'Content-Type': 'application/json' },
            responseType: 'arraybuffer'
            
        })
        .success(function (response) {
            result.resolve(response);
        })
        .error(function (response) {
            result.reject(response);
        });

        return result.promise;
    }


    this.getContactPersonDetails = function (id) {
        var result = $q.defer();
        $http({
            method: 'GET',
            url: 'api/ContactPerson/details',
            headers: { 'Content-Type': 'application/json' },
            params: { id: id }
        })
        .success(function (response) {
            result.resolve(response);
        })
        .error(function (response) {
            result.reject(response);
        });

        return result.promise;
    }
    this.getAllContactPersons = function () {
        var result = $q.defer();
        $http({
            method: 'GET',
            url: 'api/ContactPerson/all',
            headers: { 'Content-Type': 'application/json' }
        })
        .success(function (response) {
          result.resolve(response);
        })
        .error(function (response) {
          result.reject(response);
        });

        return result.promise;
    }

    this.saveContactPerson = function (newContactPerson) {
        var result = $q.defer();
        $http({
            method: 'POST',
            url: 'api/ContactPerson/save',
            data: newContactPerson,
            headers: { 'Content-Type': 'application/json'}
        })
        .success(function (response) {
            result.resolve(response);
        })
        .error(function (response) {
            result.reject(response);
        });

        return result.promise;

    };

    this.deleteContactPerson = function (contactPerson) {
        var result = $q.defer();
        $http({
            method: 'POST',
            url: 'api/ContactPerson/delete',
            data: contactPerson,
            headers: { 'Content-Type': 'application/json' }
        })
        .success(function (response) {
            result.resolve(response);
        })
        .error(function (response) {
            result.reject(response);
        });

        return result.promise;

    };

    this.updateContactPerson = function (updatedContactPerson) {
        var result = $q.defer();
        $http({
            method: 'POST',
            url: 'api/ContactPerson/update',
            data: updatedContactPerson,
            headers: { 'Content-Type': 'application/json' }
        })
        .success(function (response) {
            result.resolve(response);
        })
        .error(function (response) {
            result.reject(response);
        });

        return result.promise;

    };


}

