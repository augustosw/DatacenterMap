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
    public class SalaControllerTests
    {
        public int idAndar = 0;

        public SalaControllerTests()
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

                Andar ad = context.Andares.FirstOrDefault(x => x.NumeroAndar == 3);
                idAndar = ad.Id;
            }

        }

        [TestMethod]
        public void Salas_Cadastrados_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var sala = CriarNovaSala1();

            var salaController = CriarController();
            salaController.Request = new HttpRequestMessage();

            ObjectContent objetoPost = salaController.CadastrarSala(sala).Content as ObjectContent;
            Sala salaRetornadaNoPost = objetoPost.Value as Sala;

            Assert.IsNotNull(salaRetornadaNoPost);

            ObjectContent objetoGet = salaController.GetSala(salaRetornadaNoPost.Id).Content as ObjectContent;
            Sala salaRetornadaNoGet = objetoGet.Value as Sala;

            Assert.IsNotNull(salaRetornadaNoGet);

            var salaRetornadoDoBancoEIgualAoOriginal = salaRetornadaNoGet.WithDeepEqual(sala)
                                                                .IgnoreSourceProperty(x => x.Id)
                                                                .IgnoreDestinationProperty(x => x.AndarId)
                                                                .IgnoreSourceProperty(x => x.Mensagens)
                                                                .IgnoreSourceProperty(x => x.Slots)
                                                                .IgnoreSourceProperty(x => x.Andar)
                                                                .Compare();

            Assert.IsTrue(salaRetornadoDoBancoEIgualAoOriginal);
        }

        [TestMethod]
        public void Salas_Removidos_Nao_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var sala = CriarNovaSala1();

            var salaController = CriarController();
            salaController.Request = new HttpRequestMessage();

            ObjectContent objeto = salaController.CadastrarSala(sala).Content as ObjectContent;
            Sala salaRetornadaNoPost = objeto.Value as Sala;

            Assert.IsNotNull(salaRetornadaNoPost);

            var edRemovida = salaController.DeletarSala(salaRetornadaNoPost.Id);

            var objetoGet = salaController.GetSala(salaRetornadaNoPost.Id).Content as ObjectContent;
            Sala salaRetornadoNoGet = objetoGet.Value as Sala;

            Assert.IsNull(salaRetornadoNoGet);
        }

        [TestMethod]
        public void Criar_Sala_Deve_Retornar_Erro_Quando_O_Sala_For_Nulo()
        {
            SalaModel sala = null;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var resposta = controller.CadastrarSala(sala);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("O parametro request não pode ser null", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Sala_Deve_Retornar_Erro_Quando_Sala_Ja_Existir()
        {
            SalaModel sala = CriarNovaSala1();

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            controller.CadastrarSala(sala);

            var resposta = controller.CadastrarSala(sala);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("Já existe uma sala com esse número nesse andar.", mensagens[0]);
        }

        [TestMethod]
        public void Remover_Sala_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var badRequest = controller.DeletarSala(1);
            string[] mensagens = (badRequest.Content as ObjectContent).Value as string[];

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("Sala não encontrada.", mensagens[0]);
        }

        private SalaModel CriarNovaSala1()
        {
            return new SalaModel()
            {
                NumeroSala = "3.2",
                AndarId = idAndar,
                Comprimento = 100,
                Largura = 100,
                QuantidadeMaximaSlots = 5
            };
        }

        private SalaModel CriarNovaSala2()
        {
            return new SalaModel()
            {
                NumeroSala = "3.5",
                AndarId = idAndar,
                Comprimento = 110,
                Largura = 150,
                QuantidadeMaximaSlots = 3
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
                context.SaveChanges();


            }
        }

        private SalaController CriarController()
        {
            return new SalaController(new DatacenterMapContext("DatacenterMapTest"));
        }
    }
}
