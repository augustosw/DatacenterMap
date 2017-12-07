using System.Collections.Generic;

namespace DatacenterMap.Domain.Entidades
{
    public class Gaveta : EntidadeBasica
    {

        public int Id { get; set; }
        public bool Ocupado { get; set; }
        public Rack Rack { get; set; }
        public List<Equipamento> Equipamentos { get; set; }

        public Gaveta()
        {
            Equipamentos = new List<Equipamento>();
        }

        public override bool Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}
