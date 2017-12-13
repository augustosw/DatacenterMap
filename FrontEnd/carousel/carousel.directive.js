angular.module('app')
.directive('mapCarousel', function (authService, edificacaoService, $routeParams, $rootScope, $location, $log, $timeout) {

  return {

    restrict: 'E',

    scope: {  
        entidades: '=',
        tipoAtual: '=' 
    },
    
    templateUrl: 'carousel/carousel.directive.html',
    
    controller: function ($scope) {

      $scope.active = 0;
      $scope.buscarEntidade = buscarEntidade;
      $scope.getSecondIndex = getSecondIndex;

      function getSecondIndex(index) {
        if(index - $scope.entidades.length >= 0)
          return index-$scope.entidades.length;
        else
          return index;
      }

      function buscarEntidade(entidade) {
        $location.path(`/${$scope.tipoAtual}/${entidade.Id}`);
      }

    }

    
  }

});