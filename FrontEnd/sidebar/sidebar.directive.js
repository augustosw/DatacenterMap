angular.module('app')
.directive('mapSidebar', function (authService, $rootScope, $log, $timeout) {

  return {

    restrict: 'E',

    scope: {},
    
    templateUrl: 'sidebar/sidebar.directive.html',
    
    controller: function ($scope ) {

        $scope.abrir = abrir;
        $scope.fechar = fechar;
        $scope.isSidenavOpen = true
        $scope.toggleSidenav = function() {
          $scope.isSidenavOpen = !$scope.isSidenavOpen
        }

        $scope.edificacoes = []; 
        var currIndex = 0;

        $scope.edificacoes.push({
          image: '/image/building.jpeg',
          text: ['building details'][$scope.edificacoes.length % 4],
          id: currIndex++ 
        });

        $scope.edificacoes.push({
            image: '/image/logo.svg',
            text: ['segunda foto'][$scope.edificacoes.length % 4],
            id: currIndex++ 
          }); 


        $scope.edificacoes.push({
          image: '/image/user-logo.svg',
          text: ['terceira foto'][$scope.edificacoes.length % 4],
          id: currIndex++ 
        });

        function abrir() {
          $scope.cadastro = true;
        }

        function fechar() {
          $scope.cadastro = false;
        }

    }

    
  }

});