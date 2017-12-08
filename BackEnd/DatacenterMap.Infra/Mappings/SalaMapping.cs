using DatacenterMap.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatacenterMap.Infra.Mappings
{
	internal class SalaMapping : EntityTypeConfiguration<Sala>
	{
		public SalaMapping()
		{
			ToTable("Sala", "DatacenterMap");

			HasKey(x => x.Id);

			Property(x => x.NumeroSala).HasColumnType("varchar").IsRequired();

			Property(x => x.QuantidadeMaximaSlots).IsRequired();

			Property(x => x.Largura).IsRequired();

			Property(x => x.Comprimento).IsRequired();

			HasRequired(x => x.Andar).WithRequiredDependent().Map(x => x.MapKey("Andar_Id"));

			HasMany(x => x.Slots).WithRequired().Map(x => x.MapKey("Sala_Id"));
		}

	}
}
