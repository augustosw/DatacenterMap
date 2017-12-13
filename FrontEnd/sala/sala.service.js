// Service de salas
angular.module('app').factory('salaService', function ($http, $location) {
    let url = "http://localhost:51641/api/sala/";

    function buscarPorId(id){
        return $http.get(url + id);
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
        excluir: excluir,
        editar: editar,
        buscarPorId: buscarPorId
    }
});   