angular.module('app').controller('RackController', function ($scope, rackService, equipamentoService, $routeParams) {
    
 
    $scope.tamanho = [] 
    $scope.equipamento = {};
    $scope.aumentarTamanho = aumentarTamanho;
    $scope.criarEquipamento = criarEquipamento; 
    $scope.posicaoInvalida = posicaoInvalida;
    $scope.deletarEquipamento = deletarEquipamento;

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
                id:1
            }
        },
        {
            id:3,
            ocupado:true,
            equipamento: {
                id:1
            }
        },
        {
            id:4,
            ocupado:false
        },
        {
            id:5,
            ocupado:false
        },
        {
            id:6,
            ocupado:false
        },
        {
            id:7,
            ocupado:false
        }
    ];  
    
});
    