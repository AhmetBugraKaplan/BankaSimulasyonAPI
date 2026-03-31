using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests
{
    public class KartSifreGuncelleRequest
    {
        [Required(ErrorMessage = "Kart numarası zorunludur")]
        public string KartNumara { get; set; } = string.Empty;

        [Required(ErrorMessage = "Yeni şifre zorunludur")]
        [Length(1, 4, ErrorMessage = "Şifre 4 haneli olmalı")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Şifre sadece rakamlardan oluşmalı")]
        public string YeniKartSifre { get; set; } = string.Empty;
    }
}