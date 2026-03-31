using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests
{
    public class KartGunlukLimitGuncelleRequest
    {
        [Required(ErrorMessage = "Kart numarası zorunludur")]
        public string KartNumara { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Yeni limit zorunludur")]
        public decimal YeniKartLimit { get; set; }
        
        [Required(ErrorMessage = "Müşteri ID zorunludur")]
        [Range(1, int.MaxValue)]
        public int MusteriId { get; set; }
    }
}