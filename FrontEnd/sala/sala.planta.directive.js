angular.module('app').directive('salaPlanta', ['$compile', function ($compile) {
    return {
        restrict: 'A',
        templateUrl: 'image/sala.svg',
        link: function (scope, element, attrs) {

        }
    }
}]);