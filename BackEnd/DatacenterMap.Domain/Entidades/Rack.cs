using System.Collections.Generic;

namespace DatacenterMap.Domain.Entidades
{
    public class Rack : EntidadeBasica
    {

        public int Id { get; set; }
        public int QuantidadeGavetas { get; set; }
        public int Tensao { get; set; }
        public string Descricao { get; set; }
        public Slot Slot { get; set; }
        public List<Gaveta> Gavetas { get; set; }
        
        public Rack()
        {
            Gavetas = new List<Gaveta>();
        }

        public override bool Validar()
        {
            Mensagens.Clear();

            if (QuantidadeGavetas <= 0)
                Mensagens.Add("Quantidade de gavetas deve ser maior que 0.");

            if (string.IsNullOrWhiteSpace(Descricao))
                Mensagens.Add("Descrição não pode ser nula ou vazia.");

            if (Tensao <= 0)
                Mensagens.Add("Tensão deve ser maior que 0.");

            return Mensagens.Count == 0;
        }
    }
}
