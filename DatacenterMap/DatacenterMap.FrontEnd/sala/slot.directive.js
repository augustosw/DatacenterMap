angular.module('app').directive('slotSala', ['$compile', function ($compile) {
    return {
        restrict: 'A',
        templateUrl: 'image/sala.svg',
        scope: {
            buscarAltura: '=',
            buscarAbcissa: '=',
        },
        link: function (scope, element, attrs) {
            const MIN_Y = 100;
            const MIN_X = 100;

            var slot = scope.$parent.slot;

            element.attr("y", `${scope.buscarAltura() * MIN_Y}`);
            element.attr("x", `${scope.buscarAbcissa() * MIN_X}`);
            element.attr("height", `100`);
            element.attr("width", `100`);

            slot.Ocupado ? element.attr("fill", "#f3bca7") : element.attr("fill", "#a2f0d6");
            
        }
    }
}]);