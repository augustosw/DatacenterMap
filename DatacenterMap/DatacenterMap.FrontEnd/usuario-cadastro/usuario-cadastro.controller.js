angular.module('app').controller('CadastroController', function ($scope, $location, usuarioService, $routeParams, $mdSidenav) {
    

    $scope.login = login;
    $scope.criar = criar;

    function criar(usuario) {
        if(validar(usuario)){
            usuarioService.criar(usuario)
                            .then( response => $location.path('\login'));
        }
        else {
            return;
        }
    }
    

    function login() {
        $location.path('/login');
    }


    function validar(usuario) {
        var mensagens = [];
        if ($scope.cadastroUsuarioForm.$invalid && $scope.cadastroUsuarioForm.$submitted) {
            mensagens.push('Formulário Inválido');
            alert("Falha na solicitação!" + mensagens.join(' - '));
            return false;
        }

        if($scope.usuario.Senha != $scope.confirmSenha)
            mensagens.push('Senhas devem ser iguais');
  
        if(!(usuario.Email))
          mensagens.push('Email inválido'); 
  
        if(!(usuario.Nome))
          mensagens.push('Nome inválido');
        
        if(usuario.Senha < 1)
          mensagens.push('Senha inválida');
        
        if(mensagens.length >= 1){
          alert("Falha na solicitação!" + mensagens.join(' - ')); 
          return false;
        }
        return true
      }
});