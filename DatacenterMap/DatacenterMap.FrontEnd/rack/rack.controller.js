angular.module('app').controller('RackController', function ($scope, rackService, salaService, equipamentoService, $routeParams, edificacaoService,
                                andarService) {
    
 
    $scope.tamanho = [] 
    $scope.equipamento = {};
    $scope.aumentarTamanho = aumentarTamanho;
    $scope.deletarEquipamento = deletarEquipamento;
    $scope.isAlterar = false;
    $scope.editar = editar; 
    $scope.criarEquipamento = criarEquipamento; 
    $scope.posicaoInvalida = posicaoInvalida;
    $scope.equipamentoEdicao = {};
    $scope.deletarEquipamento = deletarEquipamento;
    $scope.verificarTamanho = verificarTamanho;
    $scope.buscarAndares = buscarAndares;
    $scope.buscarSalas = buscarSalas;
    $scope.buscarRacks = buscarRacks;
    $scope.moverEquipamento = moverEquipamento;

    var tamanhoAtual = 0;

    setup(); 
    function setup() {
        rackService.buscarPorId($routeParams.id)
                    .then(function(response) {
                        console.log(response.data); 
                        $scope.rack = response.data;
                    })
    }

    function editar(gaveta){
        $scope.isAlterar = true;
        $scope.equipamentoEdicao = gaveta.Equipamento;
    }

    function aumentarTamanho(gaveta, ativo) {
        console.log(gaveta);
        if(gaveta.Ocupado){
            editar(gaveta);
            return;
        }
        $scope.isAlterar = false;
        solicitarCadastro = true;
        if(ativo)
            $scope.tamanho.push(gaveta.Id);
        else {
            var indice = $scope.tamanho.indexOf(gaveta.Id);
            if (indice > -1) {
            $scope.tamanho.splice(indice, 1);
            }
        }
    }

    function criarEquipamento(equipamento){
        console.log(equipamento);
        if (true) {      
            equipamento.GavetasId = [];
            equipamento.Tamanho = $scope.tamanho.length; 
            $scope.tamanho.forEach(x => equipamento.GavetasId.push(x));
            console.log(equipamento);
            equipamentoService.criar(equipamento)
                                .then(response => console.log(response.data)) 
          }
          else {
             $scope.enviar = true;    
          }
        }

    function posicaoInvalida(idGaveta){
        return !($scope.tamanho[$scope.tamanho.length - 1] - idGaveta === 1  || 
                 $scope.tamanho[$scope.tamanho.length - 1] - idGaveta === -1 ||
                 $scope.tamanho.some(x => x === idGaveta)                    || 
                 $scope.tamanho.length === 0                                 ||
                 validarSeNumeroEstaEntreArray(idGaveta))
    }

    function validarSeNumeroEstaEntreArray(idGaveta){
        return $scope.tamanho.some(x => x + 1 === idGaveta || x - 1 === idGaveta);
    }

    function verificarTamanho(gaveta) {
        if(gaveta.Equipamento){
            if(tamanhoAtual === 0){
                    tamanhoAtual = gaveta.Equipamento.Tamanho;
                    $scope.tamanhoGaveta = {
                        height:`${tamanhoAtual*53 -1}px`,
                        // background: "rgba(173, 170, 166, 0.5)"
                    }
            } 
            else {
                $scope.tamanhoGaveta = { display: "none" } 
            }
        tamanhoAtual--;
        }
        else {
            $scope.tamanhoGaveta = { height: "30px" }
        }
    }


    function buscarAndares(equipamentoEdicao) {
        andarService.buscarPorIdComRackDisponiveis(1, equipamentoEdicao.Tamanho)
                        .then(function(response) { 
                            $scope.andares = response.data;
                            console.log(response.data); 
                        })
    }

    function buscarSalas(equipamentoEdicao, andar) {
        console.log('teste');
        salaService.buscarPorIdComRackDisponiveis(andar, equipamentoEdicao.Tamanho)
                    .then(function(response) {
                        console.log(response);
                        $scope.salas = response.data;
                    }) 
    }

    function buscarRacks(equipamentoEdicao, sala) {
        console.log(equipamentoEdicao.Tamanho);
        console.log(sala);
        rackService.buscarPorIdComRackDisponiveis(sala, equipamentoEdicao.Tamanho)
                    .then(function (response) {
                        $scope.racks = response.data;
                    })
    }

    function moverEquipamento(equipamentoMover) {
        console.log(equipamentoMover);
        equipamentoService.moverEquipamento(equipamentoMover.RackMover, equipamentoMover.Id)
                            .then(function (response) {
                                $scope.racks = response.data.Racks; 
                                location.reload();
                            })
    }


    function deletarEquipamento(equipamento){
        console.log(equipamento.Id);
        equipamentoService.excluir(equipamento.Id)
                            .then(function (response) {
                                location.reload();
                            })
    }
    
});
    