using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests
{
    public class ParaCekRequest
    {
        
        [Required(ErrorMessage = "Çekilecek tutar zorunludur")]
        public int CekilecekTutar { get; set; } 
    }
}