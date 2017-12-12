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
    public class EdificacaoControllerTests
    {
        public EdificacaoControllerTests()
        {
            CleanUp.LimparTabelas(new DatacenterMapContext("DatacenterMapTest"));
        }

        [TestMethod]
        public void Edificacoes_Cadastradas_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            
            var edificacao = CriarNovaEdificacao1();

            var edificacaoController = CriarController();
            edificacaoController.Request = new HttpRequestMessage();

            ObjectContent objetoPost = edificacaoController.CadastrarEdificacao(edificacao).Content as ObjectContent;
            Edificacao edificacaoRetornadaNoPost = objetoPost.Value as Edificacao;

            Assert.IsNotNull(edificacaoRetornadaNoPost);

            ObjectContent objetoGet = edificacaoController.GetEdificacao(edificacaoRetornadaNoPost.Id).Content as ObjectContent;
            Edificacao edificacaoRetornadaNoGet = objetoGet.Value as Edificacao;

            Assert.IsNotNull(edificacaoRetornadaNoGet);

            var edificacaoRetornadoDoBancoEIgualAoOriginal = edificacaoRetornadaNoGet.WithDeepEqual(edificacao)
                                                                .IgnoreSourceProperty(x => x.Id)
                                                                .IgnoreSourceProperty(x => x.Andares)
                                                                .IgnoreSourceProperty(x => x.Mensagens)
                                                                .Compare();

            Assert.IsTrue(edificacaoRetornadoDoBancoEIgualAoOriginal);
        }

        [TestMethod]
        public void Edificacoes_Removidas_Nao_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            var edificacao = CriarNovaEdificacao1();

            var edificacaoController = CriarController();
            edificacaoController.Request = new HttpRequestMessage();

            ObjectContent objeto = edificacaoController.CadastrarEdificacao(edificacao).Content as ObjectContent;
            Edificacao edificacaoRetornadaNoPost = objeto.Value as Edificacao;

            Assert.IsNotNull(edificacaoRetornadaNoPost);

            var edRemovida = edificacaoController.DeletarEdificacao(edificacaoRetornadaNoPost.Id);

            var objetoGet = edificacaoController.GetEdificacao(edificacaoRetornadaNoPost.Id).Content as ObjectContent;
            Edificacao edificacaoRetornadoNoGet = objetoGet.Value as Edificacao;

            Assert.IsNull(edificacaoRetornadoNoGet);
        }

        [TestMethod]
        public void Todas_Edificacoes_Cadastradas_Devem_Ser_Retornadas_No_Obter_Todos()
        {
            var edificacao1 = CriarNovaEdificacao1();
            var edificacao2 = CriarNovaEdificacao2();

            var edificacaoController = CriarController();
            edificacaoController.Request = new HttpRequestMessage();

            ObjectContent objeto1 = edificacaoController.CadastrarEdificacao(edificacao1).Content as ObjectContent;
            Edificacao edificacaoRetornadaNoPost1 = objeto1.Value as Edificacao;

            Assert.IsNotNull(edificacaoRetornadaNoPost1);

            ObjectContent objeto2 = edificacaoController.CadastrarEdificacao(edificacao2).Content as ObjectContent;
            Edificacao edificacaoRetornadaNoPost2 = objeto2.Value as Edificacao;

            Assert.IsNotNull(edificacaoRetornadaNoPost2);

            ObjectContent objetoGet = edificacaoController.GetEdificacoes().Content as ObjectContent;
            List<Edificacao> edificacoesRetornadasNoGet = objetoGet.Value as List<Edificacao>;

            Assert.IsNotNull(edificacoesRetornadasNoGet);

            Assert.AreEqual(2, edificacoesRetornadasNoGet.Count);

            var edificacao1RetornadoDoGet = edificacoesRetornadasNoGet.FirstOrDefault(edificacao => edificacao.Nome.Equals("Framework"));
            var edificacao2RetornadoDoGet = edificacoesRetornadasNoGet.FirstOrDefault(edificacao => edificacao.Nome.Equals("CWI Sede"));

            var edificacao1RetornadoDoBancoEIgualAoOriginal = edificacao1RetornadoDoGet.WithDeepEqual(edificacao1)
                                                                .IgnoreSourceProperty(x => x.Id)
                                                                .IgnoreSourceProperty(x => x.Andares)
                                                                .IgnoreSourceProperty(x => x.Mensagens).Compare();

            var edificacao2RetornadoDoBancoEIgualAoOriginal = edificacao2RetornadoDoGet.WithDeepEqual(edificacao2)
                                                                .IgnoreSourceProperty(x => x.Id)
                                                                .IgnoreSourceProperty(x => x.Andares)
                                                                .IgnoreSourceProperty(x => x.Mensagens).Compare();

            Assert.IsTrue(edificacao1RetornadoDoBancoEIgualAoOriginal);
            Assert.IsTrue(edificacao2RetornadoDoBancoEIgualAoOriginal);
        }

        [TestMethod]
        public void Criar_Edificacao_Deve_Retornar_Erro_Quando_A_Edificacao_For_Nula()
        {
            EdificacaoModel edificacao = null;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var resposta = controller.CadastrarEdificacao(edificacao);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("O parametro request não pode ser null", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Edificacao_Deve_Retornar_Erro_Quando_O_Nome_Ja_Existir()
        {
            EdificacaoModel edificacao = CriarNovaEdificacao1();

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            controller.CadastrarEdificacao(edificacao);
            edificacao.Latitude = 2;

            var resposta = controller.CadastrarEdificacao(edificacao);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("Já existe uma edificação com esse nome.", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Edificacao_Deve_Retornar_Erro_Quando_A_Localizacao_Ja_Existir()
        {
            EdificacaoModel edificacao = CriarNovaEdificacao1();

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            controller.CadastrarEdificacao(edificacao);
            edificacao.Nome = "Outro Nome";

            var resposta = controller.CadastrarEdificacao(edificacao);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("Já existe uma edificação nessa localização.", mensagens[0]);
        }

        [TestMethod]
        public void Remover_Edificacao_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        {
            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var badRequest = controller.DeletarEdificacao(1);
            string[] mensagens = (badRequest.Content as ObjectContent).Value as string[];

            Assert.IsNotNull(badRequest);

            Assert.AreEqual("Edificação não encontrada.", mensagens[0]);
        }

        private EdificacaoModel CriarNovaEdificacao1()
        {
            return new EdificacaoModel()
            {
                Nome = "Framework",
                NumeroAndares = 2,
                Latitude = 32,
                Longitude = 72
            };
        }

        private EdificacaoModel CriarNovaEdificacao2()
        {
            return new EdificacaoModel()
            {
                Nome = "CWI Sede",
                NumeroAndares = 6,
                Latitude = 35,
                Longitude = 79
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
                context.Edificacoes.RemoveRange(context.Edificacoes);
                context.SaveChanges();
            }
        }

        private EdificacaoController CriarController()
        {
            return new EdificacaoController(new DatacenterMapContext("DatacenterMapTest"));
        }
    }
}
