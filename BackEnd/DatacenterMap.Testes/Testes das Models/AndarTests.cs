using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatacenterMap.Domain.Entidades;
using System.Linq;
namespace DatacenterMap.Testes.Controllers
{
    [TestClass]
    public class AndarTests
    {

        [TestMethod]
        public void Deve_Criar_Entidade_Andar_Valida()
        {
            var andar = CriarNovoAndar();
            Assert.IsTrue(andar.Validar());
            Assert.IsFalse(andar.Mensagens.Any());
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Andar_Com_QuantidadeMaximaSalas_Menor_Que_0()
        {
            var andar = CriarNovoAndar();
            andar.QuantidadeMaximaSalas = -3;
            Assert.IsFalse(andar.Validar());
            Assert.IsTrue(andar.Mensagens.Any());
            Assert.IsTrue(andar.Mensagens[0] == "Quantidade máxima de salas deve ser maior que 0.");
        }

        [TestMethod]
        public void Nao_Deve_Validar_Entidade_Andar_Com_QuantidadeMaximaSalas_0()
        {
            var andar = CriarNovoAndar();
            andar.QuantidadeMaximaSalas = 0;
            Assert.IsFalse(andar.Validar());
            Assert.IsTrue(andar.Mensagens.Any());
            Assert.IsTrue(andar.Mensagens[0] == "Quantidade máxima de salas deve ser maior que 0.");
        }

        private Andar CriarNovoAndar()
        {
            return new Andar()
            {
                Id = 0,
                QuantidadeMaximaSalas = 4
            };
        }
    }
}
