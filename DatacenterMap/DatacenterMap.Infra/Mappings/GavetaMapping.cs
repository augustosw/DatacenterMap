using DatacenterMap.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace DatacenterMap.Infra.Mappings
{
	internal class GavetaMapping : EntityTypeConfiguration<Gaveta>
	{
		public GavetaMapping()
		{
			ToTable("Gaveta", "DatacenterMap");

			HasKey(x => x.Id);

			Property(x => x.Ocupado).IsRequired();

			Property(x => x.Posicao).IsRequired();
           
		}
	}
}
