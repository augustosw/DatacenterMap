// Service de andares
angular.module('app').factory('andarService', function ($http, $location) {
	let url = "http://localhost:51641/api/andar/";

    function listar(){
        return $http.get(url);
    }

    //método utilizado pela controller de rack para encontrar andares com equipamentos disponiveis
    function buscarPorIdComRackDisponiveis(edificacaoId, tamanho){
        return $http.get(url + "disponiveis/" + edificacaoId + "/" + tamanho);
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