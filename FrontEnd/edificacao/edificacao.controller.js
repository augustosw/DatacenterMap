angular.module('app').controller('EdificacaoController', function ($scope, edificacaoService, andarService, $routeParams) {

    $scope.criar = criar;//para testes atualmente
    $scope.adicionarAndar = adicionarAndar;
    $scope.selecionarAndar= selecionarAndar;
    $scope.isAlterar = !!$routeParams.id;
    $scope.isAlterar = false;
    $scope.voltar = voltar;
    $scope.excluir = excluir;
    // TODO: necessário criar dados para poder usar.
    // setup();

    setup();
    function setup() {
        ($scope.isAlterar) ? buscarEdificacaoPorId($routeParams.id) : buscarEdificacaoPorId(2);
    }

    function buscarEdificacaoPorId(id){
        edificacaoService.buscarPorId(id)
                        .then(function(response) {
                            console.log(response.data.dados);
                            $scope.edificacaoSelecionada = response.data.dados;
                            $scope.andaresPadroes = [];
                            $scope.andares = $scope.edificacaoSelecionada.Andares;
                            for(var i = 1; i <= $scope.edificacaoSelecionada.NumeroAndares; i++) {
                                $scope.andaresPadroes.push(i);
                            }
                            $scope.andaresTela = $scope.andaresPadroes;
                            console.log($scope.andares);
                        })
    }

    function buscar(){
        edificacaoService.buscar()
                        .then(function(response) {
                            console.log(response.data.dados);
                            $scope.edificacoes = response.data.dados;
                        })
    }


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

    function adicionarAndar(indice) {
        if($scope.detalhe){
            $scope.andarStyle = {
                transform:"translateZ(15vmin) rotate3d(0,0,1,20deg)",
                opacity:1 }
        }
        else {
            $scope.andarStyle = {
                transform:`translateZ(${indice*10}vmin)`,
                opacity:1
            }
        }
        return '';
    }

    var dataTemp = {};
    function listarSalas(id) {
        andarService.obterPorId(id)
            .then(
            function (response) {
                var salas = response.data.dados.Salas;
                angular.forEach(salas, function (sala, key) {
                    dataTemp[sala.NumeroSala] = sala;
                });
                $scope.dummyData = dataTemp;
                console.log($scope.dummyData);
            },
            function (response) {
                console.log(response);
            });
    }

    function salaClick(id) {
        $location.path("/sala/" + id);
    };

    function selecionarAndar(andar) {
        $scope.andaresTela = [];
        $scope.detalhe = true;
        $scope.andaresTela.push(andar);
        $scope.andarSelecionado = $scope.andares.filter(function (valor){ return valor.NumeroAndar == andar});
        console.log($scope.andarSelecionado);
    }

    function voltar(){
        $scope.detalhe = false;
        $scope.andaresTela = $scope.andaresPadroes;
        $scope.andarSelecionado = [];
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
