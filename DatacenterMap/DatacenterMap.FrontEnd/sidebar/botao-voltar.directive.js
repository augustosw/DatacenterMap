angular.module('app')
	.directive('botaoVoltar', function ($location, $compile) {

		return {

			restrict: 'A',
			scope: {
				isHomeOrLogin: '@'
			},
			link: function (scope, element, attrs) {
				const HOME_PATH = new RegExp(/^\/edificacao\/?$/);
				const LOGIN_PATH = new RegExp(/^\/login\/?$/);

				scope.$on('$locationChangeSuccess', function () {
					var location = $location.path();
					scope.isHomeOrLogin = (HOME_PATH.test(location) || LOGIN_PATH.test(location));
					$compile(element)(scope);
				});

				element.on('click', function () {
					window.history.back();
				})
			}

		}

	});