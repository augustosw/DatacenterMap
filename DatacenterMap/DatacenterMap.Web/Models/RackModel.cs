namespace DatacenterMap.Web.Models
{
    public class RackModel
    {
        public int Id { get; set; }
        public int QuantidadeGavetas { get; set; }
        public int Tensao { get; set; }
        public string Descricao { get; set; }
        public int SlotId { get; set; }
    }
}