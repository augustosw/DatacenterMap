angular.module('app').controller('RackController', function ($scope, rackService, salaService, equipamentoService, $routeParams, edificacaoService, andarService, toastr) {
    
 
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
    $scope.gavetaEstaEntreGavetas = gavetaEstaEntreGavetas;

    var tamanhoAtual = 0;

    setup(); 
    function setup() {
        rackService.buscarPorId($routeParams.id)
                    .then(function(response) {
                        $scope.rack = response.data;
                    })
    }

    function editar(gaveta){
        $scope.isAlterar = true;
        $scope.equipamentoEdicao = gaveta.Equipamento;
    }

    function aumentarTamanho(gaveta, ativo) {
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
        if (true) {      
            equipamento.GavetasId = [];
            equipamento.Tamanho = $scope.tamanho.length; 
            $scope.tamanho.forEach(x => equipamento.GavetasId.push(x));
            equipamentoService.criar(equipamento)
                                .then(function (response) {
                                    toastr.success('Novo equipamento cadastrado!', {
                                      iconClass: 'toast-sucesso'
                                    });
                                    location.reload();
                                }) 
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

    function gavetaEstaEntreGavetas(idGaveta) {
        let array = ordenarPorGaveta($scope.tamanho); 
        return !(array[array.length - 1] === idGaveta || 
            array[0] === idGaveta)
    }

    function ordenarPorGaveta(gavetasSelecionadas) {
        return gavetasSelecionadas.sort(function(a, b) { 
            return a - b;
        });
      }

    function verificarTamanho(gaveta) {

        if(gaveta.Equipamento){
            if(tamanhoAtual === 0){
                    tamanhoAtual = gaveta.Equipamento.Tamanho;
                    $scope.tamanhoGaveta = {
                        height:`${tamanhoAtual*53 -1}px`,
                        // background: "rgba(173, 170, 166, 0.5)"
                    }
                    $scope.tooltip = ""; 
                    $scope.tooltip = ("Descrição: " + gaveta.Equipamento.Descricao + "\n"+ "Tamanho: " + gaveta.Equipamento.Tamanho + "\n" 
                                    + "Tensão: " + gaveta.Equipamento.Tensao + "V"); 

                    
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
                        })
    }

    function buscarSalas(equipamentoEdicao, andar) {
        salaService.buscarPorIdComRackDisponiveis(andar, equipamentoEdicao.Tamanho)
                    .then(function(response) {
                        $scope.salas = response.data;
                    }) 
    }

    function buscarRacks(equipamentoEdicao, sala) {
        rackService.buscarPorIdComRackDisponiveis(sala, equipamentoEdicao.Tamanho)
                    .then(function (response) {
                        $scope.racks = response.data;
                    })
    }

    function moverEquipamento(equipamentoMover) {
        equipamentoService.moverEquipamento(equipamentoMover.RackMover, equipamentoMover.Id)
                            .then(function (response) {
                                toastr.success('Equipamento movido', {
                                  iconClass: 'toast-sucesso'
                                });
                                $scope.racks = response.data.Racks; 
                                location.reload();
                            })
    }


    function deletarEquipamento(equipamento){
        equipamentoService.excluir(equipamento.Id)
                            .then(function (response) {
                                toastr.success('Equipamento excluído', {
                                  iconClass: 'toast-sucesso'
                                });
                                location.reload();
                            })
    }
    
});
    