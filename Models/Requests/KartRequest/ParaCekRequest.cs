using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests
{
    public class ParaCekRequest
    {
        [Required(ErrorMessage = "Kart numarası zorunludur")]
        public string KartNumara { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "ATM ID zorunludur")]
        [Range(1, int.MaxValue)]
        public int AtmId { get; set; }
        
        [Required(ErrorMessage = "Çekilecek tutar zorunludur")]
        public int CekilecekTutar { get; set; } 
    }
}