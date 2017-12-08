using DatacenterMap.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			HasRequired(x => x.Rack).WithRequiredDependent().Map(x => x.MapKey("Rack_Id"));

			HasOptional(x => x.Equipamento).WithOptionalDependent().Map(x => x.MapKey("Equipamento_Id"));
		}
	}
}
