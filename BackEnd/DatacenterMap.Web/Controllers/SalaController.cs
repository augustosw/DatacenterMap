using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using DatacenterMap.Web.Models;
using System.Collections.Generic;
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

            Andar andar = contexto.Andares.AsNoTracking().FirstOrDefault(x => x.Id == request.AndarId);

            if (contexto.Salas.Where(x => x.Andar == andar && x.NumeroSala.Equals(request.NumeroSala)).Count() != 0)
                return BadRequest("Já existe uma sala com esse número nesse andar.");

            Sala sala = CreateSala(request.NumeroSala, request.QuantidadeMaximaSlots, request.Largura, request.Comprimento);

            if (sala.Validar())
            {
                contexto.Salas.Add(sala);
                for (var i = 0; i < sala.QuantidadeMaximaSlots; i++)
                {
                    contexto.Slots.Add(CreateSlot(sala));
                }
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

            Sala salaAntiga = contexto.Salas.FirstOrDefault(x => x.Id == request.Id);

            Sala salaNova = CreateSala(request.NumeroSala, request.QuantidadeMaximaSlots, request.Largura, request.Comprimento);

            if (salaNova.QuantidadeMaximaSlots < salaAntiga.QuantidadeMaximaSlots)
                return BadRequest("A quantidade máxima de slots não pode ser diminuida.");

            if (salaNova.Validar())
            {
                salaAntiga.QuantidadeMaximaSlots = request.QuantidadeMaximaSlots;

                int quantidadeExtrasSlots = request.QuantidadeMaximaSlots - salaAntiga.QuantidadeMaximaSlots;
                for(var i=0; i < quantidadeExtrasSlots; i++)
                {
                    contexto.Slots.Add(CreateSlot(salaAntiga));
                }

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

            Sala sala = contexto.Salas.FirstOrDefault(x => x.Id == id);

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

        public Slot CreateSlot(Sala sala)
        {
            var slot = new Slot
            {
                Ocupado = false,
                Sala = sala
            };

            return slot;
        }

    }
}