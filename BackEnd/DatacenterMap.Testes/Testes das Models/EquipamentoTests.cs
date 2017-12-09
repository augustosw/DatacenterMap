using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatacenterMap.Domain.Entidades;
using System.Linq;
namespace DatacenterMap.Testes.Controllers
{
    [TestClass]
    public class EquipamentoTests
    {

        [TestMethod]
        public void Deve_Criar_Entidade_Equipamento_Valida()
        {
            var equipamento = CriarNovoEquipamento();
            Assert.IsTrue(equipamento.Validar());
            Assert.IsFalse(equipamento.Mensagens.Any());
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Equipamento_Com_Tamanho_0()
        {
            var equipamento = CriarNovoEquipamento();
            equipamento.Tamanho = 0;
            Assert.IsFalse(equipamento.Validar());
            Assert.IsTrue(equipamento.Mensagens.Any());
            Assert.IsTrue(equipamento.Mensagens[0] == "Tamanho ocupado deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Equipamento_Com_Tamanho_MenorQue0()
        {
            var equipamento = CriarNovoEquipamento();
            equipamento.Tamanho = -10;
            Assert.IsFalse(equipamento.Validar());
            Assert.IsTrue(equipamento.Mensagens.Any());
            Assert.IsTrue(equipamento.Mensagens[0] == "Tamanho ocupado deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Equipamento_Com_Descricao_Nula()
        {
            var equipamento = CriarNovoEquipamento();
            equipamento.Descricao = null;
            Assert.IsFalse(equipamento.Validar());
            Assert.IsTrue(equipamento.Mensagens.Any());
            Assert.IsTrue(equipamento.Mensagens[0] == "Descrição não pode ser nula ou vazia.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Equipamento_Com_Descricao_Vazia()
        {
            var equipamento = CriarNovoEquipamento();
            equipamento.Descricao = "  ";
            Assert.IsFalse(equipamento.Validar());
            Assert.IsTrue(equipamento.Mensagens.Any());
            Assert.IsTrue(equipamento.Mensagens[0] == "Descrição não pode ser nula ou vazia.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Equipamento_Com_Tensao_0()
        {
            var equipamento = CriarNovoEquipamento();
            equipamento.Tensao = 0;
            Assert.IsFalse(equipamento.Validar());
            Assert.IsTrue(equipamento.Mensagens.Any());
            Assert.IsTrue(equipamento.Mensagens[0] == "Tensão deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Equipamento_Com_Tensao_MenorQue0()
        {
            var equipamento = CriarNovoEquipamento();
            equipamento.Tensao = -10;
            Assert.IsFalse(equipamento.Validar());
            Assert.IsTrue(equipamento.Mensagens.Any());
            Assert.IsTrue(equipamento.Mensagens[0] == "Tensão deve ser maior que 0.");
        }

        private Equipamento CriarNovoEquipamento()
        {
            return new Equipamento()
            {
                Id = 0,
                Tensao = 220,
                Descricao = "Servidor",
                Tamanho = 1
            };
        }
    }
}
