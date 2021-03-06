angular.module('app').directive('salaAndar', ['$compile', function($location) {
	return {
        restrict: 'A',
        scope: {
            buscarAltura: '=',
            buscarAbcissa: '='
        },
        link: function (scope, element, attrs) {
            // const MIN_Y = -135;
            // const MIN_X = -250;

            // let maximoSalas = scope.$parent.andar.QuantidadeMaximaSalas;
            // let indice = scope.$parent.$index;
            
            // let x = 100;
            // let y = 65;

            // let abcissa = scope.buscarAbcissa();
            // let ordenada = scope.buscarAltura();
            
            // element.attr("style",`transform: translate(${(abcissa * x) + MIN_X}%, ${(ordenada == 100 ? ordenada - y : MIN_Y)}%);`)

            const MIN_Y = 15;
            const MIN_X = 0;

            let maximoSalas = scope.$parent.andar.QuantidadeMaximaSalas;
            let indice = scope.$parent.$index;
            
            let x = 20;
            let y = 20;
            
            // console.log(scope.$parent.sala);
            scope.$parent.tooltip = ""; 
            scope.$parent.tooltip = ("Sala: " + scope.$parent.sala.NumeroSala + "\n"+ "Slots: " + scope.$parent.sala.QuantidadeMaximaSlots); 
            console.log(scope.$parent.tooltip)

            let abcissa = scope.buscarAbcissa();
            let ordenada = scope.buscarAltura();
     
            element.attr("style",`left: ${(abcissa * x) + MIN_X}%; top: ${(ordenada == 80 ? ordenada - y : MIN_Y)}%;`)



        }
    }
}])