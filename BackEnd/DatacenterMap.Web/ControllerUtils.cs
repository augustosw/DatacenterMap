using DatacenterMap.Domain.Entidades;
using DatacenterMap.Infra;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
namespace DatacenterMap.Web
{
    public static class ControllerUtils
    {
    
        public static bool AndarHasRackDisponivel(IDatacenterMapContext contexto, int andarId, int tamanhoEquipamento)
        {
            Andar andar = contexto.Andares.Include(x => x.Salas)
                          .AsNoTracking()
                          .FirstOrDefault(x => x.Id == andarId);

            return andar.Salas.Any(x => SalaHasRackDisponivel(contexto, x.Id, tamanhoEquipamento));
        }

        public static bool SalaHasRackDisponivel(IDatacenterMapContext contexto, int salaId, int tamanhoEquipamento)
        {
            List<Slot> slots = contexto.Slots
                          .AsNoTracking()
                          .Include(x => x.Sala)
                          .Where(x => x.Sala.Id == salaId)
                          .ToList();

            List<int> slotsId = new List<int>();
            slots.ForEach(x => slotsId.Add(x.Id));

            List<Rack> racks = contexto.Racks.Include(x => x.Slot)
                          .AsNoTracking()
                          .Where(x => slotsId.Contains(x.Id))
                          .ToList();

            return racks.Any(x => RackIsDisponivel(contexto, x.Id, tamanhoEquipamento));
        }

        public static bool RackIsDisponivel(IDatacenterMapContext contexto, int rackId, int tamanhoEquipamento)
        {
            List<Gaveta> gavetasLivresDoRack = contexto.Gavetas.OrderBy(x => x.Posicao)
                                               .Where(x => x.Rack.Id == rackId && !x.Ocupado)
                                               .ToList();
            if (gavetasLivresDoRack.Count()
                < tamanhoEquipamento) return false; // Se o rack não tiver gavetas disponíveis

            List<Gaveta> gavetasSelecionadas = new List<Gaveta>();
            foreach (Gaveta g in gavetasLivresDoRack)
            {
                // Se a próxima gaveta tem a posição consecutiva em relação a anterior, adiciona a gaveta às gavetas selecionadas
                if (gavetasSelecionadas.Count() == 0 ||
                    gavetasSelecionadas.ElementAt(gavetasSelecionadas.Count() - 1).Posicao + 1 == g.Posicao)
                {
                    gavetasSelecionadas.Add(g);
                }
                // Se não, limpa as gavetas selecionadas e recomeça
                else
                {
                    gavetasSelecionadas.Clear();
                    gavetasSelecionadas.Add(g);
                }

                // Se as gavetas selecionadas forem do tamanho correto, termina o For e retorna o resultado
                if (gavetasSelecionadas.Count() == tamanhoEquipamento)
                {
                    return true;
                }
            }

            return false; // Se as gavetas do rack não estão alinhadas de acordo com a necessidade
        }
    }
}