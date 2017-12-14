using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using System.Data.Entity;
using DatacenterMap.Web.Models;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;

namespace DatacenterMap.Web.Controllers
{
    [BasicAuthorization]
    [RoutePrefix("api/andar")]
    public class AndarController : ControllerBasica
    {

        private IDatacenterMapContext contexto;

        public AndarController()
        {

        }

        public AndarController(IDatacenterMapContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        public HttpResponseMessage CadastrarAndar([FromBody] AndarModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Edificacao edificacao = contexto.Edificacoes.FirstOrDefault(x => x.Id == request.EdificacaoId);

            if(edificacao.NumeroAndares < request.NumeroAndar) return BadRequest("O andar solicitado ultrapassa o limite máximo do prédio.");

            if (contexto.Andares.Where(x => x.Edificacao.Id == edificacao.Id && x.NumeroAndar == request.NumeroAndar).ToList().Count() != 0) return BadRequest("Já existe este andar no edifício.");

            Andar andar = CreateAndar(request.NumeroAndar, request.QuantidadeMaximaSalas, request.EdificacaoId);

            if (andar.Validar())
            {
                contexto.Andares.Add(andar);
                contexto.SaveChanges();

                return Ok(andar);
            }

            return BadRequest(andar.Mensagens);
        }

        [HttpPut]
        public HttpResponseMessage AlterarAndar([FromBody] AndarModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Andar andarAntigo = contexto.Andares.FirstOrDefault(x => x.Id == request.Id);

            Andar novoAndar = CreateAndar(request.NumeroAndar, request.QuantidadeMaximaSalas, request.EdificacaoId);

            if (novoAndar.Validar())
            {
                andarAntigo.QuantidadeMaximaSalas = request.QuantidadeMaximaSalas;
                contexto.SaveChanges();

                return Ok(andarAntigo);
            }

            return BadRequest(novoAndar.Mensagens); ;
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetAndar([FromUri] int id)
        {
            if (contexto.Andares.Where(x => x.Id == id).Count() == 0) return BadRequest("Edificação não encontrada.");

            Andar andar = contexto.Andares.AsNoTracking().Include(x => x.Salas).FirstOrDefault(x => x.Id == id);

            return Ok(andar);
        }

        [HttpGet]
        [Route("disponiveis/{edificacaoId}/{tamanho}")]
        public HttpResponseMessage GetAndaresDisponiveis([FromUri] int edificacaoId, int tamanho)
        {
            List<Andar> andares = contexto.Andares
                          .AsNoTracking()
                          .Include(x => x.Edificacao)
                          .Where(x => x.Edificacao.Id == edificacaoId
                                 && ControllerUtils.AndarHasRackDisponivel(contexto, x.Id, tamanho))
                          .ToList();

            return Ok(andares);

        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeletarAndar([FromUri] int id)
        {
            if (contexto.Andares.Where(x => x.Id == id).Count() == 0) return BadRequest("Andar não encontrado.");

            ControllerUtils.DeletarAndar(contexto, id);

            return Ok("Removido com Sucesso");
        }

        internal Andar CreateAndar(int numeroAndar, int quantidadeSalas, int edificacaoId)
        {
            var andar = new Andar
            {
                NumeroAndar = numeroAndar,
                QuantidadeMaximaSalas = quantidadeSalas,
                Edificacao = contexto.Edificacoes.FirstOrDefault(x => x.Id == edificacaoId)
            };

            return andar;
        }

    }
}