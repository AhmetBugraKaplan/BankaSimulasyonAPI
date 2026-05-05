using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests.HesapRequest
{
    public class CebeParaGonderRequest
    {
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string GonderenKartNo { get; set; } = string.Empty;
        [Required(ErrorMessage="TCKN zorunludur")]
        [RegularExpression(@"^\d{11}$", ErrorMessage="TCKN 11 haneli olmalidir")]
        public string AliciTckNO { get; set; } = string.Empty;
        [Required(ErrorMessage="Telefon numarasi zorunludur")]
        [RegularExpression(@"^0\d{10}$", ErrorMessage="Telefon numarasi 0 ile baslamali ve 11 haneli olmalidir")]
        public string AliciTelNo { get; set; } = string.Empty;
        [Required(ErrorMessage="Tutar zorunludur")]
        [Range(10, int.MaxValue, ErrorMessage="Tutar en az 10 TL olmalidir")]
        public decimal GonderilenTutar { get; set; }
    }
}

//Cebe para gonderirken onay butonu lsun