using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatacenterMap.Domain.Entidades;
using System.Linq;
using DatacenterMap.Web.Controllers;
using DatacenterMap.Domain;

namespace DatacenterMap.Testes.Controllers
{
	[TestClass]
	public class UsuarioTests
	{

		[TestMethod]
		public void Deve_Criar_Entidade_Usuario_Valida()
		{
			var usuario = CriarNovoUsuario();
			Assert.IsTrue(usuario.Validar());
			Assert.IsFalse(usuario.Mensagens.Any());
		}

		[TestMethod]
		public void Nao_Deve_Validar_Entidade_Usuario_Sem_Nome()
		{
			var usuario = CriarNovoUsuario();
			usuario.Nome = null;
			Assert.IsFalse(usuario.Validar());
			Assert.IsTrue(usuario.Mensagens.Any());
			Assert.IsTrue(usuario.Mensagens[0] == "Nome é inválido.");
		}

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Usuario_Com_Nome_Vazio()
        {
            var usuario = CriarNovoUsuario();
            usuario.Nome = " ";
            Assert.IsFalse(usuario.Validar());
            Assert.IsTrue(usuario.Mensagens.Any());
            Assert.IsTrue(usuario.Mensagens[0] == "Nome é inválido.");
        }

        [TestMethod]
		public void Nao_Deve_Validar_Entidade_Usuario_Sem_Email()
		{
			var usuario = CriarNovoUsuario();
			usuario.Email = null;
			Assert.IsFalse(usuario.Validar());
			Assert.IsTrue(usuario.Mensagens.Any());
			Assert.IsTrue(usuario.Mensagens[0] == "Email é inválido.");
		}

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Usuario_Com_Email_Vazio()
        {
            var usuario = CriarNovoUsuario();
            usuario.Email = "  ";
            Assert.IsFalse(usuario.Validar());
            Assert.IsTrue(usuario.Mensagens.Any());
            Assert.IsTrue(usuario.Mensagens[0] == "Email é inválido.");
        }

        [TestMethod]
		public void Nao_Deve_Validar_Entidade_Usuario_Com_Email_Sem_Arroba()
		{
			var usuario = CriarNovoUsuario();
			usuario.Email = "joaozinho.teste.com";
			Assert.IsFalse(usuario.Validar());
			Assert.IsTrue(usuario.Mensagens.Any());
			Assert.IsTrue(usuario.Mensagens[0] == "Email é inválido.");
		}

		[TestMethod]
		public void Nao_Deve_Validar_Entidade_Usuario_Com_Email_Sem_Ponto()
		{
			var usuario = CriarNovoUsuario();
			usuario.Email = "joaozinho@teste";
			Assert.IsFalse(usuario.Validar());
			Assert.IsTrue(usuario.Mensagens.Any());
			Assert.IsTrue(usuario.Mensagens[0] == "Email é inválido.");
		}

		[TestMethod]
		public void Nao_Deve_Validar_Entidade_Usuario_Sem_Senha()
		{
			var usuario = CriarNovoUsuario();
			usuario.Senha = null;
			Assert.IsFalse(usuario.Validar());
			Assert.IsTrue(usuario.Mensagens.Any());
			Assert.IsTrue(usuario.Mensagens[0] == "Senha é inválida.");
		}

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Usuario_Com_Senha_Vazia()
        {
            var usuario = CriarNovoUsuario();
            usuario.Senha = "  ";
            Assert.IsFalse(usuario.Validar());
            Assert.IsTrue(usuario.Mensagens.Any());
            Assert.IsTrue(usuario.Mensagens[0] == "Senha é inválida.");
        }

        [TestMethod]
		public void Deve_Validar_Senha_Usuario_Correta()
		{
			var usuario = CriarNovoUsuario();
			Assert.IsTrue(usuario.ValidarSenha("minhasenha123"));
		}

		[TestMethod]
		public void Deve_Criptografar_Senha_Usuario()
		{
			var usuario = CriarNovoUsuario();
			var usuarioController = new UsuarioController();
			var usuarioCadastrado = usuarioController.CreateUsuario(usuario.Nome, usuario.Email, usuario.Senha);
			Assert.IsTrue(usuarioCadastrado.Senha != "minhasenha123");
		}


		private Usuario CriarNovoUsuario()
		{
            return new Usuario()
            {
                Id = 0,
                Nome = "João Silva",
                Email = "joaozinho@teste.com",
                Senha = Criptografia.CriptografarSenha("joaozinho@teste.com", "minhasenha123")
			};
		}
	}
}
