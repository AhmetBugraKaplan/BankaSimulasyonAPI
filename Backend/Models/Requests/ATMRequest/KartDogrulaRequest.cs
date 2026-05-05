using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.ATM
{
    public class KartDogrulaRequest
    {
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string KartNumara { get; set; } = null!;
        [Required(ErrorMessage="Sifre zorunludur")]
        [RegularExpression(@"^\d{3}$", ErrorMessage="Sifre 3 haneli olmalidir")]
        public string KartSifre { get; set; } = null!;
        [Required(ErrorMessage="ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage="Gecerli bir ID giriniz")]
        public int AtmId { get; set; }
    }
}
