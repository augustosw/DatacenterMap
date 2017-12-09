using DatacenterMap.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace DatacenterMap.Infra.Mappings
{
	internal class RackMapping : EntityTypeConfiguration<Rack>
	{
		public RackMapping()
		{
			ToTable("Rack", "DatacenterMap");

			HasKey(x => x.Id);

			Property(x => x.QuantidadeGavetas).IsRequired();

			Property(x => x.Tensao).IsRequired();

			Property(x => x.Descricao).HasColumnType("varchar").HasMaxLength(255).IsRequired();

		}
	}
}
