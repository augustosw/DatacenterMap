angular.module('app').controller('LoginController', function ($scope,$location, authService) {

  $scope.registrar = registrar; 
  $scope.login = function (usuario) {

    authService.login(usuario)
      .then(
        function (response) {
          console.log(response);
        },
        function (response) {
          console.log(response);
        });
  };

  function registrar() {
    $location.path('/cadastro');
  }

});