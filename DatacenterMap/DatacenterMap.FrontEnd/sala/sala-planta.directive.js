angular.module('app').directive('salaPlanta', ['$compile', function ($compile, $location, rackService) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            
            var altura = 0;
            var abcissa = 1;

            scope.limparSlot = function (id) {
                rackService.excluir(id)
                    .then(function(response){
                        location.reload();
                    });
            };

            scope.buscarAltura = function buscarAltura() {
                if(altura == 6){
                    abcissa++;
                    abcissa++; 
                    altura = 1;
                }
                else
                    altura++;
                return altura;
            }

            scope.buscarAbcissa = function buscarAbcissa() {
                    return abcissa;
            }

            
        }
    }
}]);