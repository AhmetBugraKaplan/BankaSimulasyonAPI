using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class Musteri
    {
        [Key]
        public int Id { get; set; }
        public String Isim { get; set; } = "İsim değeri girilmedi";
        public String Soyisim { get; set; } = "Soyisim girilmedi";
        public decimal MusteriLimit { get; set; } = 0;
        public string TelefonNumarasi { get; set; } = string.Empty; 
        public string TckNO { get; set; } = string.Empty;
        public List<Hesap> Hesaplar { get; set; } = new();

    }
}