angular.module('app').controller('EdificacaoController', function ($scope, edificacaoService) {

    $scope.criar = criar;
    $scope.excluir = excluir;

    function criar(edificacao) {
        edificacaoService.criar(edificacao) //chama o método de post da service
            .then(
            function (response) {
                console.log(response);
            },
            function (response) {
                console.log(response);
            });
    }


    function excluir(edificacao) {
        edificacaoService.excluir(edificacao) //chama o método de delete da service
            .then(
            function (response) {
                console.log(response);
            },
            function (response) {
                console.log(response);
            });
    }

});