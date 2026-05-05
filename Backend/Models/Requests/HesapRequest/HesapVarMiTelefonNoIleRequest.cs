using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class HesapVarMiTelefonNoIleRequest
    {
        [Required(ErrorMessage="Telefon numarasi zorunludur")]
        [RegularExpression(@"^0\d{10}$", ErrorMessage="Telefon numarasi 0 ile baslamali ve 11 haneli olmalidir")]
        public string TelefonNumara { get; set; } = string.Empty;

    }
}
