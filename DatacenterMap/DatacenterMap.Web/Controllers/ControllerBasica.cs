using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatacenterMap.Web.Controllers
{
    public class ControllerBasica : ApiController
    {

        public HttpResponseMessage OK(object dados = null)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { dados });
        }

        public HttpResponseMessage BadRequest(params string[] mensagens)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { mensagens });
        }

        public HttpResponseMessage BadRequest(IEnumerable<string> mensagens)
        {
            return Request.CreateResponse(HttpStatusCode.BadRequest, new { mensagens });
        }
    }
}