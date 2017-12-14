using DatacenterMap.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace DatacenterMap.Infra.Mappings
{
	internal class SalaMapping : EntityTypeConfiguration<Sala>
	{
		public SalaMapping()
		{
			ToTable("Sala", "DatacenterMap");

			HasKey(x => x.Id);

			Property(x => x.NumeroSala).HasColumnType("varchar").HasMaxLength(80).IsRequired();

			Property(x => x.QuantidadeMaximaSlots).IsRequired();

			Property(x => x.Largura).IsRequired();

			Property(x => x.Comprimento).IsRequired();
		}

	}
}
