angular.module('app')
.directive('mapEdificacaoCadastro', function (authService, edificacaoService, $rootScope, $log, $location, $timeout, toastr) {

  return {

    restrict: 'E',
    
    templateUrl: 'edificacao-cadastro/edificacao-cadastro.html', 
    scope: {
      onClose: "="
    },
    link: function (scope, element, attrs) {
    // controller: function (scope ) {

      autoComplete();

      var origem;
      var destino = {lat: -29.794918, lng: -51.146509}

      scope.enviar = false;
      scope.criar = criar;
      scope.fecharJanela = fecharJanela;
      scope.edificacao = {};
      scope.isCheio = false;
      scope.edificacao.numeroAndares = 1;
      scope.andares = [0]; //variavel usada para fazer manipulação de dados na tela
      scope.adicionar = adicionar;
      scope.remover = remover;
      scope.adicionarAndar = adicionarAndar;
      scope.adicionarDigitados = adicionarDigitados;
      scope.numeroAndares = 1; 
      //variavel para manipular na tela

      function criar(edificacao) {
        edificacao.NumeroAndares = scope.andares.length;
        if (validar(edificacao)) {
          edificacaoService.criar(edificacao)
                            .then(function(response) {
                              toastr.success('Nova edificação criada!', {
                                iconClass: 'toast-sucesso'
                              });
                              let novaEdificacaoId = response.data.Id; 
                              $location.path(`/edificacao/${novaEdificacaoId}`);
                              }, function(response) {
                                toastr.error(response.data.join(" - "), 'Falha na solicitação!', {
                                  iconClass: 'toast-erro'
                                });
                              })
        }
        else {
           scope.enviar = true;    
        }
      }

      function adicionar(){
        // Essa array deve se preocupar em somente aumentar o numero de andares
        scope.andares.push(Math.random(10));
      }
      
      function remover(){
        if(scope.andares.length == 1)
          return;
        scope.andares.splice(-1,1);
      }

      function adicionarAndar(indice) {
        scope.numeroAndares = indice + 1;
        if(atingiuLimiteDeAndaresTela(indice))
          return;
        scope.isCheio = false;
        scope.adicionando = {
              transform:`rotateX(70deg) rotateZ(-45deg) translateZ(${indice + 2}vmin)`,
              opacity:1 
        }
        return; 
      }

      function adicionarDigitados(andaresDigitados) {
        if(andaresDigitados < scope.andares.length){
         var diffAndares =   scope.andares.length - andaresDigitados; 
          for(var i = 0; i < diffAndares; i++)
            remover();
        }
        else {
          for(var i = 1; i < andaresDigitados; i++) 
              adicionar();
        }
        atingiuLimiteDeAndaresTela(andaresDigitados);
      }

      function atingiuLimiteDeAndaresTela(andares){
        if(andares > 14){
            scope.isCheio = true;
            return true;
        }
        return false;
      }

      function fecharJanela(){
        scope.onClose();
      }

      function validar(edificacao) {
        var mensagens = [];
        if (scope.cadastroEdificacaoForm.$invalid && scope.cadastroEdificacaoForm.$submitted) {
            mensagens.push('Formulário Inválido');
            toastr.error(mensagens.join(' - '), 'Falha na solicitação!', {
              iconClass: 'toast-erro'
            });
            return false;
        }
  
        if(!edificacao.Nome)
          mensagens.push('Edificacao deve possuir um nome');

        if(!scope.localizacao)
          mensagens.push("Edificacao deve possuir uma localização."); 
  
        if(edificacao.numeroAndares < 1)
          mensagens.push("Número de andares deve ser positivo.");
        
        if(mensagens.length >= 1){
          toastr.error(mensagens.join(' - '), 'Falha na solicitação!', {
            iconClass: 'toast-erro'
          });
          return false;
        }
        return true
      }
      


      //api google maps autocomplete

      function autoComplete() {
        scope.autocomplete = new google.maps.places.Autocomplete(
            (document.getElementById('autocomplete'))
        );
        scope.autocomplete.addListener('place_changed', obterCoordenadas);
      }

      function obterCoordenadas() {
        var place = scope.autocomplete.getPlace();
        scope.edificacao.Latitude = place.geometry.location.lat();
        scope.edificacao.Longitude = place.geometry.location.lng();
        return;
      }




       
    }
  }

});