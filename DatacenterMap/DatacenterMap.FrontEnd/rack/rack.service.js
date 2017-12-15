// Service de racks
angular.module('app').factory('rackService', function ($http, $location) {
	let url = "http://localhost:51641/api/rack";

    function criar(rack){
        return $http.post(url, rack);
    }

    function buscarPorId(id){
        return $http.get(url +'/'+ id);
    }

    function buscarPorIdComRackDisponiveis(salaId, tamanho){
        return $http.get(url + "disponiveis/" + salaId + tamanho)
    }

    function buscarRackPorIdSlot(slot) {
        return $http.get(url + '/' + slot.Id + '/slot')
    }

    function moverEquipamento(idRack, idEquipamento) {
        return $http.put(url + idRack + '/' + idEquipamento)
    }

    function excluir(id){
        return $http.delete(url + id);
    }
    return {
        criar: criar,
        buscarRackPorIdSlot:buscarRackPorIdSlot,
        moverEquipamento:moverEquipamento,
        buscarPorIdComRackDisponiveis:buscarPorIdComRackDisponiveis,
        buscarPorId:buscarPorId,
        excluir: excluir
    }
});   