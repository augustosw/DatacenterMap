angular.module('app')
.directive('mapAndarCadastro', function (authService, edificacaoService, andarService, $rootScope, $log, $timeout) {

  return {

    restrict: 'E',

    scope: {
        edificacao: '='
    },
    
    templateUrl: 'andar-cadastro/andar-cadastro.html', 

    controller: function  controller($scope,  $log, $mdSidenav) {
      
    $scope.criar = criar;
    
    function criar(andar) {
      console.log($scope.edificacao);
      if (validar(andar)) {
        andar.EdificacaoId = $scope.edificacao.Id;
        andarService.criar(andar)
                    .then(
                      function (response) {
                        $scope.edificacao.Andares.push(response.data);
                        $scope.edificacao.Andares = ordenarPorAndar($scope.edificacao.Andares);
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
    
    
    function ordenarPorAndar(andares) {
      return andares.sort(function(a, b) { 
          return a.NumeroAndar - b.NumeroAndar;
      });
    }


    function validar(andar) {
      var mensagens = [];
      if ($scope.cadastroAndarForm.$invalid && $scope.cadastroAndarForm.$submitted) {
          mensagens.push('Formulário Inválido');
          alert("Falha na solicitação!" + mensagens.join(' - '));
          return false;
      }

      if(andar.NumeroAndar > $scope.edificacao.NumeroAndares)
        mensagens.push("O andar solicitado ultrapassa o limite máximo do prédio."); 

      if($scope.edificacao.Andares.some(x => x.NumeroAndar == andar.NumeroAndar))
        mensagens.push("Já existe este andar no edifício.");
      
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