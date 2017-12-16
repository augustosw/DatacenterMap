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


            element.attr("y", `${scope.buscarAltura() == 800 ? scope.buscarAltura() - altura : scope.buscarAltura() * altura}`);
            element.attr("x", `${scope.buscarAbcissa() * largura}`);
            element.attr("height", `${altura}`);
            element.attr("width", `${largura - 10}`);
        }
    }
}])