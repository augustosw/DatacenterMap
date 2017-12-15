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
            
            let largura = (1200 / maximoSalas) + 100;
            let altura = 300;

            function calcY() {
                if (calcX() == 300)
                    return (maximoSalas - indice) * -300;
                else 
                    return (indice * 300);
            }
            function calcX() {
                if (indice > (maximoSalas / 2))
                    return indice * largura;
                else
                    return 0;
            }

            element.attr("y", `${scope.buscarAltura() == 800 ? scope.buscarAltura() - altura : scope.buscarAltura() * altura}`);
            element.attr("x", `${scope.buscarAbcissa() * largura}`);
            element.attr("height", `${altura}`);
            element.attr("width", `${largura - 10}`);
        }
    }
}])