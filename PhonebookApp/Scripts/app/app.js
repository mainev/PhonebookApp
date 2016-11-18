var app = angular.module('PhonebookApp', [

    "ngCookies",
     "ngRoute",
    "ngResource",
    'ui.bootstrap',

]);

//***********************************SERVICES*****************************//
app.service('ContactPersonService', ContactPersonService);

//***********************************CONTROLLERS*****************************//
app.controller('ContactPersonController', ContactPersonController);


var configFunction = function ($routeProvider) {

    $routeProvider
      .when('/ContactPersons', {
          templateUrl: 'Home/ContactPersons',
          controller: 'ContactPersonController'

      })
      .otherwise({ redirectTo: '/' });



};

configFunction.$inject = ['$routeProvider'];



app.config(configFunction);
