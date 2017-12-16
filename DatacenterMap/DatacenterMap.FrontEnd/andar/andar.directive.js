angular.module('app').directive('andarPlanta', ['$compile', function ($compile) {
    return {
        restrict: 'E',
        templateUrl: 'andar/andar.html',
        link: function (scope, element, attrs) {

            var altura = 0;
            var abcissa = 0;

            scope.buscarAbcissa = function buscarAbcissa() {
                if (abcissa == 4) {
                    altura = 100;
                    abcissa = 0;
                }
                else
                    abcissa++;
                return abcissa;
            }

            scope.buscarAltura = function buscarAltura() {
                return altura;
            }

            element.on('click', function () {
                altura = 0;
                abcissa = -1;
            });

        }
    }
}]);