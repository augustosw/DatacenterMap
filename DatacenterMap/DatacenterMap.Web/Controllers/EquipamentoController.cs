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
    [RoutePrefix("api/equipamento")]
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
        public HttpResponseMessage CadastrarEquipamento([FromBody] EquipamentoModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            List<Gaveta> gavetasPedidas = contexto.Gavetas.Include(x => x.Rack)
                                          .Where(x => request.GavetasId.Contains(x.Id))
                                          .OrderBy(x => x.Posicao).ToList();

            if (gavetasPedidas.Count() != request.Tamanho)
                return BadRequest("A quantidade de gavetas encontradas não é igual ao tamanho do equipamento.");

            if (gavetasPedidas.Any(x => x.Ocupado == true))
                return BadRequest("Gaveta(s) ocupada(s).");

            if (gavetasPedidas.Any(x => x.Rack.Id != gavetasPedidas[0].Rack.Id))
                return BadRequest("As gavetas não são do mesmo rack.");

            for(int i = 0; i < gavetasPedidas.Count()-1; i++)
            {
                if (gavetasPedidas[i].Posicao > gavetasPedidas[i + 1].Posicao)
                    return BadRequest("As gavetas pedidas não são consecutivas.");
            }

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

        [HttpPut]
        [Route("{idRack}/{idEquipamento}")]
        public HttpResponseMessage MoverEquipamento([FromUri] int idRack, int idEquipamento)
        {
            if (contexto.Equipamentos.Where(x => x.Id == idEquipamento).Count() == 0) return BadRequest("Equipamento não encontrado.");
            Equipamento equipamento = contexto.Equipamentos.Include(x => x.Gavetas).FirstOrDefault(x => x.Id == idEquipamento);

            if (contexto.Racks.Where(x => x.Id == idRack).Count() == 0) return BadRequest("Rack não encontrado.");

            Rack novoRack = contexto.Racks.FirstOrDefault(x => x.Id == idRack);
            List<Gaveta> gavetas = contexto.Gavetas.OrderBy(x => x.Posicao).Where(x => x.Rack.Id == novoRack.Id && !x.Ocupado).ToList();
        
            if (gavetas.Count()
                < equipamento.Tamanho) return BadRequest("Não há espaço no Rack.");

            List<Gaveta> gavetasSelecionadas = new List<Gaveta>();
            foreach (Gaveta g in gavetas)
            {
                // Se a próxima gaveta tem a posição consecutiva em relação a anterior, adiciona a gaveta às gavetas selecionadas
                if (gavetasSelecionadas.Count() == 0 || gavetasSelecionadas.ElementAt(gavetasSelecionadas.Count() - 1).Posicao + 1 == g.Posicao)
                {
                    gavetasSelecionadas.Add(g);
                }
                // Se não, limpa as gavetas selecionadas e recomeça
                else
                {
                    gavetasSelecionadas.Clear();
                    gavetasSelecionadas.Add(g);
                }

                // Se as gavetas selecionadas forem do tamanho correto, termina o For e retorna o resultado
                if (gavetasSelecionadas.Count() == equipamento.Tamanho)
                {
                    List<Gaveta> gavetaAntigas = contexto.Gavetas.Include(x => x.Equipamento).Where(x => x.Equipamento.Id == equipamento.Id).ToList();
                    gavetaAntigas.ForEach(x => { x.Equipamento = null; x.Ocupado = false;  });

                    equipamento.Gavetas = gavetasSelecionadas;
                    gavetasSelecionadas.ForEach(x => { x.Equipamento = equipamento; x.Ocupado = true; });

                    contexto.SaveChanges();

                    return Ok(equipamento);
                }
            }

            return BadRequest("Não existem gavetas consecutivas no rack para alocar o equipamento.");
        }

        [HttpPut]
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
        [Route("{id}")]
        public HttpResponseMessage GetEquipamento([FromUri] int id)
        {
            if (contexto.Equipamentos.Where(x => x.Id == id).Count() == 0) return BadRequest("Equipamento não encontrado.");

            Equipamento equipamento = contexto.Equipamentos.AsNoTracking().Include(x => x.Gavetas).FirstOrDefault(x => x.Id == id);

            return Ok(equipamento);
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeletarEquipamento([FromUri] int id)
        {
            if (contexto.Equipamentos.Where(x => x.Id == id).Count() == 0) return BadRequest("Equipamento não encontrado.");

            Equipamento equipamento = contexto.Equipamentos.FirstOrDefault(x => x.Id == id);

            contexto.Equipamentos.Remove(equipamento);
            contexto.SaveChanges();

            return Ok("Removido com Sucesso");
        }

        internal Equipamento CreateEquipamento(string descricao, int tamanho, int tensao)
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