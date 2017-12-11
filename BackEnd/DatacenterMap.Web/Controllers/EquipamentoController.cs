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
    public class EquipamentoController : ControllerBasica
    {

        private IDatacenterMapContext contexto;

        public EquipamentoController()
        {

        }

        public EquipamentoController(IDatacenterMapContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [Route("api/equipamento")]
        public HttpResponseMessage CadastrarEquipamento([FromBody] EquipamentoModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            List<Gaveta> gavetasPedidas = contexto.Gavetas.Include(x => x.Rack).Where(x => request.GavetasId.Contains(x.Id)).ToList();

            if (gavetasPedidas.Count() != request.Tamanho)
                return BadRequest("A quantidade de gavetas encontradas não é igual ao tamanho do equipamento.");

            if (gavetasPedidas.Any(x => x.Ocupado == true))
                return BadRequest("Gaveta(s) ocupada(s).");

            if (gavetasPedidas.Any(x => x.Rack.Id != gavetasPedidas[0].Rack.Id))
                return BadRequest("As gavetas não são do mesmo rack.");

            int idRack = gavetasPedidas[0].Rack.Id;
            Rack rack = contexto.Racks.FirstOrDefault(x => x.Id == idRack);
            if (rack.Tensao != request.Tensao)
                return BadRequest("O rack não tem a mesma tensão do equipamento.");

            Equipamento equipamento = CreateEquipamento(request.Descricao, request.Tamanho, request.Tensao);

            if (equipamento.Validar())
            {
                contexto.Equipamentos.Add(equipamento);
                foreach(Gaveta gaveta in gavetasPedidas)
                {
                    gaveta.Ocupado = true;
                    gaveta.Equipamento = equipamento;
                }
                contexto.SaveChanges();

                return Ok(equipamento);
            }

            return BadRequest(equipamento.Mensagens);
        }

        // TO-DO: Adicionar mover equipamento
        
        [HttpPut]
        [Route("api/equipamento")]
        public HttpResponseMessage AlterarDescEquipamento([FromBody] EquipamentoModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Equipamento equipamentoAntigo = contexto.Equipamentos.FirstOrDefault(x => x.Id == request.Id);

            equipamentoAntigo.Descricao = request.Descricao;
            if (equipamentoAntigo.Validar())
            {
                contexto.SaveChanges();

                return Ok(equipamentoAntigo);
            }

            return BadRequest(equipamentoAntigo.Mensagens);
        }

        [HttpGet]
        [Route("api/equipamento/{id}")]
        public HttpResponseMessage GetEquipamento([FromUri] int id)
        {
            if (contexto.Equipamentos.Where(x => x.Id == id).Count() == 0) return BadRequest("Equipamento não encontrado.");

            Equipamento equipamento = contexto.Equipamentos.AsNoTracking().Include(x => x.Gavetas).FirstOrDefault(x => x.Id == id);

            return Ok(equipamento);
        }

        [HttpDelete]
        [Route("api/equipamento/{id}")]
        public HttpResponseMessage DeletarEquipamento([FromUri] int id)
        {
            if (contexto.Equipamentos.Where(x => x.Id == id).Count() == 0) return BadRequest("Equipamento não encontrado.");

            Equipamento equipamento = contexto.Equipamentos.FirstOrDefault(x => x.Id == id);

            contexto.Equipamentos.Remove(equipamento);
            contexto.SaveChanges();
            return Ok("Removido com Sucesso");
        }

        public Equipamento CreateEquipamento(string descricao, int tamanho, int tensao)
        {
            var equipamento = new Equipamento
            {
                Descricao = descricao,
                Tensao = tensao,
                Tamanho = tamanho
            };
            return equipamento;
        }
    }
}