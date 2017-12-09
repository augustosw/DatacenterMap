using System.Data.Entity.ModelConfiguration;
using DatacenterMap.Domain.Entidades;

namespace DatacenterMap.Infra.Mappings
{
    internal class AndarMapping : EntityTypeConfiguration<Andar>
    {
        public AndarMapping()
        {
            ToTable("Andar", "DatacenterMap");

            HasKey(x => x.Id);

            Property(x => x.NumeroAndar).IsRequired();

            Property(x => x.QuantidadeMaximaSalas).IsRequired();
		}
    }
}
