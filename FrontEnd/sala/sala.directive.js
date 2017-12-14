angular.module('app').directive('salaAndar', ['$compile', function($location) {
	return {
        restrict: 'A',
        scope: {},
        link: function (scope, element, attrs) {
            const MIN_Y = 100;
            const MIN_X = 100;

            let maximoSalas = scope.$parent.andar.QuantidadeMaximaSalas;
            let indice = scope.$parent.$index;

            function calcY() {
                if (calcX() == 300)
                    return (maximoSalas - indice) * -300;
                else 
                    return (indice * 300);
            }
            function calcX() {
                if (indice > (maximoSalas / 2))
                    return 300;
                else
                    return 0;
            }

            element.attr("x", `${calcX() + MIN_X}`);
            element.attr("y", `${calcY() + MIN_Y}`);
            element.attr("height", `${maximoSalas * 70}`);
            element.attr("width", `${maximoSalas * 70}`);
        }
    }
}])