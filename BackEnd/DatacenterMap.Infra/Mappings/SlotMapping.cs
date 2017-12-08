using DatacenterMap.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatacenterMap.Infra.Mappings
{
	internal class SlotMapping : EntityTypeConfiguration<Slot>
	{
		public SlotMapping()
		{
			ToTable("Slot", "DatacenterMap");

			HasKey(x => x.Id);

			Property(x => x.Ocupado).IsRequired();

			HasRequired(x => x.Sala).WithMany().Map(x => x.MapKey("Sala_Id"));
		}
	}
}
