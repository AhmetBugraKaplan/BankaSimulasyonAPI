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
        
        /*
        public String TelefonNumarasi { get; set; } = "Telefon numarası girilmedi";
        public String Adres { get; set; } = "Adres girilmedi";
        public String Cinsiyet { get; set; } = "Cinsiyet girilmedi";
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string KullaniciRol { get; set; } = null!;
        */ 

    }
}