// Service de equipamentos
angular.module('app').factory('equipamentoService', function ($http, $location) {
	let url = $location.absUrl() + "/equipamento";

    function criar(equipamento){
        return $http.post(url, equipamento);
    }
    return {
        criar: criar
    }
});   