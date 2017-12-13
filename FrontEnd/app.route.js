angular.module('app').config(function ($routeProvider) {

    $routeProvider
        // públicas
        // .when('/cadastro', {
        //     controller: 'CadastroController',
        //     templateUrl: 'cadastro/cadastro.html'
        // })
        .when('/login', {
            controller: 'LoginController',
            templateUrl: 'login/login.html'
        })

        .when('/edificacao/:id?', {
            controller: 'EdificacaoController',
            templateUrl: 'edificacao/edificacao.html',
            resolve: {
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/andar/:id?', {
            controller: 'AndarController',
            templateUrl: 'andar/andar.html',
            resolve: {
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/sala', {
            controller: 'SalaController',
            templateUrl: 'sala/sala.html',
            resolve: {
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/rack', {
            controller: 'RackController',
            templateUrl: 'rack/rack.html',
            resolve: {
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })
        .when('/equipamento', {
            controller: 'EquipamentoController',
            templateUrl: 'equipamento/equipamento.html',
            resolve: {
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .otherwise('/login');
});
