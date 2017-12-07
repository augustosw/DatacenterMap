// Service de edificacoes
angular.module('app').factory('edificacaoService', function ($http) {
	let url = "http://localhost:9090/api/edificacao";

    function criar(edificacao){
        return $http.post(url, edificacao);
    }
    function exluir(edificacao){
        return $http.put(url, edificacao);
    }
    return {
        criar: criar,
        excluir: excluir
    }
});   