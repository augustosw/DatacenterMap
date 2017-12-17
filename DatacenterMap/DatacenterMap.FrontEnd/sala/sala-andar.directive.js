angular.module('app').directive('salaAndar', ['$compile', function($location) {
	return {
        restrict: 'A',
        scope: {
            buscarAltura: '=',
            buscarAbcissa: '='
        },
        link: function (scope, element, attrs) {
            const MIN_Y = -135;
            const MIN_X = -250;

            let maximoSalas = scope.$parent.andar.QuantidadeMaximaSalas;
            let indice = scope.$parent.$index;
            
            let x = 100;
            let y = 65;

            let abcissa = scope.buscarAbcissa();
            let ordenada = scope.buscarAltura();
            
            element.attr("style",`transform: translate(${(abcissa * x) + MIN_X}%, ${(ordenada == 100 ? ordenada - y : MIN_Y)}%);`)

            // const MIN_Y = 25;
            // const MIN_X = 0;

            // let maximoSalas = scope.$parent.andar.QuantidadeMaximaSalas;
            // let indice = scope.$parent.$index;
            
            // let x = 21;
            // let y = 55;

            //  let abcissa = scope.buscarAbcissa();
            // let ordenada = scope.buscarAltura();

            // element.attr("style",`left: ${(abcissa * x) + MIN_X}%; top: ${(abcissa == 6 ? ordenada - y : MIN_Y)}%;`)



        }
    }
}])