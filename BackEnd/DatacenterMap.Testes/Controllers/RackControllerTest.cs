using DatacenterMap.Domain.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Linq;
using DeepEqual.Syntax;
using DatacenterMap.Infra;
using DatacenterMap.Web.Controllers;
using DatacenterMap.Web.Models;
using System.Net.Http;
using System.Collections.Generic;

namespace DatacenterMap.Testes.Controllers
{

    [TestClass]
    public class RackControllerTests
    {
        public List<Slot> slots;

        public RackControllerTests()
        {
            using (var context = new DatacenterMapContext("DatacenterMapTest"))
            {
                Edificacao edificacao = new Edificacao()
                {
                    Nome = "Framework Building",
                    NumeroAndares = 4,
                    Latitude = 30,
                    Longitude = 20
                };

                context.Edificacoes.Add(edificacao);
                context.SaveChanges();

                Andar andar = new Andar()
                {
                    Edificacao = context.Edificacoes.FirstOrDefault(x => x.NumeroAndares == 4),
                    NumeroAndar = 3,
                    QuantidadeMaximaSalas = 2
                };

                context.Andares.Add(andar);
                context.SaveChanges();

                Sala sala = new Sala()
                {
                    NumeroSala = "3.5",
                    Andar = context.Andares.FirstOrDefault(x => x.NumeroAndar == 3),
                    Comprimento = 110,
                    Largura = 150,
                    QuantidadeMaximaSlots = 3
                };

                context.Salas.Add(sala);
                for (var i = 0; i < sala.QuantidadeMaximaSlots; i++)
                {
                    context.Slots.Add(CreateSlot(sala));
                }

                context.SaveChanges();
                slots = context.Slots.ToList();
            }

        }

        [TestMethod]
        public void Racks_Cadastrados_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var rack = CriarNovaRack1();

            var rackController = CriarController();
            rackController.Request = new HttpRequestMessage();

            ObjectContent objetoPost = rackController.CadastrarRack(rack).Content as ObjectContent;
            Rack rackRetornadaNoPost = objetoPost.Value as Rack;

            Assert.IsNotNull(rackRetornadaNoPost);

            ObjectContent objetoGet = rackController.GetRack(rackRetornadaNoPost.Id).Content as ObjectContent;
            Rack rackRetornadaNoGet = objetoGet.Value as Rack;

            Assert.IsNotNull(rackRetornadaNoGet);

            var rackRetornadoDoBancoEIgualAoOriginal = rackRetornadaNoGet.WithDeepEqual(rack)
                                                                .IgnoreSourceProperty(x => x.Id)
                                                                .IgnoreDestinationProperty(x => x.SlotId)
                                                                .IgnoreSourceProperty(x => x.Mensagens)
                                                                .IgnoreSourceProperty(x => x.Slot)
                                                                .IgnoreSourceProperty(x => x.Gavetas)
                                                                .Compare();

            Assert.IsTrue(rackRetornadoDoBancoEIgualAoOriginal);
        }

        [TestMethod]
        public void Racks_Removidos_Nao_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var rack = CriarNovaRack1();

            var rackController = CriarController();
            rackController.Request = new HttpRequestMessage();

            ObjectContent objeto = rackController.CadastrarRack(rack).Content as ObjectContent;
            Rack rackRetornadaNoPost = objeto.Value as Rack;

            Assert.IsNotNull(rackRetornadaNoPost);

            var edRemovida = rackController.DeletarRack(rackRetornadaNoPost.Id);

            var objetoGet = rackController.GetRack(rackRetornadaNoPost.Id).Content as ObjectContent;
            Rack rackRetornadoNoGet = objetoGet.Value as Rack;

            Assert.IsNull(rackRetornadoNoGet);
        }

        [TestMethod]
        public void Criar_Rack_Deve_Retornar_Erro_Quando_O_Rack_For_Nulo()
        {
            RackModel rack = null;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var resposta = controller.CadastrarRack(rack);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("O parametro request não pode ser null", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Rack_Deve_Retornar_Erro_Quando_Ja_Existir_Rack_No_Slot()
        {
            RackModel rack = CriarNovaRack1();

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            controller.CadastrarRack(rack);

            var resposta = controller.CadastrarRack(rack);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("Já existe um rack neste slot.", mensagens[0]);
        }

        [TestMethod]
        public void Alterar_Rack_Deve_Retornar_Erro_Quando_Dimuir_Quantidade_De_Gavetas()
        {
            RackModel rack = CriarNovaRack1();

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            ObjectContent objeto = controller.CadastrarRack(rack).Content as ObjectContent;
            Rack rackRetornadaNoPost = objeto.Value as Rack;

            rack.Id = rackRetornadaNoPost.Id;
            rack.QuantidadeGavetas = 1;

            var resposta = controller.AlterarRack(rack);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("A quantidade máxima de gavetas não pode ser diminuida.", mensagens[0]);
        }

        [TestMethod]
        public void Remover_Rack_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var badRequest = controller.DeletarRack(1);
            string[] mensagens = (badRequest.Content as ObjectContent).Value as string[];

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("Rack não encontrado.", mensagens[0]);
        }

        private RackModel CriarNovaRack1()
        {
            return new RackModel()
            {
                Descricao = "Rack Teste 1",
                QuantidadeGavetas = 3,
                Tensao = 220,
                SlotId = slots[0].Id
            };
        }

        public Slot CreateSlot(Sala sala)
        {
            var slot = new Slot
            {
                Ocupado = false,
                Sala = sala
            };

            return slot;
        }

        [TestInitialize]
        public void Cleanup()
        {
            // Limpa as tabelas do banco
            using (var context = new DatacenterMapContext("DatacenterMapTest"))
            {
                context.Equipamentos.RemoveRange(context.Equipamentos);
                context.Gavetas.RemoveRange(context.Gavetas);
                context.Racks.RemoveRange(context.Racks);
                
                context.SaveChanges();
            }
        }

        private RackController CriarController()
        {
            return new RackController(new DatacenterMapContext("DatacenterMapTest"));
        }
    }
}
