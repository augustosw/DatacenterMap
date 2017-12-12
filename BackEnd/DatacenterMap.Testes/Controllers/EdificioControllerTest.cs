using DatacenterMap.Domain.Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DeepEqual.Syntax;
using System.Web.Http.Results;
using DatacenterMap.Infra;
using DatacenterMap.Web.Controllers;
using DatacenterMap.Web.Models;

namespace DatacenterMap.Testes.Controllers
{
    [TestClass]
    public class EdificacaoControllerTests
    {
<<<<<<< HEAD
        //[TestMethod]
        //public void Edificacaos_Cadastrados_Devem_Ser_Retornados_No_Obter_Por_Id()
        //{
        //    var edificacao = CriarNovaEdificacao1();
=======
        public EdificacaoControllerTests()
        {
            CleanUp.LimparTabelas(new DatacenterMapContext("DatacenterMapTest"));
        }

        [TestMethod]
        public void Edificacoes_Cadastradas_Devem_Ser_Retornadas_No_Obter_Por_Id()
        {
            
            var edificacao = CriarNovaEdificacao1();
>>>>>>> master

        //    var edificacaoController = CriarController();

        //    var edificacaoRetornadoNoPost = edificacaoController.CriarEdificacao(edificacao) as CreatedNegotiatedContentResult<Edificacao>;

        //    Assert.IsNotNull(edificacaoRetornadoNoPost);

        //    var edificacaoRetornadoNoGet = edificacaoController.ObterEdificacaoPorId(edificacaoRetornadoNoPost.Content.Id) as OkNegotiatedContentResult<Edificacao>;

        //    Assert.IsNotNull(edificacaoRetornadoNoGet);

        //    var edificacaoRetornadoDoBancoEIgualAoOriginal = edificacaoRetornadoNoGet.Content.WithDeepEqual(edificacao).IgnoreSourceProperty(x => x.Id).Compare();

        //    Assert.IsTrue(edificacaoRetornadoDoBancoEIgualAoOriginal);
        //}

        //[TestMethod]
        //public void Edificacaos_Atualizados_Devem_Ser_Retornados_No_Obter_Por_Id()
        //{
        //    var edificacao = CriarNovaEdificacao1();

        //    var edificacaoController = CriarController();

        //    var edificacaoRetornadoNoPost = edificacaoController.CriarEdificacao(edificacao) as CreatedNegotiatedContentResult<Edificacao>;

        //    Assert.IsNotNull(edificacaoRetornadoNoPost);

        //    edificacaoRetornadoNoPost.Content.Nome = "Joaquim";
        //    var edificacaoRetornadoNoPut = edificacaoController.AtualizarEdificacao(edificacaoRetornadoNoPost.Content.Id, edificacaoRetornadoNoPost.Content) as OkNegotiatedContentResult<Edificacao>;

<<<<<<< HEAD
        //    Assert.IsNotNull(edificacaoRetornadoNoPut);

        //    var edificacaoRetornadoNoGet = edificacaoController.ObterEdificacaoPorId(edificacaoRetornadoNoPost.Content.Id) as OkNegotiatedContentResult<Edificacao>;
=======
            var objetoGet = edificacaoController.GetEdificacao(edificacaoRetornadaNoPost.Id).Content as ObjectContent;
            Edificacao edificacaoRetornadoNoGet = objetoGet.Value as Edificacao;

            Assert.IsNull(edificacaoRetornadoNoGet);
        }
>>>>>>> master

        //    Assert.IsNotNull(edificacaoRetornadoNoGet);

        //    var edificacaoRetornadoDoBancoEIgualAoOriginal = edificacaoRetornadoNoGet.Content.WithDeepEqual(edificacao).IgnoreSourceProperty(x => x.Id).Compare();

        //    Assert.IsTrue(edificacaoRetornadoDoBancoEIgualAoOriginal);
        //}

        [TestMethod]
        public void Edificacaos_Removidos_Nao_Devem_Ser_Retornados_No_Obter_Por_Id()
        {
            var edificacao = CriarNovaEdificacao1();

            var edificacaoController = CriarController();

            var edificacaoRetornadoNoPost = edificacaoController.CadastrarEdificacao(edificacao);

            Assert.IsNotNull(edificacaoRetornadoNoPost);
        }

        //[TestMethod]
        //public void Todos_Edificacaos_Cadastrados_Devem_Ser_Retornados_No_Obter_Todos()
        //{
        //    var edificacao1 = CriarNovaEdificacao1();
        //    var edificacao2 = CriarNovaEdificacao2();

        //    var edificacaoController = CriarController();

        //    var edificacao1RetornadoNoPost = edificacaoController.CadastrarEdificacao(edificacao1) as HttpResponseMessage;

        //    Assert.IsNotNull(edificacao1RetornadoNoPost);

        //    var edificacao2RetornadoNoPost = edificacaoController.CadastrarEdificacao(edificacao2) as HttpResponseMessage;

        //    Assert.IsNotNull(edificacao2RetornadoNoPost);

        //    var edificacaosRetornadosDoGet = edificacaoController.GetEdificacoes() as HttpResponseMessage;

        //    Assert.IsNotNull(edificacaosRetornadosDoGet);

        //    Assert.AreEqual(2, edificacaosRetornadosDoGet.Content.Count);

        //    var edificacao1RetornadoDoGet = edificacaosRetornadosDoGet.Content.FirstOrDefault(edificacao => edificacao.Nome == "Teddy");
        //    var edificacao2RetornadoDoGet = edificacaosRetornadosDoGet.Content.FirstOrDefault(edificacao => edificacao.Nome == "Tobi");

        //    var edificacao1RetornadoDoBancoEIgualAoOriginal = edificacao1RetornadoDoGet.WithDeepEqual(edificacao1).IgnoreSourceProperty(x => x.Id).Compare();

        //    Assert.IsTrue(edificacao1RetornadoDoBancoEIgualAoOriginal);

        //    var edificacao2RetornadoDoBancoEIgualAoOriginal = edificacao2RetornadoDoGet.WithDeepEqual(edificacao2).IgnoreSourceProperty(x => x.Id).Compare();

        //    Assert.IsTrue(edificacao2RetornadoDoBancoEIgualAoOriginal);
        //}

        //[TestMethod]
        //public void Criar_Edificacao_Deve_Retornar_Erro_Quando_O_Edificacao_For_Nulo()
        //{
        //    var edificacao = CriarNovaEdificacao1();

        //    var controller = CriarController();

        //    var badRequest = controller.CadastrarEdificacao(null) as HttpResponseMessage;

        //    Assert.IsNotNull(badRequest);

        //    Assert.AreEqual(badRequest.Message, "O parametro edificacao não pode ser null");
        //}

        //[TestMethod]
        //public void Atualizar_Edificacao_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        //{
        //    var controller = CriarController();

        //    var badRequest = controller.AtualizarEdificacao(1, new Edificacao()) as HttpResponseMessage;

        //    Assert.IsNotNull(badRequest);

        //    Assert.AreEqual(badRequest.Message, "Id inexistente");
        //}

        //[TestMethod]
        //public void Remover_Edificacao_Deve_Retornar_Erro_Quando_O_Id_For_Inexistente()
        //{
        //    var controller = CriarController();

        //    var badRequest = controller.DeletarEdificacao(1) as HttpResponseMessage;

        //    Assert.IsNotNull(badRequest);

        //    Assert.AreEqual(badRequest.Message, "Id inexistente");
        //}

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
