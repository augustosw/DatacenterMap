using System.Data.Entity.ModelConfiguration;
using DatacenterMap.Domain.Entidades;

namespace DatacenterMap.Infra.Mappings
{
    internal class UsuarioMapping : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMapping()
        {
            ToTable("Usuario", "DatacenterMap");

            HasKey(x => x.Id);

            Property(x => x.Nome).HasColumnType("varchar").HasMaxLength(64).IsRequired();

            Property(x => x.Email).HasColumnType("varchar").HasMaxLength(128).IsRequired();

            Property(x => x.Senha).HasColumnType("varchar").HasMaxLength(255).IsRequired();
        }
    }
}
