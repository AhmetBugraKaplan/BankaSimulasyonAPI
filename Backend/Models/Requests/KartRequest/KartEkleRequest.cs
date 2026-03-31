using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests
{
    public class KartEkleRequest
    {
        [Required(ErrorMessage = "Kullanıcı ID zorunludur")]
        public int KullaniciId { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Kart numara 4 haneli olmalı")]
        public string KartNumara { get; set; } = string.Empty;
        public decimal KartGunlukLimit { get; set; }

        [Required]
        //Bunu düzelticez sonrasında şu anlık 3 haneli şifreler sabit 4 yapıcaz
        [Length(1, 4, ErrorMessage = "Şifre 4 haneli olmalı")]
        public string KartSifre { get; set; } = string.Empty;
    }
}