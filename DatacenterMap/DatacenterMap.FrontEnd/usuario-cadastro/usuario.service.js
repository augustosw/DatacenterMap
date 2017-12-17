// Service de usuario
angular.module('app').factory('usuarioService', function ($http, $location) {
    
        let url = "http://localhost:51641/api/usuario";
    
        function criar(usuario){
            return $http.post(url, usuario);
        }
        return {
            criar: criar
        }
    });   