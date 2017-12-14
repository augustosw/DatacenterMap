angular.module('app').directive('salaPlanta', ['$compile', function ($compile) {
    return {
        restrict: 'A',
        templateUrl: 'image/sala.svg',
        link: function (scope, element, attrs) {
            
            var altura = 0;
            var abcissa = 1;
            
            scope.buscarAbcissa = function buscarAbcissa() {
                    return abcissa;
            }

            scope.buscarAltura = function buscarAltura() {
                if(altura == 6){
                    abcissa++;
                    abcissa++; 
                    altura = 1
                }
                else
                    altura++;
                return altura;
            }

            
        }
    }
}]);