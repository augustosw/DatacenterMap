angular.module('app').controller('AndarController', function ($scope, $location, $routeParams, andarService) {

    $scope.criar = criar;
    $scope.excluir = excluir;
    $scope.salaClick = salaClick;

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

    var dataTemp = {};
    function listarSalas() {
        andarService.obterPorId($routeParams.id)
            .then(
            function (response) {
                var salas = response.data.dados.Salas;
                angular.forEach(salas, function (sala, key) {
                    dataTemp[sala.NumeroSala] = sala;
                });
                $scope.dummyData = dataTemp;
                console.log($scope.dummyData);
            },
            function (response) {
                console.log(response);
            });
    }

    function salaClick(id) {
        $location.path("/sala/" + id);
    };

    listarSalas();

    $scope.changeHoverSala = function (sala) {
        $scope.hoverSala = sala;
    };
});