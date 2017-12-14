using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace DatacenterMap.Domain.Entidades
{
    public class Andar : EntidadeBasica
    {
        public int Id { get; set; }
        public int NumeroAndar { get; set; }
        public int QuantidadeMaximaSalas { get; set; }
        public Edificacao Edificacao { get; set; }
        public List<Sala> Salas { get; set; }

        public Andar()
        {
            Salas = new List<Sala>();
        }

        public override bool Validar()
        {
            Mensagens.Clear();

            if (QuantidadeMaximaSalas <= 0)
                Mensagens.Add("Quantidade máxima de salas deve ser maior que 0.");

            return Mensagens.Count == 0;
        }
    }
}
