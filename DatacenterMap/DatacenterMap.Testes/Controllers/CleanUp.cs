using DatacenterMap.Infra;

namespace DatacenterMap.Testes.Controllers
{
    public static class CleanUp
    {
        public static void LimparTabelas(DatacenterMapContext context)
        {
            context.Equipamentos.RemoveRange(context.Equipamentos);
            context.Gavetas.RemoveRange(context.Gavetas);
            context.Racks.RemoveRange(context.Racks);
            context.Slots.RemoveRange(context.Slots);
            context.Salas.RemoveRange(context.Salas);
            context.Andares.RemoveRange(context.Andares);
            context.Edificacoes.RemoveRange(context.Edificacoes);
            context.SaveChanges();
        }
    }
}
