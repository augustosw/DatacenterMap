// Service de edificacoes
angular.module('app').factory('edificacaoService', function ($http, $location) {
	let url = $location.absUrl() + "/edificacao";

    function criar(edificacao){
        return $http.post(url, edificacao);
    }
    function excluir(edificacao){
        return $http.put(url, edificacao);
    }
    return {
        criar: criar,
        excluir: excluir
    }
});   