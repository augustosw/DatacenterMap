using DatacenterMap.Domain.Entidades;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatacenterMap.Web.Controllers
{
    public class ControllerBasica : ApiController
    {

        public HttpResponseMessage Ok(object dados = null)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new ObjectContent(dados.GetType(), dados, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
            return response;
        }

        public HttpResponseMessage BadRequest(List<string> mensagens)
        {
            var response = Request.CreateResponse(HttpStatusCode.BadRequest);
            response.Content = new ObjectContent(mensagens.GetType(), mensagens, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
            return response;
        }

        public HttpResponseMessage BadRequest(params string[] mensagens)
        {
            var response = Request.CreateResponse(HttpStatusCode.BadRequest);
            response.Content = new ObjectContent(mensagens.GetType(), mensagens, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
            return response;
        }

        public HttpResponseMessage BadRequest(IEnumerable<string> mensagens)
        {
            var response = Request.CreateResponse(HttpStatusCode.BadRequest);
            response.Content = new ObjectContent(mensagens.GetType(), mensagens, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
            return response;
        }
    }
}