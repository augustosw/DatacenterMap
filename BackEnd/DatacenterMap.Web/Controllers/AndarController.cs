using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using DatacenterMap.Web.Models;
using System.Linq;
using System.Web.Http;

namespace DatacenterMap.Web.Controllers
{
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
        public IHttpActionResult CadastrarAndar([FromBody] AndarModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            Edificacao edificacao = contexto.Edificacoes.AsNoTracking().FirstOrDefault(x => x.Id == request.EdificacaoId);

            if(edificacao.NumeroAndares < request.NumeroAndar) return BadRequest("O andar solicitado ultrapassa o limite máximo do prédio.");

            if (contexto.Andares.Where(x => x.Edificacao == edificacao && x.NumeroAndar == request.NumeroAndar).Count() != 0) return BadRequest("Já existe este andar no edifício.");

            Andar andar = CreateAndar(request.NumeroAndar, request.QuantidadeMaximaSalas, request.EdificacaoId);

            if (andar.Validar())
            {
                contexto.Andares.Add(andar);
                contexto.SaveChanges();

                return Ok(andar);
            }

            return (IHttpActionResult)BadRequest(andar.Mensagens);
        }

        [HttpPut]
        public IHttpActionResult AlterarAndar([FromBody] AndarModel request)
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

            return (IHttpActionResult)BadRequest(novoAndar.Mensagens); ;
        }

        [HttpDelete]
        [Route("/{id}")]
        public IHttpActionResult DeletarAndar([FromUri] int id)
        {
            if (contexto.Andares.Where(x => x.Id == id).Count() == 0) return BadRequest("Andar não encontrado.");

            Andar andar = contexto.Andares.FirstOrDefault(x => x.Id == id);
            contexto.Andares.Remove(andar);

            return Ok("Removido com Sucesso");
        }

        public Andar CreateAndar(int numeroAndar, int quantidadeSalas, int edificacaoId)
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