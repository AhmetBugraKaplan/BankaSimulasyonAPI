using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.Musteri
{
    public class MusteriEkleRequest
    {
        [Required(ErrorMessage = "İsim zorunludur")]
        [StringLength(50, MinimumLength = 2, 
            ErrorMessage = "İsim 2-50 karakter arasında olmalı")]
        public string Isim { get; set; } = null!;
        
        [Required(ErrorMessage = "Soyisim zorunludur")]
        [StringLength(50, MinimumLength = 2, 
            ErrorMessage = "Soyisim 2-50 karakter arasında olmalı")]
        public string Soyisim { get; set; } = null!;
    }
}