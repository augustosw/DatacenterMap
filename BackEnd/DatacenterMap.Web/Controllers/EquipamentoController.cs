using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using DatacenterMap.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace DatacenterMap.Web.Controllers
{
    [BasicAuthorization]
    [RoutePrefix("api/equipamento")]
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
        public IHttpActionResult CadastrarEquipamento([FromBody] EquipamentoModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            List<Gaveta> gavetasPedidas = contexto.Gavetas.Where(x => request.GavetasId.Contains(x.Id)).ToList();

            if (gavetasPedidas.Count() != request.Tamanho)
                return BadRequest("A quantidade de gavetas encontradas não é igual ao tamanho do equipamento.");

            if (gavetasPedidas.Any(x => x.Ocupado == true))
                return BadRequest("Gaveta(s) ocupada(s).");

            if (gavetasPedidas.Any(x => x.Rack.Id != gavetasPedidas[0].Rack.Id))
                return BadRequest("As gavetas não são do mesmo rack.");

            Rack rack = contexto.Racks.FirstOrDefault(x => x.Id == gavetasPedidas[0].Rack.Id);
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

            return (IHttpActionResult)BadRequest(equipamento.Mensagens);
        }

        // TO-DO: Adicionar mover equipamento
        
        [HttpPut]
        public IHttpActionResult AlterarDescEquipamento([FromBody] EquipamentoModel request)
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

            return (IHttpActionResult)BadRequest(equipamentoAntigo.Mensagens);
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetEquipamento([FromUri] int id)
        {
            if (contexto.Equipamentos.Where(x => x.Id == id).Count() == 0) return BadRequest("Equipamento não encontrado.");

            Equipamento equipamento = contexto.Equipamentos.AsNoTracking().Include(x => x.Gavetas).FirstOrDefault(x => x.Id == id);

            return Ok(equipamento);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeletarEquipamento([FromUri] int id)
        {
            if (contexto.Edificacoes.Where(x => x.Id == id).Count() == 0) return BadRequest("Equipamento não encontrado.");

            Equipamento equipamento = contexto.Equipamentos.FirstOrDefault(x => x.Id == id);

            contexto.Equipamentos.Remove(equipamento);

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