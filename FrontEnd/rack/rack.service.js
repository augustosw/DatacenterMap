// Service de racks
angular.module('app').factory('rackService', function ($http, $location) {
	let url = $location.absUrl() + "/rack";

    function criar(rack){
        return $http.post(url, rack);
    }

    function buscarPorId(id){
        return $http.get(url +'/'+ id);
    }

    function buscarPorIdComRackDisponiveis(salaId, tamanho){
        return $http.get(url + "disponiveis/" + salaId + tamanho)
    }

    function moverEquipamento(idRack, idEquipamento) {
        return $http.put(url + idRack + '/' + idEquipamento)
    }

    function excluir(id){
        return $http.delete(url + id);
    }
    return {
        criar: criar,
        moverEquipamento:moverEquipamento,
        buscarPorIdComRackDisponiveis:buscarPorIdComRackDisponiveis,
        buscarPorId:buscarPorId,
        excluir: excluir
    }
});   