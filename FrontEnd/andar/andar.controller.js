angular.module('app').controller('AndarController', function ($scope) {

    $scope.criar = criar;
    $scope.excluir = excluir;

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
    var salas = ["01", "02", "03", "04", "05", "06"];
    $scope.createDummyData = function () {
        var dataTemp = {};
        angular.forEach(salas, function (sala, key) {
            dataTemp[sala] = { numero: "3.1", slots: 5, largura: 10, comprimento: 20 }
        });
        $scope.dummyData = dataTemp;
    };
    $scope.createDummyData();

    $scope.changeHoverSala = function (sala) {
        $scope.hoverSala = sala;
    };
});