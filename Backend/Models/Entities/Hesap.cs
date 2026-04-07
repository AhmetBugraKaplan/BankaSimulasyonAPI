using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class Hesap
    {
        [Key]
        public int Id { get; set; }
        public string HesapNumara { get; set; } = string.Empty;
        public decimal HesapBakiye { get; set; }
        //FK
        public int MusteriId { get; set; }
        public Musteri Musteri { get; set; } = new();

        //İleride silicez geçiçi olarka ekledik
        public string ParaBirimi { get; set; }  = string.Empty;
        public string HesapTip { get; set; }  = string.Empty;

    }
}