using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankaSimulasyon.Models.Requests
{
    public class KartEkleRequest
    {
        [Required(ErrorMessage="ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage="Gecerli bir ID giriniz")]
        public int KullaniciId { get; set; }
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string KartNumara { get; set; } = string.Empty;
        [Required(ErrorMessage="Limit zorunludur")]
        [Range(10, int.MaxValue, ErrorMessage="Limit en az 10 TL olmalidir")]
        public decimal KartGunlukLimit { get; set; }
        //Bunu dÃ¼zelticez sonrasÄ±nda ÅŸu anlÄ±k 3 haneli ÅŸifreler sabit 4 yapÄ±caz
        [Required(ErrorMessage="Sifre zorunludur")]
        [RegularExpression(@"^\d{3}$", ErrorMessage="Sifre 3 haneli olmalidir")]
        public string KartSifre { get; set; } = string.Empty;
    }
}
