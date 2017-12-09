angular.module('app').directive('salaAndar', ['$compile', function($compile) {
	return {
        restrict: 'A',
        scope: {
            dummyData: "=",
            hoverSala: "="
        },
        link: function (scope, element, attrs) {
            scope.elementId = element.attr("id");
            scope.salaClick = function () {
                alert(scope.dummyData[scope.elementId].value);
            };
            scope.salaMouseOver = function () {
                scope.hoverSala = scope.elementId;
                element[0].parentNode.appendChild(element[0]);
            };
            element.attr("ng-click", "salaClick()");
            element.attr("ng-mouseover", "salaMouseOver()");
            element.attr("ng-class", "{active:hoverSala==elementId}");
            element.removeAttr("sala-andar");
            $compile(element)(scope);
        }
    }
}])