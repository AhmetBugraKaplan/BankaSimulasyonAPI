using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.ATM
{
    public class AtmEkleRequest
    {
        [Required(ErrorMessage = "ATM konumu zorunludur")]
        [StringLength(100, MinimumLength = 3, 
            ErrorMessage = "Konum 3-100 karakter arasında olmalı")]
        public string Konum { get; set; } = null!;
        
        [Required(ErrorMessage = "Aktif/Pasif durumu belirtilmelidir")]
        public bool AktifMi { get; set; }
    }
}