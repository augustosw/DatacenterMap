angular.module('app').config(function ($routeProvider) {

    $routeProvider
        // públicas
        // .when('/cadastro', {
        //     controller: 'CadastroController',
        //     templateUrl: 'cadastro/cadastro.html'
        // })
        // .when('/login', {
        //     controller: 'LoginController',
        //     templateUrl: 'login/login.html'
        // })
        // privadas
        .when('/edificacao', {
            controller: 'EdificacaoController',
            templateUrl: 'edificacao/edificacao.html',
            //TODO : Implementar autenticação
            // resolve: {
            //     // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
            //     autenticado: function (authService) {
            //         return authService.isAutenticadoPromise();
            //     }
            // }
        })

        .when('/edificacao/:id?', {
            controller: 'EdificacaoController',
            templateUrl: 'edificacao/edificacao.html',
           //TODO : Implementar autenticação
            // resolve: {
            //     // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
            //     autenticado: function (authService) {
            //         return authService.isAutenticadoPromise();
            //     }
            // }
        })
               
        .when('/sala', {
            controller: 'SalaController',
            templateUrl: 'sala/sala.html',
            //TODO : Implementar autenticação
            // resolve: {
            //     // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
            //     autenticado: function (authService) {
            //         return authService.isAutenticadoPromise();
            //     }
            // }
        })
        
        .when('/rack', {
            controller: 'RackController',
            templateUrl: 'rack/rack.html',
            //TODO : Implementar autenticação
            // resolve: {
            //     // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
            //     autenticado: function (authService) {
            //         return authService.isAutenticadoPromise();
            //     }
            // }
        })
        .when('/equipamento', {
            controller: 'EquipamentoController',
            templateUrl: 'equipamento/equipamento.html',
            //TODO : Implementar autenticação
            // resolve: {
            //     // define que para acessar esta página deve ser um usuário autenticado (mas não restringe o tipo de permissão)
            //     autenticado: function (authService) {
            //         return authService.isAutenticadoPromise();
            //     }
            // }
        })

        .otherwise('/edificacao');
});