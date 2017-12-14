using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatacenterMap.Domain.Entidades;
using System.Linq;
namespace DatacenterMap.Testes.Controllers
{
    [TestClass]
    public class RackTests
    {

        [TestMethod]
        public void Deve_Criar_Entidade_Rack_Valida()
        {
            var rack = CriarNovoRack();
            Assert.IsTrue(rack.Validar());
            Assert.IsFalse(rack.Mensagens.Any());
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Rack_Com_QuantidadeGavetas_0()
        {
            var rack = CriarNovoRack();
            rack.QuantidadeGavetas = 0;
            Assert.IsFalse(rack.Validar());
            Assert.IsTrue(rack.Mensagens.Any());
            Assert.IsTrue(rack.Mensagens[0] == "Quantidade de gavetas deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Rack_Com_QuantidadeGavetas_MenorQue0()
        {
            var rack = CriarNovoRack();
            rack.QuantidadeGavetas = -10;
            Assert.IsFalse(rack.Validar());
            Assert.IsTrue(rack.Mensagens.Any());
            Assert.IsTrue(rack.Mensagens[0] == "Quantidade de gavetas deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Rack_Com_Descricao_Nula()
        {
            var rack = CriarNovoRack();
            rack.Descricao = null;
            Assert.IsFalse(rack.Validar());
            Assert.IsTrue(rack.Mensagens.Any());
            Assert.IsTrue(rack.Mensagens[0] == "Descrição não pode ser nula ou vazia.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Rack_Com_Descricao_Vazia()
        {
            var rack = CriarNovoRack();
            rack.Descricao = "  ";
            Assert.IsFalse(rack.Validar());
            Assert.IsTrue(rack.Mensagens.Any());
            Assert.IsTrue(rack.Mensagens[0] == "Descrição não pode ser nula ou vazia.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Rack_Com_Tensao_0()
        {
            var rack = CriarNovoRack();
            rack.Tensao = 0;
            Assert.IsFalse(rack.Validar());
            Assert.IsTrue(rack.Mensagens.Any());
            Assert.IsTrue(rack.Mensagens[0] == "Tensão deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Rack_Com_Tensao_MenorQue0()
        {
            var rack = CriarNovoRack();
            rack.Tensao = -10;
            Assert.IsFalse(rack.Validar());
            Assert.IsTrue(rack.Mensagens.Any());
            Assert.IsTrue(rack.Mensagens[0] == "Tensão deve ser maior que 0.");
        }

        private Rack CriarNovoRack()
        {
            return new Rack()
            {
                Id = 0,
                Tensao = 220,
                Descricao = "Gaveta 23",
                QuantidadeGavetas = 3
            };
        }
    }
}
