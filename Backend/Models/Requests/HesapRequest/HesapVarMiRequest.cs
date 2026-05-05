using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class HesapVarMiRequest
    {
        [Required(ErrorMessage="Hesap numarasi zorunludur")]
        [RegularExpression(@"^\d{3}$", ErrorMessage="Hesap numarasi 3 haneli olmalidir")]
        public string HesapNumara { get; set; } = string.Empty;
    }
}
