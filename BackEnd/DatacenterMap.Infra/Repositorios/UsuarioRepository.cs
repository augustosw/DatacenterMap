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
