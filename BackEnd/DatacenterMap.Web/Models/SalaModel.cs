namespace DatacenterMap.Web.Models
{
    public class SalaModel
    {
        public int Id { get; set; }
        public string NumeroSala { get; set; }
        public int QuantidadeMaximaSlots { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public int AndarId { get; set; } 
    }
}