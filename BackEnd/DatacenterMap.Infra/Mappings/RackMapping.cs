using DatacenterMap.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			HasRequired(x => x.Slot).WithRequiredDependent().Map(x => x.MapKey("Slot_Id"));

			HasMany(x => x.Gavetas).WithRequired().Map(x => x.MapKey("Rack_Id"));
		}
	}
}
