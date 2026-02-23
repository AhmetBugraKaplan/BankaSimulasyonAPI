using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BankaSimulasyon.Models.Entities;

namespace BankaSimulasyon.Models.Entities
{
    public class KullaniciHesap
    {
        [Key]
        public int id { get; set; }
        public int KullaniciId { get; set; }
        public Kullanici kullanici { get; set; } = null!;
        public int HesapNumarasi { get; set; } = 0;
        public decimal Bakiye { get; set; } = 0;
        public String Sifre { get; set; } = null!;
    }
}