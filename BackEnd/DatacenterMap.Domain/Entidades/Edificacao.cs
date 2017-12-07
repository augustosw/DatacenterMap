using System.Collections.Generic;

namespace DatacenterMap.Domain.Entidades
{
    public class Edificacao : EntidadeBasica
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public int NumeroAndares { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Andar> Andares { get; set; }

        protected Edificacao()
        {
            Andares = new List<Andar>(); 
        }

        public Edificacao(string nome, int numeroAndares, double longitude, double latitude)
        {
            Nome = nome;
            NumeroAndares = numeroAndares;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override bool Validar()
        {
            Mensagens.Clear();

            if (string.IsNullOrWhiteSpace(Nome))
                Mensagens.Add("Nome é inválido.");

            if (NumeroAndares <= 0)
                Mensagens.Add("Número de andares deve ser maior que 0.");

            if (Latitude > 90 || Latitude < -90)
                Mensagens.Add("Latitude é inválida.");

            if (Longitude > 90 || Longitude < -90)
                Mensagens.Add("Longitude é inválida.");

            return Mensagens.Count == 0;
        }
    }
}
