angular.module('app')
.directive('mapRackCadastro', function (authService, rackService, andarService, $rootScope, $log, $timeout) {

  return {

    restrict: 'E',

    scope: {
        sala: '='
    },
    
    templateUrl: 'rack-cadastro/rack-cadastro.html', 

    controller: function ($scope,  $log, $mdSidenav) {
      
    $scope.criar = criar;
    
    function criar(rack) {
      console.log($scope.slot);
      if (validar(rack)) {
        rack.SlotId = $scope.slot.Id;
        rackService.criar(rack)
                    .then(
                      function (response) {
                        $scope.slot.Ocupado = true;
                        console.log(response.data);
                    },
                      function(response){
                        console.log(response);
                        alert(response.data);  
                        })
      }
      else {
          $scope.enviar = true;    
      }
    } 


    function validar(rack) {
      var mensagens = [];
      if ($scope.cadastroRackForm.$invalid && $scope.cadastroRackForm.$submitted) {
          mensagens.push('Formulário Inválido');
          alert("Falha na solicitação!" + mensagens.join(' - '));
          return false;
      }

      if(rack.QuantidadeGavetas < 1)
        mensagens.push('A quantidade de gavetas deve ser positiva'); 

      if(!rack.Descricao)
        mensagens.push('Rack deve possuir uma descrição');
      
      if(rack.Tensao < 0)
        mensagens.push('Rack deve possuir uma tensão válida');
      
      if(mensagens.length >= 1){
        alert("Falha na solicitação!" + mensagens.join(' - ')); 
        return false;
      }
      return true
    }
    
    $scope.close = function () {
      $mdSidenav('andar').close()
        .then(function () {
        });
    };
        

}}});