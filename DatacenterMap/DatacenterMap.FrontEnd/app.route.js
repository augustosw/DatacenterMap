angular.module('app').config(function ($routeProvider) {

    $routeProvider
        //públicas
        .when('/cadastro', {
            controller: 'CadastroController',
            templateUrl: 'usuario-cadastro/usuario-cadastro.html'
        })

        .when('/login', {
            controller: 'LoginController',
            templateUrl: 'login/login.html'
        })

        .when('/edificacao/:id?', {
            controller: 'EdificacaoController',
            templateUrl: 'edificacao/edificacao.html',
            resolve: {
                // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/andar/:id?', {
            controller: 'AndarController',
            templateUrl: 'andar/andar.html',
            resolve: {
                // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/sala/:id?', {
            controller: 'SalaController',
            templateUrl: 'sala/sala.html',
            resolve: {
                // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/rack', {
            controller: 'RackController',
            templateUrl: 'rack/rack.html',
            resolve: {
                // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/rack/:id?', {
            controller: 'RackController',
            templateUrl: 'rack/rack.html',
            resolve: {
                // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/equipamento', {
            controller: 'EquipamentoController',
            templateUrl: 'equipamento/equipamento.html',
            resolve: {
                // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })

        .when('/equipamento/:id?', {
            controller: 'EquipamentoController',
            templateUrl: 'equipamento/equipamento.html',
            resolve: {
                // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
                autenticado: function (authService) {
                    return authService.isAutenticadoPromise();
                }
            }
        })


        .otherwise('/login');
});
