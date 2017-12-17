angular.module('app').directive('slotSala', ['$compile', function ($compile) {
    return {
        restrict: 'A',
        scope: {
            buscarAltura: '=',
            buscarAbcissa: '=',
        },
        link: function (scope, element, attrs) {
            const MIN_Y = 100;
            const MIN_X = 100;

            var slot = scope.$parent.slot;

            let ordenada = scope.buscarAltura();
            let abcissa = scope.buscarAbcissa();

            element.attr("style",`transform: translate(${abcissa * MIN_X}%, ${ordenada * MIN_Y}%);`);

            slot.Ocupado ? element.css({background: '#f3bca7'}) : element.css({background: '#a2f0d6'});
            
        }
    }
}]);