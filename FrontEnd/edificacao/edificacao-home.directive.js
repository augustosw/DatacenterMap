angular.module('app')
.directive('mapEdificacaoHome', function (authService, edificacaoService, $rootScope, $log, $location, $timeout) {

  return {

    restrict: 'E',

    scope: {},
    
    templateUrl: 'edificacao/edificacao-home.html', 

    controller: function ($scope ) { 

    }
  }

});