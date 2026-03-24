using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.ATM
{
    public class KartDogrulaRequest
    {
        [Required(ErrorMessage = "Kart numarası zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Kart numarası 4 haneli olmalı")]
        public string KartNumara { get; set; } = null!;
        
        [Required(ErrorMessage = "Kart şifresi zorunludur")]
        [Length(1, 4, ErrorMessage = "Şifre 3 haneli olmalı")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Şifre sadece rakamlardan oluşmalı")]
        public string KartSifre { get; set; } = null!;
        
        [Required(ErrorMessage = "ATM ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ATM ID girin")]
        public int AtmId { get; set; }
    }
}