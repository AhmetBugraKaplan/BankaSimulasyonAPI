using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.ATM
{
    public class AtmKasetGuncelleRequest
    {
        [Required(ErrorMessage = "ATM ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ATM ID girin")]
        public int AtmId { get; set; }
        
        [Required(ErrorMessage = "Slot numarası zorunludur")]
        [Range(1, 4, ErrorMessage = "Slot numarası 1-4 arasında olmalı")]
        public int SlotNumarasi { get; set; }
        
        [Required(ErrorMessage = "Adet zorunludur")]
        [Range(0, 500, ErrorMessage = "Adet 0-500 arasında olmalı")]
        public int Adet { get; set; }
        
        [Required(ErrorMessage = "Kupur zorunludur")]
        [RegularExpression(@"^(5|10|20|50|100|200)$", 
            ErrorMessage = "Uyumsuz küpür")]
        public int Kupur { get; set; }
    }
}