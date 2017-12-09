angular.module('app')
.directive('mapSidebar', function (authService, $rootScope, $log, $timeout) {

  return {

    restrict: 'E',

    scope: {},
    
    templateUrl: 'sidebar/sidebar.directive.html',
    
    controller: function ($scope ) {

        $scope.isSidenavOpen = false
        
        $scope.toggleSidenav = function() {
          $scope.isSidenavOpen = !$scope.isSidenavOpen
        }



        //////////////////////////

        $scope.active = 0;
        var edificacoes = $scope.edificacoes = []; 
        var slides = $scope.slides = [];
        var currIndex = 0;
      
        $scope.addSlide = function() {
          edificacoes.push({
            image: '/image/building.jpeg',
            text: ['building details'][slides.length % 4],
            id: currIndex++
          });
        };
      
        for (var i = 0; i < 4; i++) {
          $scope.addSlide();
        }
      
      
      

    }

    
  }

});