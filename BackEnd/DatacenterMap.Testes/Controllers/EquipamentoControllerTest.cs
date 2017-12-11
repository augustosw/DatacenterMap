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
    public class EquipamentoControllerTests
    {
        public List<Gaveta> gavetas;

        public EquipamentoControllerTests()
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

                Rack rack = new Rack()
                {
                    Tensao = 220,
                    QuantidadeGavetas = 5,
                    Slot = context.Slots.ToList()[0],
                    Descricao = "Rack 1"
                };

                context.Racks.Add(rack);
                for (var i = 0; i < rack.QuantidadeGavetas; i++)
                {
                    context.Gavetas.Add(CreateGaveta(rack, i + 1));
                }
                context.SaveChanges();

                gavetas = context.Gavetas.ToList();
            }

        }

        [TestMethod]
        public void Equipamentos_Cadastrados_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var equipamento = CriarNovaEquipamento1();

            var equipamentoController = CriarController();
            equipamentoController.Request = new HttpRequestMessage();

            ObjectContent objetoPost = equipamentoController.CadastrarEquipamento(equipamento).Content as ObjectContent;
            Equipamento equipamentoRetornadaNoPost = objetoPost.Value as Equipamento;

            Assert.IsNotNull(equipamentoRetornadaNoPost);

            ObjectContent objetoGet = equipamentoController.GetEquipamento(equipamentoRetornadaNoPost.Id).Content as ObjectContent;
            Equipamento equipamentoRetornadaNoGet = objetoGet.Value as Equipamento;

            Assert.IsNotNull(equipamentoRetornadaNoGet);

            var equipamentoRetornadoDoBancoEIgualAoOriginal = equipamentoRetornadaNoGet.WithDeepEqual(equipamento)
                                                                .IgnoreSourceProperty(x => x.Id)
                                                                .IgnoreDestinationProperty(x => x.GavetasId)
                                                                .IgnoreSourceProperty(x => x.Mensagens)
                                                                .IgnoreSourceProperty(x => x.Gavetas)
                                                                .Compare();

            Assert.IsTrue(equipamentoRetornadoDoBancoEIgualAoOriginal);
        }

        [TestMethod]
        public void Equipamentos_Removidos_Nao_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var equipamento = CriarNovaEquipamento1();

            var equipamentoController = CriarController();
            equipamentoController.Request = new HttpRequestMessage();

            ObjectContent objeto = equipamentoController.CadastrarEquipamento(equipamento).Content as ObjectContent;
            Equipamento equipamentoRetornadaNoPost = objeto.Value as Equipamento;

            Assert.IsNotNull(equipamentoRetornadaNoPost);

            var edRemovida = equipamentoController.DeletarEquipamento(equipamentoRetornadaNoPost.Id);

            var equipamentoRetornadoNoGet = equipamentoController.GetEquipamento(equipamentoRetornadaNoPost.Id).Content as ObjectContent;

            Assert.IsNotNull(equipamentoRetornadoNoGet.Value);
        }

        [TestMethod]
        public void Criar_Equipamento_Deve_Retornar_Erro_Quando_O_Equipamento_For_Nulo()
        {
            EquipamentoModel equipamento = null;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var resposta = controller.CadastrarEquipamento(equipamento);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("O parametro request não pode ser null", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Equipamento_Deve_Retornar_Erro_Quando_Ja_Existir_Equipamento_No_Slot()
        {
            EquipamentoModel equipamento = CriarNovaEquipamento1();

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            controller.CadastrarEquipamento(equipamento);

            var resposta = controller.CadastrarEquipamento(equipamento);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("Gaveta(s) ocupada(s).", mensagens[0]);
        }

        [TestMethod]
        public void Remover_Equipamento_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var badRequest = controller.DeletarEquipamento(1);
            string[] mensagens = (badRequest.Content as ObjectContent).Value as string[];

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("Equipamento não encontrado.", mensagens[0]);
        }

        private EquipamentoModel CriarNovaEquipamento1()
        {
            List<int> ids = new List<int>();
            ids.Add(gavetas[0].Id);
            return new EquipamentoModel()
            {
                Descricao = "Equipamento Teste 1",
                Tensao = 220,
                GavetasId = ids,
                Tamanho = 1
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

        public Gaveta CreateGaveta(Rack rack, int posicao)
        {
            var gaveta = new Gaveta
            {
                Ocupado = false,
                Posicao = posicao,
                Rack = rack
            };
            return gaveta;
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Limpa as tabelas do banco
            using (var context = new DatacenterMapContext("DatacenterMapTest"))
            {
                context.Equipamentos.RemoveRange(context.Equipamentos);
                context.Gavetas.RemoveRange(context.Gavetas);
                context.SaveChanges();
            }
        }

        private EquipamentoController CriarController()
        {
            return new EquipamentoController(new DatacenterMapContext("DatacenterMapTest"));
        }
    }
}
