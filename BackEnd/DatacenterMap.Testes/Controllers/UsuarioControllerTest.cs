using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatacenterMap.Infra;
using DatacenterMap.Web.Controllers;
using DatacenterMap.Web.Models;
using System.Net.Http;

namespace DatacenterMap.Testes.Controllers
{
    [TestClass]
    public class UsuarioControllerTests
    {
     
        [TestMethod]
        public void Criar_Usuario_Deve_Retornar_Erro_Quando_A_Usuario_For_Nula()
        {
            UsuarioModel usuario = null;

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            var resposta = controller.CadastrarUsuario(usuario);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("O parametro request não pode ser null", mensagens[0]);
        }

        [TestMethod]
        public void Criar_Usuario_Deve_Retornar_Erro_Quando_O_Email_Ja_Existir()
        {
            UsuarioModel usuario = CriarNovaUsuario1();

            var controller = CriarController();
            controller.Request = new HttpRequestMessage();

            controller.CadastrarUsuario(usuario);

            var resposta = controller.CadastrarUsuario(usuario);
            string[] mensagens = (resposta.Content as ObjectContent).Value as string[];

            Assert.IsFalse(resposta.IsSuccessStatusCode);

            Assert.AreEqual("Já existe uma conta com esse email.", mensagens[0]);
        }

        private UsuarioModel CriarNovaUsuario1()
        {
            return new UsuarioModel()
            {
                Nome = "Gustavinho das Meninas",
                Email = "gustavinho@gmail.com",
                Senha = "1234"
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
                context.Usuarios.RemoveRange(context.Usuarios);
                context.SaveChanges();
            }
        }

        private UsuarioController CriarController()
        {
            return new UsuarioController(new DatacenterMapContext("DatacenterMapTest"));
        }
    }
}
