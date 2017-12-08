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
            Slots = new List<Slot>();
        }

        public override bool Validar()
        {
            Mensagens.Clear();

            if (QuantidadeMaximaSlots <= 0)
                Mensagens.Add("Quantidade máxima de slots deve ser maior que 0.");

            if (Largura <= 0)
                Mensagens.Add("Largura deve ser maior que 0.");

            if (Comprimento <= 0)
                Mensagens.Add("Comprimento deve ser maior que 0.");
      
            if (string.IsNullOrWhiteSpace(NumeroSala))
                Mensagens.Add("Numero da sala é inválido.");

            return Mensagens.Count == 0;
        }
    }
}
