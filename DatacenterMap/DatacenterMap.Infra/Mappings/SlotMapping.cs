using DatacenterMap.Domain.Entidades;
using System.Data.Entity.ModelConfiguration;

namespace DatacenterMap.Infra.Mappings
{
	internal class SlotMapping : EntityTypeConfiguration<Slot>
	{
		public SlotMapping()
		{
			ToTable("Slot", "DatacenterMap");

			HasKey(x => x.Id);

			Property(x => x.Ocupado).IsRequired();

		}
	}
}
