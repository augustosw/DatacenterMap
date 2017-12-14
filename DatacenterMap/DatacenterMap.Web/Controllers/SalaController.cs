using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using DatacenterMap.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace DatacenterMap.Web.Controllers
{
    [BasicAuthorization]
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
        public HttpResponseMessage CadastrarSala([FromBody] SalaModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Andar andar = contexto.Andares.FirstOrDefault(x => x.Id == request.AndarId);

            if (contexto.Salas.Where(x => x.Andar.Id == andar.Id).Count() >= andar.QuantidadeMaximaSalas)
                return BadRequest("Quantidade máxima de salas ultrapassada.");

            if(contexto.Salas.Where(x => x.Andar.Id == andar.Id && x.NumeroSala.Equals(request.NumeroSala)).Count() != 0)
                return BadRequest("Já existe uma sala com esse número nesse andar.");

            Sala sala = CreateSala(request.NumeroSala, request.QuantidadeMaximaSlots, request.Largura, request.Comprimento);
            sala.Andar = andar;

            andar.Salas.Add(sala);

            if (sala.Validar())
            {
                for (var i = 0; i < sala.QuantidadeMaximaSlots; i++)
                {
                    contexto.Slots.Add(CreateSlot(sala));
                }
                contexto.SaveChanges();

                return Ok(sala);
            }

            return BadRequest(sala.Mensagens);
        }

        [HttpPut]
        public HttpResponseMessage AlterarSala([FromBody] SalaModel request)
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

            return BadRequest(salaNova.Mensagens);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeletarSala([FromUri] int id)
        {
            if (contexto.Salas.Where(x => x.Id == id).Count() == 0) return BadRequest("Sala não encontrada.");

            ControllerUtils.DeletarSala(contexto, id);
            contexto.SaveChanges();

            return Ok("Removido com Sucesso");
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetSala([FromUri] int id)
        {
            if (contexto.Salas.Where(x => x.Id == id).Count() == 0) return BadRequest("Sala não encontrada.");

            Sala sala = contexto.Salas.Include(x => x.Slots).AsNoTracking().FirstOrDefault(x => x.Id == id);

            return Ok(sala);
        }

        [HttpGet]
        [Route("disponiveis/{andarId}/{tamanho}")]
        public HttpResponseMessage GetSalasDisponiveis([FromUri] int andarId, int tamanho)
        {
            List<Sala> salas = contexto.Salas
                          .AsNoTracking()
                          .Where(x => x.Andar.Id == andarId
                                 && ControllerUtils.SalaHasRackDisponivel(contexto, x.Id, tamanho))
                          .ToList();

            return Ok(salas);
            
        }

        internal Sala CreateSala(string numeroSala, int quantidadeMaximaSlots, double largura, double comprimento)
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

        internal Slot CreateSlot(Sala sala)
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