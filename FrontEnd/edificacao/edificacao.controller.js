angular.module('app').controller('EdificacaoController', function ($scope, edificacaoService) {


    $scope.criar = criar;
    $scope.andares = [1,2,3,4,5]; //para testes atualmente
    $scope.adicionarAndar = adicionarAndar;


    function criar (edificacao) {
        edificacaoService.criar(edificacao) //chama o método de post da service
            .then(
            function (response) {
                console.log(response);
            },
            function (response) {
                console.log(response);
            });
    }

    function adicionarAndar(indice) {
        $scope.valor = `translateZ(${indice*10}vmin)`;
        return '';  
    }


});