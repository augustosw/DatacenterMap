using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatacenterMap.Domain.Repositorios
using System.Threading.Tasks;
using DatacenterMap.Domain.Entidades;

namespace DatacenterMap.Testes.MockRepositorios
{
	public class UsuarioRepositorioMock : IUsuarioRepositorio
	{
		private readonly List<Usuario> _usuarios;
		private int _incremento = 0;

		public UsuarioRepositorioMock()
		{
			_usuarios = new List<Usuario>();
		}

		public void Criar(Usuario entidade)
		{
			entidade.Id = ++_incremento;
			_usuarios.Add(entidade);
		}

		public void Excluir(int id)
		{
			_usuarios.Remove(Obter(id));
		}

		public List<Usuario> Listar()
		{
			throw new NotImplementedException();
		}

		public Usuario Obter(int id)
		{
			return _usuarios.FirstOrDefault(u => u.Id == id);
		}
	}
}
