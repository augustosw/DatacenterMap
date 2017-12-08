using System.Collections.Generic;

namespace DatacenterMap.Domain.Entidades
{
    public class Equipamento : EntidadeBasica
    {

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Tamanho { get; set; }
        public int Tensao { get; set; }
        public List<Gaveta> Gavetas { get; set; }

        public Equipamento()
        {
            Gavetas = new List<Gaveta>();
        }

        public override bool Validar()
        {
            Mensagens.Clear();

            if (Tamanho <= 0)
                Mensagens.Add("Tamanho ocupado deve ser maior que 0.");

            if (string.IsNullOrWhiteSpace(Descricao))
                Mensagens.Add("Descrição não pode ser nula ou vazia.");

            if (Tensao <= 0)
                Mensagens.Add("Tensão deve ser maior que 0.");

            return Mensagens.Count == 0;
        }
    }
}
