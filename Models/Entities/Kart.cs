using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class Kart
    {
        [Key]
        public int id { get; set; }
        public Musteri musteri { get; set; } = null!;
        public int MusteriId { get; set; }
        public string KartNumara { get; set; } = null!;
        public decimal KartGunlukLimit { get; set; } = 0;
        public decimal KartKalanLimit{ get; set; } = 0;
        public KartSifre? KartSifre { get; set; }
    }
}