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
                          .Where(x => x.Sala.Id == salaId && x.Ocupado)
                          .ToList();

            List<int> slotsId = new List<int>();
            slots.ForEach(x => slotsId.Add(x.Id));

            List<Rack> racks = contexto.Racks.Include(x => x.Slot)
                          .AsNoTracking()
                          .Where(x => slotsId.Contains(x.Slot.Id))
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

        public static void DeletarEdificacao(IDatacenterMapContext contexto, int edificacaoId)
        {
            Edificacao edificacao = contexto.Edificacoes.Include(x => x.Andares).FirstOrDefault(x => x.Id == edificacaoId);
            List<Andar> andares = edificacao.Andares.ToList();
            andares.ForEach(x => { DeletarAndar(contexto, x.Id); andares = contexto.Andares.Where(e => e.Edificacao.Id == edificacaoId).ToList(); });
            contexto.Edificacoes.Remove(edificacao);
            contexto.SaveChanges();
        }

        public static void DeletarAndar(IDatacenterMapContext contexto, int andarId)
        {
            Andar andar = contexto.Andares.Include(x => x.Salas).FirstOrDefault(x => x.Id == andarId);
            List<Sala> salas = andar.Salas.ToList();
            salas.ForEach(x => { DeletarSala(contexto, x.Id); salas = contexto.Salas.Where(a => a.Andar.Id == andarId).ToList(); });
            contexto.Andares.Remove(andar);
            contexto.SaveChanges();
        }

        public static void DeletarSala(IDatacenterMapContext contexto, int salaId)
        {
            Sala sala = contexto.Salas.Include(x => x.Slots).FirstOrDefault(x => x.Id == salaId);
            List<int> idsSlots = new List<int>();
            sala.Slots.ForEach(x => idsSlots.Add(x.Id));
            List<Rack> racks = contexto.Racks.Include(x => x.Slot).Where(x => idsSlots.Contains(x.Slot.Id)).ToList();
            
            racks.ForEach(x => DeletarRack(contexto, x.Id));
            contexto.Slots.RemoveRange(sala.Slots);
            contexto.Salas.Remove(sala);
            contexto.SaveChanges();
        }

        public static void DeletarRack(IDatacenterMapContext contexto, int rackId)
        {
            Rack rack = contexto.Racks.Include(x => x.Slot).FirstOrDefault(x => x.Id == rackId);
            List<Gaveta> gavetas = contexto.Gavetas.Include(x => x.Equipamento).Include(x => x.Rack).Where(x => x.Rack.Id == rackId).ToList();
            List<int> idsEquipamentos = new List<int>();
            gavetas.Where(x => x.Equipamento != null).ToList().ForEach(x => idsEquipamentos.Add(x.Equipamento.Id));
            List<Equipamento> equipamentos = contexto.Equipamentos.Include(x => x.Gavetas).Where(x => idsEquipamentos.Contains(x.Id)).ToList();
            rack.Slot.Ocupado = false;

            contexto.Equipamentos.RemoveRange(equipamentos);
            contexto.Gavetas.RemoveRange(gavetas);
            contexto.Racks.Remove(rack);
            contexto.SaveChanges();
        }
    }
}