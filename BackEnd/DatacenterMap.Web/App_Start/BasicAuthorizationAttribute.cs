using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DatacenterMap.Web
{
    public class BasicAuthorization : AuthorizeAttribute
    {
        readonly IDatacenterMapContext contexto;

        public BasicAuthorization()
        {
            contexto = new DatacenterMapContext();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {

            if (PularAutenticacao(actionContext))
                return;

            if (actionContext.Request.Headers.Authorization == null)
            {
                var dnsHost = actionContext.Request.RequestUri.DnsSafeHost;
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", dnsHost));
                return;
            }
            else
            {
                string tokenAutenticacao =
                    actionContext.Request.Headers.Authorization.Parameter;

                string decodedTokenAutenticacao =
                    Encoding.Default.GetString(Convert.FromBase64String(tokenAutenticacao));

                // obtém o login e senha (usuario:senha)
                string[] userNameAndPassword = decodedTokenAutenticacao.Split(':');

                Usuario usuario = null;
                if (ValidarUsuario(userNameAndPassword[0], userNameAndPassword[1], out usuario))
                {
                    string[] papeis = { "Basic_Role" };

                    var identidade = new GenericIdentity(usuario.Email);
                    var genericUser = new GenericPrincipal(identidade, papeis);

                    if (string.IsNullOrEmpty(Roles))
                    {
                        Thread.CurrentPrincipal = genericUser;
                        if (HttpContext.Current != null)
                            HttpContext.Current.User = genericUser;

                        return;
                    }
                    else
                    {
                        var currentRoles = Roles.Split(',');
                        foreach (var currentRole in currentRoles)
                        {
                            if (genericUser.IsInRole(currentRole))
                            {

                                Thread.CurrentPrincipal = genericUser;
                                if (HttpContext.Current != null)
                                    HttpContext.Current.User = genericUser;

                                return;
                            }
                        }
                    }
                }
            }

            actionContext.Response =
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new { mensagens = new string[] { "Usuário ou senha inválidos." } });
        }

        // Check for authorization
        private static bool PularAutenticacao(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || (actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() &&
                       !actionContext.ActionDescriptor.GetCustomAttributes<BasicAuthorization>().Any());
        }

        private bool ValidarUsuario(string login, string senha, out Usuario usuarioRetorno)
        {
            usuarioRetorno = null;

            var usuario = contexto.Usuarios.AsNoTracking().Where(x => x.Email.Equals(login)).FirstOrDefault();

            if (usuario != null && usuario.ValidarSenha(senha))
                usuarioRetorno = usuario;
            else
                usuario = null;

            return usuario != null;
        }
    }
}