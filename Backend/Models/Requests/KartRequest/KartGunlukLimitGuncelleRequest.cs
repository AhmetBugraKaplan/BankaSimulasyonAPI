using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests
{
    public class KartGunlukLimitGuncelleRequest
    {
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string KartNumara { get; set; } = string.Empty;
        [Required(ErrorMessage="Limit zorunludur")]
        [Range(10, int.MaxValue, ErrorMessage="Limit en az 10 TL olmalidir")]
        public decimal YeniKartLimit { get; set; }
        [Required(ErrorMessage="ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage="Gecerli bir ID giriniz")]
        public int MusteriId { get; set; }
    }
}
