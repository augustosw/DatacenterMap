using System.Collections.Generic;

namespace DatacenterMap.Domain.Entidades
{
    public class Sala : EntidadeBasica
    {

        public int Id { get; set; }
        public string NumeroSala { get; set; }
        public int QuantidadeMaximaSlots { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public Andar Andar { get; set; }
        public List<Slot> Slots { get; set; }

        public Sala()
        {
            
        }

        public override bool Validar()
        {
            Mensagens.Clear();

            if (QuantidadeMaximaSlots <= 0)
                Mensagens.Add("Quantidade máxima de slots deve ser maior que 0.");

            return Mensagens.Count == 0;
        }
    }
}
