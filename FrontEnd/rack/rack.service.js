// Service de racks
angular.module('app').factory('rackService', function ($http, $location) {
	let url = $location.absUrl() + "/rack";

    function criar(rack){
        return $http.post(url, rack);
    }
    function exluir(id){
        return $http.delete(url + id);
    }
    return {
        criar: criar,
        excluir: excluir
    }
});   