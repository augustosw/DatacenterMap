angular.module('app').controller('SalaController', function ($scope, $location, salaService, $mdDialog, rackService, $routeParams, $mdSidenav, toastr) {
	
		$scope.limparSlot = limparSlot;
		$scope.isAlterar = !!$routeParams.id;
		$scope.voltar = voltar;
		$scope.deleteClick = deleteClick;
		$scope.excluir = excluir;
		$scope.tipoEntidade = "sala";
		$scope.verificarSlot = verificarSlot;

		buscarSalaPorId($routeParams.id);
		
		function buscarSalaPorId(id) {
			salaService.buscarPorId(id)
				.then(function (response) {
					$scope.sala = response.data;
					$scope.salaSelecionada = response.data;
					$scope.slots = $scope.salaSelecionada.Slots;
					$scope.slotsOcupados = $scope.slots.filter(s => s.Ocupado);
					buscarTensaoSlotsOcupados();
				})
		}
			
		function buscarTensaoSlotsOcupados() {
			//para preenchimento da popover
			console.log($scope.slotsOcupados);
			let idSlots = []; 
			$scope.slotsOcupados.forEach(slot => idSlots.push(slot.Id)); 
			rackService.buscarPorRacksPorSlots(idSlots)
									.then(function(response) { 
										response.data.forEach( rack => $scope.slotsOcupados.forEach(function(slot) {
										  if(slot.Id === rack.Slot.Id) {
												slot.Rack = rack;
												} 
										})		
										)});
			// $scope.slotsOcupados.forEach(slot => rackService.buscarRackPorIdSlot(slot)
			// 	.then(response => slot.Rack = response.data));
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
					toastr.success('Sala excluída', {
					  iconClass: 'toast-sucesso'
					});
				},
				function (response) {
					toastr.error(response.data.join(" - "), 'Falha na solicitação!', {
					  iconClass: 'toast-erro'
					});
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
				.then(response => rackService.excluir(response.data.Id)
					.then(function(response) {
						toastr.success('Rack excluído', {
						  iconClass: 'toast-sucesso'
						});
						location.reload();
					}));
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
	