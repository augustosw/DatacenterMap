angular.module('app').controller('EdificacaoController', function ($scope, edificacaoService) {

    $scope.criar = function (edificacao) {
        edificacaoService.criar(edificacao) //chama o método de post da service
            .then(
            function (response) {
                console.log(response);
            },
            function (response) {
                console.log(response);
            });
    }

});