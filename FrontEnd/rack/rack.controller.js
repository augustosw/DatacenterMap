angular.module('app').controller('RackController', function ($scope, rackService, equipamentoService, $routeParams, edificacaoService,
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

    // setup(); 
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
        if(gaveta.ocupado){
            editar(gaveta);
            return;
        }
        $scope.isAlterar = false;
        solicitarCadastro = true;
        if(ativo)
            $scope.tamanho.push(gaveta.id);
        else {
            var indice = $scope.tamanho.indexOf(gaveta.id);
            if (indice > -1) {
            $scope.tamanho.splice(indice, 1);
            }
        }
    }

    function criarEquipamento(equipamento){
        if ($scope.cadastroEquipamentoForm.$valid) {      
            equipamento.gavetasId = [];
            equipamento.tamanho = $scope.tamanho.length; 
            $scope.tamanho.forEach(x => equipamento.gavetasId.push(x));
            console.log(equipamento);
            // Chamar método para criar equipamento 
            // equipamentoService.criar() 
    
            // Chamar método para atualizar gavetas que foram ocupadas 
            // rackService.atualizarGavetas(equipamento.id, gavetasId) 
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
        if(gaveta.equipamento !== undefined){
            if(tamanhoAtual === 0){
                    tamanhoAtual = gaveta.equipamento.tamanho;
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
        let edificacaoAtualId = edificacaoService.buscarIdDeEdificacaoAtual();
        andarService.buscarPorIdComRackDisponiveis(edificacaoAtualId, equipamentoEdicao.Tamanho)
                        .then(function(response) { 
                            $scope.andares = response.data.Andares; 
                        })
    }

    function buscarSalas(equipamentoEdicao, andar) {
        salaService.buscarPorIdComRackDisponiveis(andar.Id, equipamentoEdicao.Tamanho)
                    .then(function(response) {
                        $scope.salas = response.data.Salas;
                    }) 
    }

    function buscarRacks(equipamentoEdicao, sala) {
        equipamentoService.buscarPorIdComRackDisponiveis(sala.Id, equipamentoEdicao.Tamanho)
                    .then(function (response) {
                        $scope.racks = response.data.Racks;
                    })
    }

    function moverEquipamento(equipamentoMover) {
        equipamentoService.moverEquipamento(equipamentoMover.RackMover.Id, equipamentoMover.Id)
                            .then(function (response) {
                                $scope.racks = reponse.data.Racks; 
                            })
    }


    function deletarEquipamento(equipamento){
        console.log(equipamento.Id);
        equipamentoService.excluir(equipamento.Id)
                            .then( function (response ) {
                                location.reload();
                            })
    }

    //dados mokados para teste das funcionalidades

    $scope.gavetas = [
        {
            id:1, 
            ocupado: false
        },
        {
            id:2,
            ocupado:true,
            equipamento: {
                id:1,
                tamanho:3
            }
        },
        {
            id:3,
            ocupado:true,
            equipamento: {
                id:1,
                tamanho:3
            }
        },
        {
            id:4,
            ocupado:true,
            equipamento: {
                id:1,
                tamanho:3
            }
        },
        {
            id:5,
            ocupado:true,
            equipamento: {
                id:2,
                tamanho:2
            }
        },
        {
            id:6,
            ocupado:true,
            equipamento: {
                id:2,
                tamanho:2
            }
        },
        {
            id:7,
            ocupado:false
        },
        {
            id:8,
            ocupado:false
        },
        {
            id:9,
            ocupado:false
        },
        {
            id:10,
            ocupado:false
        },
        {
            id:11,
            ocupado:false
        }
    ];  
    
});
    