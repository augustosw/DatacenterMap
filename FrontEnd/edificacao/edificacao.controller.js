angular.module('app').controller('EdificacaoController', function ($scope, edificacaoService, $location, andarService, $routeParams, $mdSidenav) {

    $scope.criar = criar;
    $scope.adicionarAndarNaTela = adicionarAndarNaTela;
    $scope.selecionarAndar = selecionarAndar;
    $scope.isAlterar = !!$routeParams.id;
    $scope.voltar = voltar;
    $scope.excluir = excluir;
    $scope.tipoEntidade = "edificacao";
    
    setup();
    function setup() {
        ($scope.isAlterar) ? buscarEdificacaoPorId($routeParams.id) : buscar();
    }

    function buscarEdificacaoPorId(id) {
        edificacaoService.buscarPorId(id)
            .then(function (response) {
                $scope.edificacaoSelecionada = response.data;
                // variavel que vai manter o numero de andares cadastrados de uma edificação
                $scope.andaresCadastrados = $scope.edificacaoSelecionada.Andares;

                //variavel que vai manter o(s) andare(s) presentes na tela
                $scope.andaresTela = $scope.andaresCadastrados;

                // variaveel que vai manter o número TOTAL de andares de uma edificação
                $scope.andaresTotais = $scope.edificacaoSelecionada.NumeroAndares;
                buscar();
            })
    }

    function buscar() {
        edificacaoService.buscar()
            .then(function (response) {
                $scope.edificacoes = response.data;
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

    function adicionarAndarNaTela(indice) {
        if ($scope.detalhe) {
            $scope.andarStyle = {
                transform: "translateZ(15vmin) rotate3d(0,0,1,20deg)",
                opacity: 1
            }
        }
        else {
            let distanciaEntreAndares = 50/$scope.edificacaoSelecionada.NumeroAndares;
            $scope.andarStyle = {
                transform: `translateZ(${indice * distanciaEntreAndares}vmin)`,
                opacity: 1
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
                $scope.salas = dataTemp;
                console.log($scope.salas);
            },
            function (response) {
                console.log(response);
            });
    }

    function salaClick(id) {
        $location.path("/sala/" + id);
    }

    function selecionarAndar(andar) {
        $scope.andaresTela = [];
        $scope.detalhe = true;
        $scope.andaresTela.push(andar);
        $scope.andarSelecionado = $scope.andaresCadastrados.filter(function (valor) { return valor.NumeroAndar == andar });
        console.log($scope.andarSelecionado);
    }

    function voltar() {
        $scope.detalhe = false;
        $scope.andaresTela = $scope.andaresCadastrados;
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


    // side-bar andar

    $scope.toggleRightAndar = buildToggler('andar');
    $scope.toggleRightSala = buildToggler('sala')


    $scope.isOpenRightAndar = function(){
      return $mdSidenav('andar').isOpen();
    };

    
    $scope.isOpenRightSala = function(){
        return $mdSidenav('sala').isOpen();
      };
    
    function buildToggler(navID) {
      return function() {
        $mdSidenav(navID)
          .toggle()
          .then(function () {
          });
      };
    }

    $scope.close = function () {
      $mdSidenav().close()
        .then(function () {
        });
    };    

});
