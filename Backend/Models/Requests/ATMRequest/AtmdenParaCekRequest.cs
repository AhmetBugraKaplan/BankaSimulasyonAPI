using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.ATM
{
    public class AtmdenParaCekRequest
    {
        [Required(ErrorMessage="ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage="Gecerli bir ID giriniz")]
        public int AtmId { get; set; }
        [Required(ErrorMessage="Tutar zorunludur")]
        [Range(10, int.MaxValue, ErrorMessage="Tutar en az 10 TL olmalidir")]
        public int CekilecekTutar { get; set; }
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string KartNumara { get; set; } = null!;
    }
}
