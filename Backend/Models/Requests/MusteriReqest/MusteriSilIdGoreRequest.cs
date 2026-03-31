using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.Musteri
{
    public class MusteriSilIdGoreRequest
    {
        [Required(ErrorMessage = "Müşteri ID zorunludur")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir müşteri ID girin")]
        public int Id { get; set; }
    }
}