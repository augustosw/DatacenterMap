using System.Collections.Generic;

namespace DatacenterMap.Domain.Entidades
{
    public class Gaveta : EntidadeBasica
    {

        public int Id { get; set; }
        public bool Ocupado { get; set; }
        public int Posicao { get; set; }
        public Rack Rack { get; set; }
        public Equipamento Equipamento { get; set; }

        public Gaveta()
        {
            
        }

        public override bool Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}
