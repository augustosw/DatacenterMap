angular.module('app').directive('andarPlanta', ['$compile', function ($compile) {
    return {
        restrict: 'A',
        templateUrl: 'image/andar.svg',
        link: function (scope, element, attrs) {
            
        }
    }
}]);