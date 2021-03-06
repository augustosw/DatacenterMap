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
        return $http.get(url + "/disponiveis/" + salaId + '/' + tamanho)
    }

    function buscarRackPorIdSlot(slot) {
        return $http.get(url + '/' + slot.Id + '/slot')
    }

    function excluir(id){
        return $http.delete(url + '/' + id);
    }
    
    function buscarPorRacksPorSlots(slotsId) {
        return $http.post(url + '/by-slots', slotsId)

    }

    return {
        criar: criar,
        buscarRackPorIdSlot:buscarRackPorIdSlot,
        buscarPorIdComRackDisponiveis:buscarPorIdComRackDisponiveis,
        buscarPorId:buscarPorId,
        excluir: excluir,
        buscarPorRacksPorSlots: buscarPorRacksPorSlots
    }
});   