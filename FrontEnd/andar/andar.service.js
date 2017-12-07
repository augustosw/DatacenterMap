// Service de andares
angular.module('app').factory('andarService', function ($http) {
	let url = "http://localhost:9090/api/andar";

    function criar(andar){
        return $http.post(url, andar);
    }
    function exluir(andar){
        return $http.put(url, andar);
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