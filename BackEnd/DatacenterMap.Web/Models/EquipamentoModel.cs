using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DatacenterMap.Web.Models
{
    public class EquipamentoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Tamanho { get; set; }
        public int Tensao { get; set; }
        public List<int> GavetasId { get; set; }
    }
}