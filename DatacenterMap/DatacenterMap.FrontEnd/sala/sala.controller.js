angular.module('app').controller('SalaController', function ($scope, $location, salaService, $mdDialog, rackService, $routeParams, $mdSidenav) {
	
		$scope.criar = criar;//para testes atualmente
		$scope.limparSlot = limparSlot;
		$scope.isAlterar = !!$routeParams.id;
		$scope.voltar = voltar;
		$scope.deleteClick = deleteClick;
		$scope.excluir = excluir;
		$scope.tipoEntidade = "sala";
		$scope.verificarSlot = verificarSlot;
		// TODO: necessário criar dados para poder usar.
		// setup();


		// $scope.toggleRightRack = buildToggler('rack');
		
		console.log($routeParams.id);
		buscarSalaPorId($routeParams.id);
		
		function buscarSalaPorId(id) {
			salaService.buscarPorId(id)
				.then(function (response) {
					$scope.sala = response.data;
					console.log(response.data);
					$scope.salaSelecionada = response.data;
					$scope.slots = $scope.salaSelecionada.Slots;
					$scope.slotsOcupados = $scope.slots.filter(s => s.Ocupado);
					buscarTensaoSlotsOcupados();
				})
		}	
		function buscarTensaoSlotsOcupados() {
			//para preenchimento da popover
			$scope.slotsOcupados.forEach(slot => rackService.buscarRackPorIdSlot(slot)
				.then(response => slot.Rack = response.data));
		}
	
		function criar(sala) {
			salaService.criar(sala) //chama o método de post da service
				.then(
				function (response) {
					console.log(response);
				},
				function (response) {
					console.log(response);
				});
		}
	
		function voltar() {
			$scope.detalhe = false;
			$scope.andaresTela = $scope.andaresPadroes;
			$scope.andarSelecionado = [];
			$scope.salas = [];
		}
	
		function excluir(ev, sala) {

			var confirm = $mdDialog.confirm()
				  .title('Você tem certeza que deseja excluir essa sala?')
				  .textContent('Todos os items relacionados a mesma serão excluídos.')
				  .ariaLabel('Lucky day')
				  .targetEvent(ev)
				  .ok('Tenho certeza')
				  .cancel('Cancelar');
		
			$mdDialog.show(confirm).then(function() {
				
				salaService.excluir(sala.Id) //chama o método de delete da service
								.then(
							function (response) {
								window.history.back();
							});
			}, function() {
			  return;
			});
		  };

		function deleteClick() {
			//para o evento de click para não ativar div
			if (!e) var e = window.event;
			e.cancelBubble = true;
			if (e.stopPropagation) e.stopPropagation();
		}

		function limparSlot(slotId) {		
			deleteClick();
			rackService.buscarRackPorIdSlot(slotId)
				.then(response => rackService.excluir(response.data.Id).then(location.reload()));
		}

		 // side-bar criação de slot		 

		function verificarSlot(slot) {
			if(!slot.Ocupado) {
				$scope.slot = slot;
				$mdSidenav('rack')
				.toggle()
				.then(function () {
				});
			}
			else {
				console.log(slot);
				rackService.buscarRackPorIdSlot(slot)
							.then(response => $location.path(`/rack/${response.data.Id}`));
			}
		}

		$scope.isOpenRightRack = function(){
			return $mdSidenav('rack').isOpen();
		  };
		  
		function buildToggler(navID) {
			return function() {
				$mdSidenav(navID)
				.toggle()
				.then(function () {
				});
			};
		}

		$scope.close = function () {
		$mdSidenav().close()
			.then(function () {
			});
		};    
	
	});
	