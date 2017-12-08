using System.Data.Entity.ModelConfiguration;
using DatacenterMap.Domain.Entidades;

namespace DatacenterMap.Infra.Mappings
{
    internal class EquipametoMapping : EntityTypeConfiguration<Equipamento>
    {
        public EquipametoMapping()
        {
            ToTable("Equipamento", "DatacenterMap");

            HasKey(x => x.Id);

            Property(x => x.Descricao).HasColumnType("varchar").HasMaxLength(255).IsRequired();

            Property(x => x.Tamanho).IsRequired();

            Property(x => x.Tensao).IsRequired();

            // Mapear Lista de Gavetas
        }
    }
}
