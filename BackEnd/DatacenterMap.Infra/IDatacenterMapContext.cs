using System.Data.Entity;
using DatacenterMap.Domain.Entidades;
using System.Data.Entity.Infrastructure;

namespace DatacenterMap.Infra
{
    public interface IDatacenterMapContext
    {
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Edificacao> Edificacoes { get; set; }
        DbSet<Andar> Andares { get; set; }
        DbSet<Sala> Salas { get; set; }
        DbSet<Slot> Slots { get; set; }
        DbSet<Gaveta> Gavetas { get; set; }
        DbSet<Rack> Racks { get; set; }
        DbSet<Equipamento> Equipamentos { get; set; }

        int SaveChanges();

       
    }

}
