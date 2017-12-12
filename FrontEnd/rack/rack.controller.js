angular.module('app').controller('RackController', function ($scope, rackService, equipamentoService, $routeParams) {
    
 
    $scope.tamanho = [] 
    $scope.equipamento = {};
    $scope.aumentarTamanho = aumentarTamanho;
    $scope.criarEquipamento = criarEquipamento; 
    $scope.posicaoInvalida = posicaoInvalida;
    $scope.deletarEquipamento = deletarEquipamento;
    $scope.verificarTamanho = verificarTamanho;
    var tamanhoAtual = 0;

    // setup(); 
    function setup() {
        rackService.buscarPorId($routeParams.id)
                    .then(function(response) {
                        console.log(response.data); 
                        $scope.rack = response.data;
                    })
    }

    function aumentarTamanho(idGaveta, ativo) {
        solicitarCadastro = true;
        if(ativo)
            $scope.tamanho.push(idGaveta);
        else {
            var indice = $scope.tamanho.indexOf(idGaveta);
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
                        height:`${tamanhoAtual*30}px`,
                        background: "#FF8C00"}
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


    // function verificarTamanho(gaveta) {
    //     if(tamanhoAtual === 0){
    //         equipamentoService.buscarPorId(gaveta.equipamentoId)
    //                             .then(function(response) {
    //                                 tamanhoAtual = response.data.tamanho;
    //                                 $scope.tamanhoGaveta = {
    //                                     height:`${tamanhoAtual*50}`
    //                                 }})
    //     }
    //     tamanhoAtual--; 
    // }
    

    function deletarEquipamento(gaveta){
        console.log(gaveta);
        // metodos corretos, porém não há comunicacao com o banco
        // equipamentoService.excluir(gaveta.equipamentoId)
        //                     .then( function (response ) {
        //                         location.reload();
        //                     })
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
            ocupado:true,
            equipamento: {
                id:3,
                tamanho:1
            }
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
    