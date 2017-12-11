// Service de edificacoes
angular.module('app').factory('edificacaoService', function ($http, $location) {
    let url = "http://localhost:51641/api/edificacao";

    function criar(edificacao) {
        return $http.post(url, edificacao);
    }

    function excluir(id) {
        return $http.delete(url + '/' + id);
    }

    function buscar() {
        return $http.get(url);
    }

    function buscarPorId(id) {
        return $http.get(url + '/' + id);
    }

    return {
        criar: criar,
        excluir: excluir,
        buscar: buscar,
        buscarPorId: buscarPorId
    }
});
