using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class MusteriTumHesaplariGetirRequest
    {
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string KartNumara { get; set; } = string.Empty;
    }
}
