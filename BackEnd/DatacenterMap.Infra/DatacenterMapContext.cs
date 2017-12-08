using DatacenterMap.Domain.Entidades;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
//using DatacenterMap.Infra.Mappings;

namespace DatacenterMap.Infra
{

    public class DatacenterMapContext : DbContext, IDatacenterMapContext
    {


        public DatacenterMapContext() : this("name=DatacenterMap") { }

        public DatacenterMapContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Edificacao> Edificacoes { get; set; }
        public DbSet<Andar> Andares { get; set; }
        public DbSet<Sala> Salas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Configurations.Add(new UsuarioMapping());

            base.OnModelCreating(modelBuilder);
        }
    }

}
