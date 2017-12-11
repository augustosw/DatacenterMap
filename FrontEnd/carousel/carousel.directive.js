angular.module('app')
.directive('mapCarousel', function (authService, $rootScope, $log, $timeout) {

  return {

    restrict: 'E',

    scope: {  
        edificacoes: '=edificacoes' 
    },
    
    templateUrl: 'carousel/carousel.directive.html',
    
    controller: function ($scope ) {


        $scope.active = 0;


          $scope.getSecondIndex = function(index)
          {
            if(index-$scope.edificacoes.length>=0)
              return index-$scope.edificacoes.length;
            else
              return index;
          }
            
      

    }

    
  }

});