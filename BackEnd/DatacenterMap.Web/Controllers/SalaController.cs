using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using DatacenterMap.Web.Models;
using System.Linq;
using System.Web.Http;

namespace DatacenterMap.Web.Controllers
{
    [RoutePrefix("api/sala")]
    public class SalaController : ControllerBasica
    {

        private IDatacenterMapContext contexto;

        public SalaController()
        {

        }

        public SalaController(IDatacenterMapContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        public IHttpActionResult CadastrarSala([FromBody] SalaModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Andar andar = contexto.Andares.AsNoTracking().Where(x => x.Id == request.AndarId).FirstOrDefault();

            if (contexto.Salas.Where(x => x.Andar == andar && x.NumeroSala == request.NumeroSala).Count() != 0) return BadRequest("Já existe uma sala com esse número nesse andar.");

            Sala sala = CreateSala(request.NumeroSala, request.QuantidadeMaximaSlots, request.Largura, request.Comprimento);

            if (sala.Validar())
            {
                contexto.Salas.Add(sala);
                contexto.SaveChanges();

                return Ok(sala);
            }

            return (IHttpActionResult)BadRequest(sala.Mensagens);
        }

        [HttpPut]
        public IHttpActionResult AlterarSala([FromBody] SalaModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Sala salaAntiga = contexto.Salas.Where(x => x.Id == request.Id).FirstOrDefault();

            Sala salaNova = CreateSala(request.NumeroSala, request.QuantidadeMaximaSlots, request.Largura, request.Comprimento);

            if (salaNova.Validar())
            {
                // Possível alterar o comprimento, largura e quantidade máxima de slots
                salaAntiga.Comprimento = request.Comprimento;
                salaAntiga.Largura = request.Largura;
                salaAntiga.QuantidadeMaximaSlots = request.QuantidadeMaximaSlots;
                contexto.SaveChanges();

                return Ok(salaAntiga);
            }

            return (IHttpActionResult)BadRequest(salaNova.Mensagens);
        }

        [HttpDelete]
        [Route("/{id}")]
        public IHttpActionResult DeletarSala([FromUri] int id)
        {
            if (contexto.Edificacoes.Where(x => x.Id == id).Count() == 0) return BadRequest("Sala não encontrada.");

            Sala sala = contexto.Salas.Where(x => x.Id == id).FirstOrDefault();
            contexto.Salas.Remove(sala);

            return Ok("Removido com Sucesso");
        }

        public Sala CreateSala(string numeroSala, int quantidadeMaximaSlots, double largura, double comprimento)
        {
            var sala = new Sala
            {
                NumeroSala = numeroSala,
                QuantidadeMaximaSlots = quantidadeMaximaSlots,
                Largura = largura,
                Comprimento = comprimento
            };

            return sala;
        }

    }
}