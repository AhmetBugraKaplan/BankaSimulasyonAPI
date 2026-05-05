using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class HavaleYapRequest
    {
        [Required(ErrorMessage="Hesap numarasi zorunludur")]
        [RegularExpression(@"^\d{3}$", ErrorMessage="Hesap numarasi 3 haneli olmalidir")]
        public string GonderenHesapNumara { get; set; } = string.Empty;
        [Required(ErrorMessage="Hesap numarasi zorunludur")]
        [RegularExpression(@"^\d{3}$", ErrorMessage="Hesap numarasi 3 haneli olmalidir")]
        public string AliciHesapNumara { get; set; } = string.Empty;
        [Required(ErrorMessage="Tutar zorunludur")]
        [Range(10, int.MaxValue, ErrorMessage="Tutar en az 10 TL olmalidir")]
        public decimal GonderilenTutar { get; set;}
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string KartNumara { get; set; } = string.Empty;
        [Required(ErrorMessage="ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage="Gecerli bir ID giriniz")]
        public int AtmId { get; set; }
        
        public bool KendiHesaplarimArasiMi { get; set; }
    }
}
