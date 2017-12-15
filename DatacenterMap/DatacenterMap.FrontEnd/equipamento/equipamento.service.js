// Service de equipamentos
angular.module('app').factory('equipamentoService', function ($http, $location) {

	let url = "http://localhost:51641/api/equipamento";

    function criar(equipamento){
        return $http.post(url, equipamento);
    }

    function excluir(id){
        return $http.delete(url +'/'+ id);
    }

    
    function moverEquipamento(idRack, idEquipamento) {
        return $http.put(url + '/' + idRack + '/' + idEquipamento)
    }
    
    return {
        criar: criar,
        moverEquipamento:moverEquipamento
    }
});   