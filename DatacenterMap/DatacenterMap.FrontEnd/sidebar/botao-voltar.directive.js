angular.module('app')
	.directive('botaoVoltar', function ($localStorage) {

		return {

			restrict: 'A',
			link: function (scope, element, attrs) {
				element.on('click', function () {
					window.history.back();
				})
			}

		}

	});