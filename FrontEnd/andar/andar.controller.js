angular.module('app').controller('AndarController', function ($scope, $routeParams, andarService, salaService) {

    $scope.criar = criar;
    $scope.excluir = excluir;
    $scope.dummyData = [];

    function criar(andar) {
        andarService.criar(andar) //chama o método de post da service
            .then(
            function (response) {
                console.log(response);
            },
            function (response) {
                console.log(response);
            });
    }

    function excluir(andar) {
        andarService.excluir(andar) //chama o método de delete da service
            .then(
            function (response) {
                console.log(response);
            },
            function (response) {
                console.log(response);
            });
    }

    function listarSalas () {
        andarService.obterPorId($routeParams.id)
        .then(
            function (response) {
                console.log(response.data);
                $scope.dummyData = response.data.salas;
            },
            function (response) {
                console.log(response);
            });
    }

    listarSalas();
    // $scope.createDummyData = function () {
    //     angular.forEach(salasIds, function (sala, key) {
    //         dataTemp[sala] = { "numeroSala": "3.1", "slots": 5, "largura": 10, "comprimento": 20 }
    //     });
    //     $scope.dummyData = dataTemp;
    // };
    // $scope.createDummyData();

    $scope.changeHoverSala = function (sala) {
        $scope.hoverSala = sala;
    };
});