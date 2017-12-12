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
    [RoutePrefix("api/rack")]
    public class RackController : ControllerBasica
    {

        private IDatacenterMapContext contexto;

        public RackController()
        {

        }

        public RackController(IDatacenterMapContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        public HttpResponseMessage CadastrarRack([FromBody] RackModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Slot slot = contexto.Slots.FirstOrDefault(x => x.Id == request.SlotId);
            slot.Ocupado = true;

            if (contexto.Racks.Where(x => x.Slot.Id == slot.Id).Count() != 0)
                return BadRequest("Já existe um rack neste slot.");

            Rack rack = MontarRack(request.QuantidadeGavetas, request.Tensao, request.Descricao);
            rack.Slot = slot;

            if (rack.Validar())
            {
                for (var i = 0; i < rack.QuantidadeGavetas; i++)
                {
                    contexto.Gavetas.Add(MontarGaveta(rack, i + 1));
                }
                contexto.SaveChanges();

                return Ok(rack);
            }

            return BadRequest(rack.Mensagens);
        }

        [HttpPut]
        public HttpResponseMessage AlterarRack([FromBody] RackModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Rack rackAntiga = contexto.Racks.FirstOrDefault(x => x.Id == request.Id);

            Rack rackNova = MontarRack(request.QuantidadeGavetas, request.Tensao, request.Descricao);

            if (rackNova.QuantidadeGavetas < rackAntiga.QuantidadeGavetas)
                return BadRequest("A quantidade máxima de gavetas não pode ser diminuida.");

            if (rackNova.Validar())
            {
                rackAntiga.QuantidadeGavetas = request.QuantidadeGavetas;
                rackAntiga.Descricao = request.Descricao;
                int quantidadeExtrasSlots = request.QuantidadeGavetas - rackAntiga.QuantidadeGavetas;
                for (var i = 0; i < quantidadeExtrasSlots; i++)
                {
                    contexto.Gavetas.Add(MontarGaveta(rackAntiga, (rackAntiga.QuantidadeGavetas+i+1)));
                }

                contexto.SaveChanges();

                return Ok(rackAntiga);
            }

            return BadRequest(rackNova.Mensagens);
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetRack([FromUri] int id)
        {
            if (contexto.Racks.Where(x => x.Id == id).Count() == 0) return BadRequest("Rack não encontrado.");

            Rack rack = contexto.Racks.AsNoTracking().FirstOrDefault(x => x.Id == id);
            rack.Gavetas = contexto.Gavetas.AsNoTracking().Include(x => x.Rack).Where(x => x.Rack.Id == rack.Id).Include(x => x.Equipamento).ToList();

            return Ok(rack);
        }

        [HttpGet]
        [Route("disponiveis/{salaId}/{tamanho}")]
        public HttpResponseMessage GetRacksDisponiveis([FromUri] int salaId, int tamanho)
        {
            List<Slot> slots = contexto.Slots
                          .AsNoTracking()
                          .Include(x => x.Sala)
                          .Where(x => x.Sala.Id == salaId)
                          .ToList();

            List<int> slotsId = new List<int>();
            slots.ForEach(x => slotsId.Add(x.Id));

            List<Rack> racks = contexto.Racks.Include(x => x.Slot)
                          .AsNoTracking()
                          .Where(x => slotsId.Contains(x.Id) && ControllerUtils.RackIsDisponivel(contexto, x.Id, tamanho))
                          .ToList();

            return Ok(racks);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeletarRack([FromUri] int id)
        {
            if (contexto.Racks.Where(x => x.Id == id).Count() == 0) return BadRequest("Rack não encontrado.");

            Rack rack = contexto.Racks.Include(x => x.Gavetas).Include(x => x.Slot).FirstOrDefault(x => x.Id == id);
            rack.Slot.Ocupado = false;

            contexto.Gavetas.RemoveRange(rack.Gavetas);
            contexto.Racks.Remove(rack);
            contexto.SaveChanges();

            return Ok("Removido com Sucesso");
        }

        [HttpDelete]
        [Route("limpar/{id}")]
        public HttpResponseMessage LimparRack([FromUri] int id)
        {
            if (contexto.Racks.Where(x => x.Id == id).Count() == 0) return BadRequest("Rack não encontrado.");

            Rack rack = contexto.Racks.Include(x => x.Gavetas).FirstOrDefault(x => x.Id == id);
            rack.Gavetas.ForEach(x => x.Ocupado = false);

            List<Equipamento> equipamentosParaRemover = new List<Equipamento>();

            contexto.Gavetas.Include(x => x.Equipamento)
                .Where(x => x.Equipamento != null && x.Ocupado == false).ToList()
                .ForEach(x => equipamentosParaRemover.Add(x.Equipamento));

            contexto.Equipamentos.RemoveRange(equipamentosParaRemover);
            contexto.SaveChanges();

            return Ok("Todos os Equipamentos foram removidos.");
        }

        public Rack MontarRack(int quantidadeGavetas, int tensao, string descricao)
        {
            var rack = new Rack
            {
                QuantidadeGavetas = quantidadeGavetas,
                Tensao = tensao,
                Descricao = descricao
            };
            return rack;
        }

        public Gaveta MontarGaveta(Rack rack, int posicao)
        {
            var gaveta = new Gaveta
            {
                Ocupado = false,
                Posicao = posicao,
                Rack = rack
            };
            return gaveta;
        }
    }
}