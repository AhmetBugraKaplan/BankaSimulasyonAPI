using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests
{
    public class KartSifreGuncelleRequest
    {
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string KartNumara { get; set; } = string.Empty;
        [Required(ErrorMessage="Sifre zorunludur")]
        [RegularExpression(@"^\d{3}$", ErrorMessage="Sifre 3 haneli olmalidir")]
        public string YeniKartSifre { get; set; } = string.Empty;
    }
}
