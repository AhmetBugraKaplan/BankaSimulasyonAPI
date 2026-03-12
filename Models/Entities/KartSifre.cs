using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class KartSifre
    {
        public int id { get; set; }
        //FK-KartId
        public int KartId { get; set; }
        public string SifreHash { get; set; } = "şifre yok";
        public Kart Kart { get; set; } = new();
 

    }
}