angular.module('app').controller('EdificacaoController', function ($scope, edificacaoService, $routeParams) {


    $scope.criar = criar;
    $scope.andares = [1,2,3,4,5]; //para testes atualmente
    $scope.adicionarAndar = adicionarAndar;
    $scope.selecionarAndar= selecionarAndar;
    $scope.isAlterar = !!$routeParams.id; 
    $scope.voltar = voltar;

    // TODO: necessário criar dados para poder usar.
    // setup(); 


    // function setup() {
    //     ($scope.isAlterar) ? buscarEdificacaoPorId($routeParams.id) : buscar();
    // }

    // function buscarEdificacaoPorId(id){
    //     edificacaoService.buscarPorId(id)
    //                     .then(function(response) {
    //                         $scope.edificacao = response.data;
    //                     })
    // }

    // function buscar(){
    //     edificacaoService.buscar()
    //                     .then(function(response) {
    //                         $scope.edificacao = response.data;
    //                     })
    // }

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
        if($scope.detalhe){
            $scope.andarStyle = {
                transform:"translateZ(15vmin) rotate3d(0,0,1,20deg)",
                opacity:1 }
        }
        else{
            $scope.andarStyle = {
                transform:`translateZ(${indice*10}vmin)`,
                opacity:1 }
        }
        return '';  
    }

    function selecionarAndar(andar) {
        $scope.andares = [];
        $scope.detalhe = true;
        $scope.andares.push(andar);
        console.log(andar);
    }

    function voltar(){
        $scope.detalhe = false;
        $scope.andares = [1,2,3,4,5]
    }

});