angular.module('app').directive('slotSala', ['$compile', function ($compile) {
    return {
        restrict: 'A',
        templateUrl: 'image/sala.svg',
        scope: {
            buscarAltura: '=',
            buscarAbcissa: '='
        },
        link: function (scope, element, attrs) {
            const MIN_Y = 100;
            const MIN_X = 100;

            element.attr("y", `${scope.buscarAltura() * MIN_Y}`);
            element.attr("x", `${scope.buscarAbcissa() * MIN_X}`);
            element.attr("height", `100`);
            element.attr("width", `100`);
            
        }
    }
}]);