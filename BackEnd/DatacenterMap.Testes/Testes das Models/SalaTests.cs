using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatacenterMap.Domain.Entidades;
using System.Linq;
namespace DatacenterMap.Testes.Controllers
{
    [TestClass]
    public class SalaTests
    {

        [TestMethod]
        public void Deve_Criar_Entidade_Sala_Valida()
        {
            var sala = CriarNovoSala();
            Assert.IsTrue(sala.Validar());
            Assert.IsFalse(sala.Mensagens.Any());
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Sala_Sem_NumeroSala()
        {
            var sala = CriarNovoSala();
            sala.NumeroSala = null;
            Assert.IsFalse(sala.Validar());
            Assert.IsTrue(sala.Mensagens.Any());
            Assert.IsTrue(sala.Mensagens[0] == "Numero da sala é inválido.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Sala_Com_NumeroSala_Vazio()
        {
            var sala = CriarNovoSala();
            sala.NumeroSala = " ";
            Assert.IsFalse(sala.Validar());
            Assert.IsTrue(sala.Mensagens.Any());
            Assert.IsTrue(sala.Mensagens[0] == "Numero da sala é inválido.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Sala_Com_Largura_Menor_Que_0()
        {
            var sala = CriarNovoSala();
            sala.Largura = -10;
            Assert.IsFalse(sala.Validar());
            Assert.IsTrue(sala.Mensagens.Any());
            Assert.IsTrue(sala.Mensagens[0] == "Largura deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Sala_Com_Largura_0()
        {
            var sala = CriarNovoSala();
            sala.Largura = 0;
            Assert.IsFalse(sala.Validar());
            Assert.IsTrue(sala.Mensagens.Any());
            Assert.IsTrue(sala.Mensagens[0] == "Largura deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Sala_Com_Comprimento_Menor_Que_0()
        {
            var sala = CriarNovoSala();
            sala.Comprimento = -10;
            Assert.IsFalse(sala.Validar());
            Assert.IsTrue(sala.Mensagens.Any());
            Assert.IsTrue(sala.Mensagens[0] == "Comprimento deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Sala_Com_Comprimento_0()
        {
            var sala = CriarNovoSala();
            sala.Comprimento = 0;
            Assert.IsFalse(sala.Validar());
            Assert.IsTrue(sala.Mensagens.Any());
            Assert.IsTrue(sala.Mensagens[0] == "Comprimento deve ser maior que 0.");
        }
        
        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Sala_Com_Slots_Menor_Que_0()
        {
            var sala = CriarNovoSala();
            sala.QuantidadeMaximaSlots = -1;
            Assert.IsFalse(sala.Validar());
            Assert.IsTrue(sala.Mensagens.Any());
            Assert.IsTrue(sala.Mensagens[0] == "Quantidade máxima de slots deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Sala_Com_Slots_0()
        {
            var sala = CriarNovoSala();
            sala.QuantidadeMaximaSlots = 0;
            Assert.IsFalse(sala.Validar());
            Assert.IsTrue(sala.Mensagens.Any());
            Assert.IsTrue(sala.Mensagens[0] == "Quantidade máxima de slots deve ser maior que 0.");
        }

        private Sala CriarNovoSala()
        {
            return new Sala()
            {
                Id = 0,
                NumeroSala = "2-1",
                QuantidadeMaximaSlots = 3,
                Comprimento = 100,
                Largura = 80
            };
        }
    }
}
