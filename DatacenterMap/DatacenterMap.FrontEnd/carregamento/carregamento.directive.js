angular.module('app').directive('carregamento', function ($http) {
		return {
			restrict: 'A',
			link: function (scope, elem) {
				scope.isLoading = isLoading;

				scope.$watch(scope.isLoading, toggleElement);

				function toggleElement(loading) {
					if (loading) {
						elem.append();
					} else {
						elem.remove();
					}
				}

				function isLoading() {
					return $http.pendingRequests.length > 0;
				}
			}
		};

	});
