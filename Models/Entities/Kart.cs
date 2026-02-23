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
        public int KullaniciHesapId { get; set; }
        public string KartNumara { get; set; } = null!;
        public string KartSKT { get; set; } = null!;
        public string CVV { get; set; } = null!;
        public string KartTipi { get; set; } = null!;
        public bool AktifMi { get; set; } = false;
        public KullaniciHesap kullaniciHesap { get; set; } = null!;

    }
}