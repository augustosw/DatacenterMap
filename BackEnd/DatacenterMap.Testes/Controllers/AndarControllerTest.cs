using DatacenterMap.Domain.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DeepEqual.Syntax;
using DatacenterMap.Infra;
using DatacenterMap.Web.Controllers;
using DatacenterMap.Web.Models;
using System.Net.Http;

namespace DatacenterMap.Testes.Controllers
{

    [TestClass]
    public class AndarControllerTests
    {
        public int idEdificio = 0;

        public AndarControllerTests()
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

                Edificacao ed = context.Edificacoes.FirstOrDefault(x => x.NumeroAndares == 4);
                idEdificio = ed.Id;
            
            }
            
        }

        [TestMethod]
        public void Andares_Cadastrados_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var andar = CriarNovoAndar1();

            var andarController = CriarController();
            andarController.Request = new HttpRequestMessage();

            ObjectContent objetoPost = andarController.CadastrarAndar(andar).Content as ObjectContent;
            Andar andarRetornadaNoPost = objetoPost.Value as Andar;

            Assert.IsNotNull(andarRetornadaNoPost);

            ObjectContent objetoGet = andarController.GetAndar(andarRetornadaNoPost.Id).Content as ObjectContent;
            Andar andarRetornadaNoGet = objetoGet.Value as Andar;

            Assert.IsNotNull(andarRetornadaNoGet);

            var andarRetornadoDoBancoEIgualAoOriginal = andarRetornadaNoGet.WithDeepEqual(andar)
                                                                .IgnoreSourceProperty(x => x.Id)
                                                                .IgnoreDestinationProperty(x => x.EdificacaoId)
                                                                .IgnoreSourceProperty(x => x.Salas)
                                                                .IgnoreSourceProperty(x => x.Edificacao)
                                                                .IgnoreSourceProperty(x => x.Mensagens)
                                                                .Compare();

            Assert.IsTrue(andarRetornadoDoBancoEIgualAoOriginal);
        }

        [TestMethod]
        public void Andares_Removidos_Nao_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var andar = CriarNovoAndar1();

            var andarController = CriarController();
            andarController.Request = new HttpRequestMessage();

            ObjectContent objeto = andarController.CadastrarAndar(andar).Content as ObjectContent;
            Andar andarRetornadaNoPost = objeto.Value as Andar;

            Assert.IsNotNull(andarRetornadaNoPost);

            var edRemovida = andarController.DeletarAndar(andarRetornadaNoPost.Id);

            var andarRetornadoNoGet = andarController.GetAndar(andarRetornadaNoPost.Id).Content as ObjectContent;

            Assert.IsNotNull(andarRetornadoNoGet.Value);
        }

        [TestMethod]
        public void Criar_Andar_Deve_Retornar_Erro_Quando_O_Andar_For_Nulo()
        {
            AndarModel andar = null;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var resposta = controller.CadastrarAndar(andar);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("O parametro request não pode ser null", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Andar_Deve_Retornar_Erro_Quando_Andar_Ja_Existir()
        {
            AndarModel andar = CriarNovoAndar1();

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            controller.CadastrarAndar(andar);

            var resposta = controller.CadastrarAndar(andar);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("Já existe este andar no edifício.", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Andar_Deve_Retornar_Erro_Quando_Numero_Ultrapassar_Limite_Predio()
        {
            AndarModel andar = CriarNovoAndar1();
            andar.NumeroAndar = 99;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var resposta = controller.CadastrarAndar(andar);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("O andar solicitado ultrapassa o limite máximo do prédio.", mensagens[0]);
        }

        [TestMethod]
        public void Remover_Andar_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var badRequest = controller.DeletarAndar(1);
            string[] mensagens = (badRequest.Content as ObjectContent).Value as string[];

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("Andar não encontrado.", mensagens[0]);
        }

        private AndarModel CriarNovoAndar1()
        {
            return new AndarModel()
            {
                NumeroAndar = 3,
                QuantidadeMaximaSalas = 3,
                EdificacaoId = idEdificio
            };
        }

        private AndarModel CriarNovoAndar2()
        {
            return new AndarModel()
            {
                NumeroAndar = 4,
                QuantidadeMaximaSalas = 2,
                EdificacaoId = idEdificio
            };
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
                context.Slots.RemoveRange(context.Slots);
                context.Salas.RemoveRange(context.Salas);
                context.Andares.RemoveRange(context.Andares);
                context.SaveChanges();

               
            }
        }

        private AndarController CriarController()
        {
            return new AndarController(new DatacenterMapContext("DatacenterMapTest"));
        }
    }
}
