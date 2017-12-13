angular.module('app').directive('salaAndar', ['$compile', function($location) {
	return {
        restrict: 'A',
        scope: {},
        link: function (scope, element, attrs) {
            const MIN_Y = 36;
            const MIN_X = 36;

            let maximoSalas = scope.$parent.andar.QuantidadeMaximaSalas;
            let indice = scope.$parent.$index;

            function calcY() {
                if (indice > (maximoSalas / 2))
                    return 400;
                else
                    return 0;
            }
            function calcX() {
                if (calcY() == 400)
                    return (maximoSalas - indice) * -300;
                else 
                    return (indice * 300);
            }

            element.attr("x", `${calcX() + MIN_X}`);
            element.attr("y", `${calcY() + MIN_Y}`);
            element.attr("height", `${maximoSalas * 70}`);
            element.attr("width", `${maximoSalas * 70}`);
        }
    }
}])