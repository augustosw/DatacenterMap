angular.module('app').directive('andarPlanta', ['$compile', function ($compile) {
    return {
        restrict: 'A',
        templateUrl: 'image/andar.svg',
        link: function (scope, element, attrs) {
            var salaElements = element[0].querySelectorAll('.sala');
            angular.forEach(salaElements, function (path, key) {
                var salaElement = angular.element(path);
                salaElement.attr("sala-andar", "");
                salaElement.attr("dummy-data", "dummyData");
                salaElement.attr("hover-sala", "hoversala");
                $compile(salaElement)(scope);
            })
        }
    }
}]);