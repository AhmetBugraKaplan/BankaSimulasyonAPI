using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.ATM
{
    public class AtmEkleRequest
    {
        [StringLength(100, MinimumLength = 3, 
            ErrorMessage = "Konum 3-100 karakter arasÄ±nda olmalÄ±")]
        public string Konum { get; set; } = null!;
        public bool AktifMi { get; set; }
    }
}
