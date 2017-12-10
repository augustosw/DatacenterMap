angular.module('app')
.directive('mapEdificacaoCadastro', function (authService, $rootScope, $log, $timeout) {

  return {

    restrict: 'E',

    scope: {},
    
    templateUrl: 'edificacao-cadastro/edificacao-cadastro.html', 

    controller: function ($scope ) {

      $scope.enviar = false;
      $scope.criar = criar;
      $scope.edificacao = {};
      $scope.edificacao.numeroAndares = 1;
      $scope.adicionar = adicionar;
      $scope.remover = remover;

      function criar(edificacao) {
        debugger
        if ($scope.cadastroEdificacaoForm.$valid) {      
          //form is valid
        }
        else {
           $scope.enviar = true;    
        }
      }

      function adicionar(){
        $scope.edificacao.numeroAndares++;
      }
      
      function remover(){
        if($scope.edificacao.numeroAndares==1)
          return
        $scope.edificacao.numeroAndares--;
      }



      google.maps.event.addDomListener(window, 'load', function () {  
        var places = new google.maps.places.Autocomplete(document.getElementById('txtPlaces'));
        google.maps.event.addListener(places, 'place_changed', function () {
            var place = places.getPlace();
            console.log(place);
            var address = place.formatted_address;
            var latitude = place.geometry.location.A;
            var longitude = place.geometry.location.F;
            var mesg = "Address: " + address;
            mesg += "\nLatitude: " + latitude;
            mesg += "\nLongitude: " + longitude;
            alert(mesg);
        });
      });




       
    }
  }

});