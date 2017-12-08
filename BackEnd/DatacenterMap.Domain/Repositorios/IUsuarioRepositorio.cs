using DatacenterMap.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatacenterMap.Domain.Repositorios
{
	public interface IUsuarioRepositorio
	{
		Usuario Obter(int id);
		List<Usuario> Listar();
		void Criar(Usuario entidade);
		void Excluir(int id);
	}
}

