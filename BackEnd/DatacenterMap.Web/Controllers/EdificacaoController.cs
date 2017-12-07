using DatacenterMap.Domain;
using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using DatacenterMap.Web.Models;
using System.Linq;
using System.Web.Http;

namespace DatacenterMap.Web.Controllers
{
    [RoutePrefix("api/edificacao")]
    public class EdificacaoController : ControllerBasica
    {

        private IDatacenterMapContext contexto;

        public EdificacaoController()
        {

        }

        public EdificacaoController(IDatacenterMapContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        public IHttpActionResult CadastrarEdificacao([FromBody] EdificacaoModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            if (contexto.Edificacoes.Where(x => x.Latitude == request.Latitude && x.Longitude == request.Longitude).Count() != 0) return BadRequest("Já existe uma edificação nessa localização.");

            if (contexto.Edificacoes.Where(x => x.Nome == request.Nome).Count() != 0) return BadRequest("Já existe uma edificação com esse nome.");

            Edificacao edificacao = CreateEdificacao(request.Nome, request.NumeroAndares, request.Longitude, request.Latitude);

            if (edificacao.Validar())
            {
                contexto.Edificacoes.Add(edificacao);
                contexto.SaveChanges();

                return Ok(edificacao);
            }

            return (IHttpActionResult)BadRequest(edificacao.Mensagens);
        }

        [HttpDelete]
        [Route("/{id}")]
        public IHttpActionResult DeletarEdificacao([FromUri] int id)
        {
            if (contexto.Edificacoes.Where(x => x.Id == id).Count() == 0) return BadRequest("Edificação não encontrada.");

            Edificacao edificacao = contexto.Edificacoes.Where(x => x.Id == id).FirstOrDefault();
            contexto.Edificacoes.Remove(edificacao);

            return Ok("Removido com Sucesso");
        }

        public Edificacao CreateEdificacao(string nome, int numeroAndares, double longitude, double latitude)
        {
            var edificacao = new Edificacao
            {
                Nome = nome,
                NumeroAndares = numeroAndares,
                Longitude = longitude,
                Latitude = latitude
            };

            return edificacao;
        }

    }
}