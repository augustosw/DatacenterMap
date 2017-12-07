// Service de salas
angular.module('app').factory('salaService', function ($http, $location) {
	let url = $location.absUrl() + "/equipamento";

    function criar(sala){
        return $http.post(url, sala);
    }
    function exluir(sala){
        return $http.put(url, sala);
    }
    function editar(id, sala){
        return $http.put(url + id, sala);
    }
    return {
        criar: criar,
        excluir: excluir,
        editar: editar
    }
});   