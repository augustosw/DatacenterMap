// Service de racks
angular.module('app').factory('rackService', function ($http) {
	let url = "http://localhost:9090/api/rack";

    function criar(rack){
        return $http.post(url, rack);
    }
    function exluir(rack){
        return $http.put(url, rack);
    }
    return {
        criar: criar,
        excluir: excluir
    }
});   