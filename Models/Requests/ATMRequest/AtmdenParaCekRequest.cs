using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.ATM
{
    public class AtmdenParaCekRequest
    {
        [Required(ErrorMessage = "ATM ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ATM ID girin")]
        public int AtmId { get; set; }

        [Required(ErrorMessage = "Çekilecek tutar zorunludur")]
        public int CekilecekTutar { get; set; }

        [Required(ErrorMessage = "Kart numarası zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Kart numarası 4 haneli olmalı")]
        public string KartNumara { get; set; } = null!;
    }
}