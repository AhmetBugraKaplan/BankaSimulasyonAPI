using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.Musteri
{
    public class MusteriEkleRequest
    {
        [StringLength(50, MinimumLength = 2, 
            ErrorMessage = "Ä°sim 2-50 karakter arasÄ±nda olmalÄ±")]
        [Required(ErrorMessage="Bu alan zorunludur")]
        public string Isim { get; set; } = null!;
        [StringLength(50, MinimumLength = 2, 
            ErrorMessage = "Soyisim 2-50 karakter arasÄ±nda olmalÄ±")]
        [Required(ErrorMessage="Bu alan zorunludur")]
        public string Soyisim { get; set; } = null!;
    }
}
