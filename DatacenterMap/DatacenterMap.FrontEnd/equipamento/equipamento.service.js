// Service de equipamentos
angular.module('app').factory('equipamentoService', function ($http, $location) {

	let url = "http://localhost:51641/api/equipamento";

    function criar(equipamento){
        return $http.post(url, equipamento);
    }

    function excluir(id){
        return $http.delete(url +'/'+ id);
    }
    
    return {
        criar: criar
    }
});   