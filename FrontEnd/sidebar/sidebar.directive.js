angular.module('app')
.directive('mapSidebar', function (authService, $rootScope, $log, $timeout) {

  return {

    restrict: 'E',

    scope: { 
      entidades: '=',
      tipoAtual:'=' 
    },
    
    templateUrl: 'sidebar/sidebar.directive.html',
    
    controller: function ($scope) {

        $scope.abrir = abrir;
        $scope.fechar = fechar;
        $scope.isSidenavOpen = true
        $scope.toggleSidenav = function() {
          $scope.isSidenavOpen = !$scope.isSidenavOpen
        }

        function abrir() {
          $scope.cadastro = true;
        }

        function fechar() {
          $scope.cadastro = false;
        }

    }

    
  }

});