using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class ATM
    {
        [Key]
        public int Id { get; set; }
        public string Konum { get; set; } = "Bilinmiyor";
        public bool AktifMi { get; set; } = false;
        // public bool BasariliMi { get; set; }
        public List<AtmKaset> Kasetler { get; set; } = new();
        
    }
}