using DatacenterMap.Domain.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace DatacenterMap.Infra.Repositorios
{
    public class UsuarioRepository
    {
        static readonly Dictionary<string, Usuario> _usuarios = new Dictionary<string, Usuario>();

        static UsuarioRepository()
        {
            // YWRtaW5AY3dpLmNvbS5icjoxMjM0NTY=
            var usrAdmin = new Usuario("admin", "admin@cwi.com.br", "123456");
            _usuarios.Add(usrAdmin.Email, usrAdmin);

            // Z2lvdmFuaUBjd2kuY29tLmJyOjEyMzQ1Ng==
            var usrGiovani = new Usuario("giovani", "giovani@cwi.com.br", "123456");
            _usuarios.Add(usrGiovani.Email, usrGiovani);
        }

        public UsuarioRepository()
        {

        }

        public void Criar(Usuario usuario)
        {
            _usuarios.Add(usuario.Email, usuario);
        }

        public void Alterar(Usuario usuario)
        {
            _usuarios[usuario.Email] = usuario;
        }
        public void Excluir(Usuario usuario)
        {
            _usuarios[usuario.Email] = usuario;
        }

        public IEnumerable<Usuario> Listar()
        {
            return _usuarios.Select(u => u.Value);
        }

        public Usuario Obter(string email)
        {
            return _usuarios.Where(u => u.Key == email).Select(u => u.Value).FirstOrDefault();
        }
    }
}
