angular.module('app').directive('salaAndar', ['$compile', function($compile, $location) {
	return {
        restrict: 'A',
        scope: {},
        link: function (scope, element, attrs) {
            console.log(scope);
            // element.attr("transform", "translate( {{ $index * 300 }} 36)");
            // $compile(element)(scope);
        }
    }
}])