using DatacenterMap.Domain;
using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using DatacenterMap.Web.Models;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace DatacenterMap.Web.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ControllerBasica
    {

        private IDatacenterMapContext contexto;

        public UsuarioController()
        {

        }

        public UsuarioController(IDatacenterMapContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        public HttpResponseMessage CadastrarUsuario([FromBody] UsuarioModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            if (contexto.Usuarios.Where(x => x.Email == request.Email).Count() != 0) return BadRequest("Já existe uma conta com esse email.");

            Usuario usuario = MontarUsuario(request.Nome, request.Email, request.Senha);

            if (usuario.Validar())
            {
                contexto.Usuarios.Add(usuario);
                contexto.SaveChanges();

                return Ok(usuario);
            }

            return BadRequest(usuario.Mensagens);
        }

        [HttpPut]
        public HttpResponseMessage AlterarUsuario([FromBody] UsuarioModel request)
        {
            if (request == null)
                return BadRequest($"O parametro {nameof(request)} não pode ser null");

            var usuarioLogado = UsuarioLogado();

            if (usuarioLogado == null) return BadRequest("Usuário inexistente");

            if (usuarioLogado.Validar())
            {
                usuarioLogado.Nome = request.Nome;
                usuarioLogado.Senha = request.Senha;
                usuarioLogado.Email = request.Email;
                contexto.SaveChanges();

                return Ok(usuarioLogado);
            }

            return BadRequest(usuarioLogado.Mensagens); ;
        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Logar(LoginModel login)
        {
            var usuario = contexto.Usuarios
                        .AsNoTracking()
                        .FirstOrDefault(x => x.Email == login.Email);

            if (usuario == null)
                return BadRequest("Não encontrado.");

            var senhaCorreta = usuario.ValidarSenha(login.Senha);

            if (senhaCorreta && usuario != null) return Ok(usuario);

            return BadRequest("Usuário ou senha inválido");
        }

        internal Usuario UsuarioLogado()
        {
            var usuario = contexto.Usuarios
                       .FirstOrDefault(x => x.Email == User.Identity.Name);

            return usuario;
        }

        public Usuario MontarUsuario(string nome, string email, string senha)
        {
            if (!string.IsNullOrWhiteSpace(senha))
                senha = Criptografia.CriptografarSenha(email, senha);

            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senha
            };
            return usuario;
        }
   
    }
}