// Service de equipamentos
angular.module('app').factory('equipamentoService', function ($http, $location) {
	let url = $location.absUrl() + "/equipamento";

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