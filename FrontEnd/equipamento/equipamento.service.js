// Service de equipamentos
angular.module('app').factory('equipamentoService', function ($http) {
	let url = "http://localhost:9090/api/equipamento";

    function criar(equipamento){
        return $http.post(url, equipamento);
    }
    return {
        criar: criar
    }
});   