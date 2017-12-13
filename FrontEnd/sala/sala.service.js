// Service de salas
angular.module('app').factory('salaService', function ($http, $location) {
    let url = "http://localhost:51641/api/sala";
    
    function listar(){
        return $http.get(url);
    }

    function obterPorId(id){
        return $http.get(url + id);
    }

    function buscarPorIdComRackDisponiveis(andarId, tamanho){
        return $http.get(url + "disponiveis/" + andarId + tamanho)
    }

    function criar(sala){
        return $http.post(url, sala);
    }
    function excluir(id){
        return $http.delete(url + id);
    }
    function editar(id, sala){
        return $http.put(url + id, sala);
    }
    return {
        criar: criar,
        buscarPorIdComRackDisponiveis:buscarPorIdComRackDisponiveis,
        excluir: excluir,
        editar: editar,
        listar: listar,
        obterPorId: obterPorId
    }
});   