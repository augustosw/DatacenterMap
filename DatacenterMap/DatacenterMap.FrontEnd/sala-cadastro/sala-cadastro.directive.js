angular.module('app')
.directive('mapSalaCadastro', function (authService, salaService, edificacaoService, $rootScope, $log, $timeout, toastr) {

  return {

    restrict: 'E',

    scope: {
        edificacao:'=',
        andar: '='
    },
    
    templateUrl: 'sala-cadastro/sala-cadastro.html', 

    controller: function ($scope, $log, $mdSidenav ) {

        $scope.criar = criar;
        
        function criar(sala) {
            if (validar(sala)) {
                sala.AndarId = $scope.andar[0].Id;
                salaService.criar(sala)
                            .then(
                                function (response) {
                                    $scope.andar[0].Salas.push(response.data);
                                    toastr.success('Nova sala cadastrada!', {
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

        
        function validar(sala) {
            var mensagens = [];
            if ($scope.cadastroSalaForm.$invalid && $scope.cadastroSalaForm.$submitted) {
                mensagens.push('Formulário Inválido');
                toastr.error(mensagens.join(' - '), 'Falha na solicitação!', {
                  iconClass: 'toast-erro'
                });
                return false;
            }
            
            if($scope.andar[0].Salas.length === $scope.andar[0].QuantidadeMaximaSalas){
                mensagens.push("Esse andar antigiu o limite máximo de salas disponíveis. Por favor, tente cadastrar em outro andar.")
            }

            if(sala.NumeroSala < 1)
                mensagens.push("O Numero de sala não deve ser nulo."); 
        
            if(sala.Largura < 1 )
                mensagens.push("A Largura não deve ser nula.");
            
            if(sala.Comprimento < 1)
                mensagens.push("O comprimento não deve ser nulo.");

            if(mensagens.length >= 1){
                toastr.error(mensagens.join(' - '), 'Falha na solicitação!', {
                  iconClass: 'toast-erro'
                });
                return false;
            }
            return true
        }
        
        
        
        $scope.close = function () {
            $mdSidenav('sala').close()
            .then(function () {
            });
        };
            
      

}}});