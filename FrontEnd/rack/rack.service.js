// Service de racks
angular.module('app').factory('rackService', function ($http, $location) {
	let url = $location.absUrl() + "/rack";

    function criar(rack){
        return $http.post(url, rack);
    }

    function buscarPorId(id){
        return $http.get(url +'/'+ id);
    }

    function excluir(id){
        return $http.delete(url + id);
    }
    return {
        criar: criar,
        buscarPorId:buscarPorId,
        excluir: excluir
    }
});   