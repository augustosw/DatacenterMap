using DatacenterMap.Domain.Entidades;
using System.Data.Entity;
//using DatacenterMap.Infra.Mappings;

namespace DatacenterMap.Infra
{

    public class BookingContext : DbContext, IDatacenterMapContext
    {

        public BookingContext() : this("name=DatacenterMap") { }

        public BookingContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Configurations.Add(new UsuarioMapping());

            base.OnModelCreating(modelBuilder);
        }
    }

}
