using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Entities
{
    public class Kullanici
    {
        [Key]
        public int id { get; set; }
        public String Isim { get; set; } = "İsim değeri girilmedi";
        public String Soyisim { get; set; } = "Soyisim girilmedi";
        public String TelefonNumarasi { get; set; } = "Telefon numarası girilmedi";
        public String Adres { get; set; } = "Adres girilmedi";
        public String Cinsiyet { get; set; } = "Cinsiyet girilmedi";
        public List<KullaniciHesap> KullaniciHesapListesi{ get; set; } = new();
    }
}