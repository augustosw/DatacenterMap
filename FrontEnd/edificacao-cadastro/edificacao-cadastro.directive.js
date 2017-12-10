angular.module('app')
.directive('mapEdificacaoCadastro', function (authService, edificacaoService, $rootScope, $log, $timeout) {

  return {

    restrict: 'E',

    scope: {},
    
    templateUrl: 'edificacao-cadastro/edificacao-cadastro.html', 

    controller: function ($scope ) {

      autoComplete();

      var origem;
      var destino = {lat: -29.794918, lng: -51.146509}

      $scope.enviar = false;
      $scope.criar = criar;
      $scope.fecharJanela = fecharJanela;
      $scope.edificacao = {};
      $scope.isCheio = false;
      $scope.edificacao.numeroAndares = 1;
      $scope.andares = [0];
      $scope.adicionar = adicionar;
      $scope.remover = remover;
      $scope.adicionarAndar = adicionarAndar;

      function criar(edificacao) {
        if ($scope.cadastroEdificacaoForm.$valid) {      
          // edificacaoService.criar(edificacao);
          console.log(edificacao);
          location.reload();
        }
        else {
           $scope.enviar = true;    
        }
      }

      function adicionar(){
        $scope.edificacao.numeroAndares++;
        $scope.andares.push($scope.edificacao.numeroAndares);
      }
      
      function remover(){
        if($scope.edificacao.numeroAndares == 1)
          return;
        $scope.edificacao.numeroAndares--;
        $scope.andares.splice(-1,1);
      }

      function adicionarAndar(indice) {
            if(indice > 14){
              $scope.isCheio = true;
              return;
            }
            $scope.isCheio = false;
            $scope.adicionando = {
                transform:`rotateX(70deg) rotateZ(-45deg) translateZ(${indice + 2}vmin)`,
                opacity:1 
            }
        return; 
      }

      function fecharJanela(){
        $scope.fechar = {
          display: "none"
        }
      }


      //api google maps autocomplete


      function autoComplete() {
        $scope.autocomplete = new google.maps.places.Autocomplete(
            (document.getElementById('autocomplete'))
        );
        $scope.autocomplete.addListener('place_changed', obterCoordenadas);
      }

      function obterCoordenadas() {
        var place = $scope.autocomplete.getPlace();
        $scope.edificacao.latitude = place.geometry.location.lat();
        $scope.edificacao.longitude = place.geometry.location.lng();
        console.log(place);
        return;
      }




       
    }
  }

});