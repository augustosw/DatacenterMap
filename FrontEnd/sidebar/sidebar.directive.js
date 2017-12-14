angular.module('app')
.directive('mapSidebar', function (authService, $rootScope, $log, $timeout, $mdSidenav) {

  return {

    restrict: 'E',

    scope: { 
      entidades: '=',
      tipoAtual:'=',
      close: '&' 
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


        $scope.toggleLeft = buildToggler('edificacao');
   
        $scope.isOpenLeft = function(){
          return $mdSidenav('edificacao').isOpen();
        };
    
        
        function buildToggler(navID) {
          return function() {
            $mdSidenav(navID)
              .toggle()
              .then(function () {
              });
          };
        }
    
        $scope.close = function () {
          $mdSidenav().close()
            .then(function () {
            });
        };    

    }

    
  }

});