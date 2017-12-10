// Service de andares
angular.module('app').factory('andarService', function ($http, $location) {
	let url = $location.absUrl() + "/andar";

    function listar(){
        return $http.get(url);
    }

    function obterPorId(id){
        return $http.get(url + id);
    }

    function criar(andar){
        return $http.post(url, andar);
    }

    function excluir(id){
        return $http.delete(url + id);
    }
    function editar(id, andar){
        return $http.put(url + id, andar);
    }
    return {
        criar: criar,
        excluir: excluir,
        editar: editar,
        listar: listar,
        obterPorId: obterPorId
    }
});   