angular.module('app')
  .directive('mapHeader', function (authService, $rootScope, $log) {

    return {

      restrict: 'E',

      scope: {},
      
      templateUrl: 'header/header.directive.html',
      
      controller: function ($scope) {

        atualizarUsuario();

        $scope.usuario = authService.getUsuario();

        $scope.logout = authService.logout;

        $rootScope.$on('authLoginSuccess', function () {
          atualizarUsuario();
        });

        $rootScope.$on('authLogoutSuccess', function () {
          atualizarUsuario();
        });        

        function atualizarUsuario() {
          $scope.usuario = authService.getUsuario();
        }

        $scope.status = {
          isopen: false
        };
      
        $scope.toggleDropdown = function($event) {
          $event.preventDefault();
          $event.stopPropagation();
          $scope.status.isopen = !$scope.status.isopen;
        };
      }
    }

  });