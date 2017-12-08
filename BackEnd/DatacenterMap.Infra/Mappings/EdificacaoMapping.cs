using System.Data.Entity.ModelConfiguration;
using DatacenterMap.Domain.Entidades;

namespace DatacenterMap.Infra.Mappings
{
    internal class EdificacaoMapping : EntityTypeConfiguration<Edificacao>
    {
        public EdificacaoMapping()
        {
            ToTable("Edificacao", "DatacenterMap");

            HasKey(x => x.Id);

            Property(x => x.NumeroAndares).IsRequired();

            Property(x => x.Nome).HasColumnType("varchar").HasMaxLength(64).IsRequired();

            Property(x => x.Latitude).IsRequired();

            Property(x => x.Longitude).IsRequired();

            // Mapear lista de Andares
        }
    }
}
