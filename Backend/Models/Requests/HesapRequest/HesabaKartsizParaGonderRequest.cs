using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class HesabaKartsizParaGonderRequest
{
    [Required(ErrorMessage="Hesap numarasi zorunludur")]
    [RegularExpression(@"^\d{3}$", ErrorMessage="Hesap numarasi 3 haneli olmalidir")]
    public string HesapNumara { get; set; } = string.Empty;
    [Required(ErrorMessage="Tutar zorunludur")]
    [Range(10, int.MaxValue, ErrorMessage="Tutar en az 10 TL olmalidir")]
    public decimal GonderilecekTutar { get; set; }
}
}
