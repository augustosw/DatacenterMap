namespace DatacenterMap.Domain.Entidades
{
    public class Slot : EntidadeBasica
    {

        public int Id { get; set; }
        public bool Ocupado { get; set; }
        public Sala Sala { get; set; }

        public Slot()
        {

        }

        public override bool Validar()
        {
            throw new System.NotImplementedException();
        }
    }
}
