angular.module('app')
.directive('mapSidebar', function (authService, $rootScope, $log, $timeout, $mdSidenav) {

  return {

    restrict: 'E',

    scope: {},
    
    templateUrl: 'sidebar/sidebar.directive.html',
    
    controller: function ($scope ) {
    
        $scope.toggleLeft = buildToggler('left');
        $scope.toggleRight = buildToggler('right');
    
        function buildToggler(componentId) {
          return function() {
            $mdSidenav(componentId).toggle();
          };
        }
    }
  }

});