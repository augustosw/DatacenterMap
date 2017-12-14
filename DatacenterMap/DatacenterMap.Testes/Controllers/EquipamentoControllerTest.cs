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
using DatacenterMap.Web;

namespace DatacenterMap.Testes.Controllers
{

    [TestClass]
    public class EquipamentoControllerTests
    {
        public List<Gaveta> gavetas;
        public int rack2Id, rack3Id, edificacaoId;
        DatacenterMapContext context;

        public EquipamentoControllerTests()
        {
            context = new DatacenterMapContext("DatacenterMapTest");
            {
                CleanUp.LimparTabelas(context);

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

                Rack rack2 = new Rack()
                {
                    Tensao = 220,
                    QuantidadeGavetas = 5,
                    Slot = context.Slots.ToList()[1],
                    Descricao = "Rack 2"
                };

                Rack rack3 = new Rack()
                {
                    Tensao = 220,
                    QuantidadeGavetas = 5,
                    Slot = context.Slots.ToList()[2],
                    Descricao = "Rack 3"
                };

                context.Racks.Add(rack);
                context.Racks.Add(rack2);
                context.Racks.Add(rack3);

                for (var i = 0; i < rack.QuantidadeGavetas; i++)
                {
                    context.Gavetas.Add(CreateGaveta(rack, i + 1, false));
                    context.Gavetas.Add(CreateGaveta(rack2, i + 1, false));
                }

                context.Gavetas.Add(CreateGaveta(rack3, 1, true));
                context.Gavetas.Add(CreateGaveta(rack3, 2, false));
                context.Gavetas.Add(CreateGaveta(rack3, 3, true));
                context.Gavetas.Add(CreateGaveta(rack3, 4, false));
                context.Gavetas.Add(CreateGaveta(rack3, 5, false));

                context.SaveChanges();
                edificacaoId = context.Edificacoes.ToList()[0].Id;
                rack3Id = context.Racks.ToList()[2].Id;
                rack2Id = context.Racks.ToList()[1].Id;
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

            var objetoGet = equipamentoController.GetEquipamento(equipamentoRetornadaNoPost.Id).Content as ObjectContent;
            Equipamento equipamentoRetornadoNoGet = objetoGet.Value as Equipamento;

            Assert.IsNull(equipamentoRetornadoNoGet);
        }

        [TestMethod]
        public void Edificio_Removido_Nao_Deve_Ter_Equipamentos()
        {
            var equipamento = CriarNovaEquipamento1();

            var equipamentoController = CriarController();
            equipamentoController.Request = new HttpRequestMessage();

            ObjectContent objeto = equipamentoController.CadastrarEquipamento(equipamento).Content as ObjectContent;
            Equipamento equipamentoRetornadaNoPost = objeto.Value as Equipamento;

            Assert.IsNotNull(equipamentoRetornadaNoPost);
         
            ControllerUtils.DeletarEdificacao(context, edificacaoId);
            
            var objetoGet = equipamentoController.GetEquipamento(equipamentoRetornadaNoPost.Id).Content as ObjectContent;
            Equipamento equipamentoRetornadoNoGet = objetoGet.Value as Equipamento;

            Assert.IsNull(equipamentoRetornadoNoGet);
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
        public void Criar_Equipamento_Deve_Retornar_Erro_Quando_A_Tensao_ForDiferente_Da_Do_Rack()
        {
            EquipamentoModel equipamento = CriarNovaEquipamento1();
            equipamento.Tensao = 110;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var resposta = controller.CadastrarEquipamento(equipamento);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("O rack não tem a mesma tensão do equipamento.", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Equipamento_Deve_Retornar_Erro_Quando_O_NumeroDeGavetas_ForDiferente_Do_Tamanho()
        {
            EquipamentoModel equipamento = CriarNovaEquipamento1();
            equipamento.Tamanho = 3;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();


            var resposta = controller.CadastrarEquipamento(equipamento);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("A quantidade de gavetas encontradas não é igual ao tamanho do equipamento.", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Equipamento_Deve_Retornar_Erro_Quando_As_Gavetas_NaoSao_Do_Mesmo_Rack()
        {
            EquipamentoModel equipamento = CriarNovaEquipamento1();
            equipamento.Tamanho = 2;
            equipamento.GavetasId.Add(gavetas[5].Id);

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();


            var resposta = controller.CadastrarEquipamento(equipamento);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("As gavetas não são do mesmo rack.", mensagens[0]);
        }

        [TestMethod]
        public void Remover_Equipamento_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var badRequest = controller.DeletarEquipamento(99999999);
            string[] mensagens = (badRequest.Content as ObjectContent).Value as string[];

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("Equipamento não encontrado.", mensagens[0]);
        }

        [TestMethod]
        public void Deve_Mover_Equipamento_Tamanho1_Com_Sucesso()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            ObjectContent objetoPost = controller.CadastrarEquipamento(CriarNovaEquipamento1()).Content as ObjectContent;
            Equipamento equipamentoRetornadoNoPost = objetoPost.Value as Equipamento;

            ObjectContent objetoMover = controller.MoverEquipamento(rack2Id, equipamentoRetornadoNoPost.Id).Content as ObjectContent;
            Equipamento depoisDeMovido = objetoMover.Value as Equipamento;

            Assert.IsNotNull(depoisDeMovido);

            Assert.IsTrue(depoisDeMovido.Gavetas[0].Id == gavetas[5].Id);
        }

        [TestMethod]
        public void Deve_Mover_Equipamento_Tamanho3_Com_Sucesso()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            EquipamentoModel equipamento = CriarNovaEquipamento1();
            equipamento.Tamanho = 3;
            equipamento.GavetasId.Add(gavetas[1].Id);
            equipamento.GavetasId.Add(gavetas[2].Id);

            ObjectContent objetoPost = controller.CadastrarEquipamento(equipamento).Content as ObjectContent;
            Equipamento equipamentoRetornadoNoPost = objetoPost.Value as Equipamento;

            ObjectContent objetoMover = controller.MoverEquipamento(rack2Id, equipamentoRetornadoNoPost.Id).Content as ObjectContent;
            Equipamento depoisDeMovido = objetoMover.Value as Equipamento;

            Assert.IsNotNull(depoisDeMovido);

            Assert.IsTrue(depoisDeMovido.Gavetas[0].Id == gavetas[5].Id);
            Assert.IsTrue(depoisDeMovido.Gavetas[1].Id == gavetas[6].Id);
            Assert.IsTrue(depoisDeMovido.Gavetas[2].Id == gavetas[7].Id);
        }

        [TestMethod]
        public void Deve_Mover_Equipamento_Tamanho2_Com_Sucesso()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            EquipamentoModel equipamento = CriarNovaEquipamento1();
            equipamento.Tamanho = 2;
            equipamento.GavetasId.Add(gavetas[1].Id);

            ObjectContent objetoPost = controller.CadastrarEquipamento(equipamento).Content as ObjectContent;
            Equipamento equipamentoRetornadoNoPost = objetoPost.Value as Equipamento;

            ObjectContent objetoMover = controller.MoverEquipamento(rack3Id, equipamentoRetornadoNoPost.Id).Content as ObjectContent;
            Equipamento depoisDeMovido = objetoMover.Value as Equipamento;

            Assert.IsNotNull(depoisDeMovido);

            Assert.IsTrue(depoisDeMovido.Gavetas[0].Id == gavetas[13].Id);
            Assert.IsTrue(depoisDeMovido.Gavetas[1].Id == gavetas[14].Id);
        }

        [TestMethod]
        public void Nao_Deve_Mover_Equipamento_Para_Gavetas_Nao_Consecuivas()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            EquipamentoModel equipamento = CriarNovaEquipamento1();
            equipamento.Tamanho = 3;
            equipamento.GavetasId.Add(gavetas[1].Id);
            equipamento.GavetasId.Add(gavetas[2].Id);

            ObjectContent objetoPost = controller.CadastrarEquipamento(equipamento).Content as ObjectContent;
            Equipamento equipamentoRetornadoNoPost = objetoPost.Value as Equipamento;

            var badRequest = controller.MoverEquipamento(rack3Id, equipamentoRetornadoNoPost.Id);
            string[] mensagens = (badRequest.Content as ObjectContent).Value as string[];

            Assert.AreEqual("Não existem gavetas consecutivas no rack para alocar o equipamento.", mensagens[0]);
        }

        private EquipamentoModel CriarNovaEquipamento1()
        {
            List<int> ids = new List<int>
            {
                gavetas[0].Id
            };

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

        public Gaveta CreateGaveta(Rack rack, int posicao, bool ocupado)
        {
            var gaveta = new Gaveta
            {
                Ocupado = ocupado,
                Posicao = posicao,
                Rack = rack
            };
            return gaveta;
        }

        private EquipamentoController CriarController()
        {
            return new EquipamentoController(context);
        }
    }
}
