using System.Data.Entity;
using DatacenterMap.Domain.Entidades;

namespace DatacenterMap.Infra
{
    public interface IDatacenterMapContext
    {
        DbSet<Usuario> Usuarios { get; set; }

        int SaveChanges();
       
    }

}
