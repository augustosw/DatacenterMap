// Service de andares
angular.module('app').factory('andarService', function ($http, $location) {
	let url = $location.absUrl() + "/andar";

    function criar(andar){
        return $http.post(url, andar);
    }
    function exluir(id){
        return $http.delete(url + id);
    }
    function editar(id, andar){
        return $http.put(url + id, andar);
    }
    return {
        criar: criar,
        excluir: excluir,
        editar: editar
    }
});   