angular.module('app').directive('salaAndar', ['$compile', function($location) {
	return {
        restrict: 'A',
        scope: {
            buscarAltura: '=',
            buscarAbcissa: '='
        },
        link: function (scope, element, attrs) {
            const MIN_Y = 0;
            const MIN_X = 0;

            let maximoSalas = scope.$parent.andar.QuantidadeMaximaSalas;
            let indice = scope.$parent.$index;
            
            let largura = 240;
            let altura = 300;

            let abcissa = scope.buscarAbcissa();
            let ordenada = scope.buscarAltura();

            element.attr("x", `${abcissa * largura}`);
            element.attr("y", `${ordenada == 800 ? ordenada - altura : ordenada * altura}`);
            element.attr("height", `${altura}`);
            element.attr("width", `${largura - 10}`);
        }
    }
}])