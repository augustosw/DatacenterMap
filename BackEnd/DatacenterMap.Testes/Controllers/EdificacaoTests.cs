using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatacenterMap.Domain.Entidades;
using System.Linq;
namespace DatacenterMap.Testes.Controllers
{
    [TestClass]
    public class EdificacaoTests
    {

        [TestMethod]
        public void Deve_Criar_Entidade_Edificacao_Valida()
        {
            var edificacao = CriarNovoEdificacao();
            Assert.IsTrue(edificacao.Validar());
            Assert.IsFalse(edificacao.Mensagens.Any());
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Edificacao_Sem_Nome()
        {
            var edificacao = CriarNovoEdificacao();
            edificacao.Nome = null;
            Assert.IsFalse(edificacao.Validar());
            Assert.IsTrue(edificacao.Mensagens.Any());
            Assert.IsTrue(edificacao.Mensagens[0] == "Nome é inválido.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Edificacao_Com_Nome_Vazio()
        {
            var edificacao = CriarNovoEdificacao();
            edificacao.Nome = " ";
            Assert.IsFalse(edificacao.Validar());
            Assert.IsTrue(edificacao.Mensagens.Any());
            Assert.IsTrue(edificacao.Mensagens[0] == "Nome é inválido.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Edificacao_Com_Longitude_Maior_Que_90()
        {
            var edificacao = CriarNovoEdificacao();
            edificacao.Longitude = 93;
            Assert.IsFalse(edificacao.Validar());
            Assert.IsTrue(edificacao.Mensagens.Any());
            Assert.IsTrue(edificacao.Mensagens[0] == "Longitude é inválida.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Edificacao_Com_Longitude_Menor_Que_Menos_90()
        {
            var edificacao = CriarNovoEdificacao();
            edificacao.Longitude = -93;
            Assert.IsFalse(edificacao.Validar());
            Assert.IsTrue(edificacao.Mensagens.Any());
            Assert.IsTrue(edificacao.Mensagens[0] == "Longitude é inválida.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Edificacao_Com_Latitude_Maior_Que_90()
        {
            var edificacao = CriarNovoEdificacao();
            edificacao.Latitude = 1000;
            Assert.IsFalse(edificacao.Validar());
            Assert.IsTrue(edificacao.Mensagens.Any());
            Assert.IsTrue(edificacao.Mensagens[0] == "Latitude é inválida.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Edificacao_Com_Latitude_Menor_Que_Menos_90()
        {
            var edificacao = CriarNovoEdificacao();
            edificacao.Latitude = -100;
            Assert.IsFalse(edificacao.Validar());
            Assert.IsTrue(edificacao.Mensagens.Any());
            Assert.IsTrue(edificacao.Mensagens[0] == "Latitude é inválida.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Edificacao_Com_NumeroAndares_Menor_Que_0()
        {
            var edificacao = CriarNovoEdificacao();
            edificacao.NumeroAndares = -1;
            Assert.IsFalse(edificacao.Validar());
            Assert.IsTrue(edificacao.Mensagens.Any());
            Assert.IsTrue(edificacao.Mensagens[0] == "Número de andares deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Edificacao_Com_NumeroAndares_0()
        {
            var edificacao = CriarNovoEdificacao();
            edificacao.NumeroAndares = 0;
            Assert.IsFalse(edificacao.Validar());
            Assert.IsTrue(edificacao.Mensagens.Any());
            Assert.IsTrue(edificacao.Mensagens[0] == "Número de andares deve ser maior que 0.");
        }

        private Edificacao CriarNovoEdificacao()
        {
            return new Edificacao()
            {
                Id = 0,
                Nome = "Edifício Framework",
                NumeroAndares = 3,
                Latitude = 80,
                Longitude = 80
            };
        }
    }
}
