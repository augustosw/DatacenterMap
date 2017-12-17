angular.module('app')
.directive('mapRackCadastro', function (authService, edificacaoService, rackService, andarService, $rootScope, $log, $timeout, toastr) {

  return {

    restrict: 'E',

    scope: {
        sala: '=',
        slot: '='
    },
    
    templateUrl: 'rack-cadastro/rack-cadastro.html', 

    controller: function ($scope,  $log, $mdSidenav) {
      
    $scope.criar = criar;
    
   
    function criar(rack) {
      if (validar(rack)) {
        rack.SlotId = $scope.slot.Id;
        rackService.criar(rack)
                    .then(
                      function (response) {
                        $scope.slot.Ocupado = true;
                        toastr.success('Novo rack cadastrado!', {
                          iconClass: 'toast-sucesso'
                        });
                        location.reload();
                      },
                      function(response){
                        toastr.error(response.data.join(" - "), 'Falha na solicitação!', {
                          iconClass: 'toast-erro'
                        });
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
          toastr.error(mensagens.join(' - '), 'Falha na solicitação!', {
            iconClass: 'toast-erro'
          });
          return false;
      }

      if(rack.QuantidadeGavetas < 1)
        mensagens.push('A quantidade de gavetas deve ser positiva'); 

      if(!rack.Descricao)
        mensagens.push('Rack deve possuir uma descrição');
      
      if(rack.Tensao < 1)
        mensagens.push('Rack deve possuir uma tensão válida');
      
      if(mensagens.length >= 1){
        toastr.error(mensagens.join(' - '), 'Falha na solicitação!', {
          iconClass: 'toast-erro'
        });
        return false;
      }
      return true
    }
    
    $scope.close = function () {
      $mdSidenav('rack').close()
        .then(function () {
        });
    };
        

}}});