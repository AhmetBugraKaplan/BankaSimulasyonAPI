// Models/Dtos/Requests/Kart/KartNumaraIleMusteriIdGetirRequest.cs
using System.ComponentModel.DataAnnotations;

namespace BankaSimulasyon.Models.Dtos.Requests.Kart
{
    public class KartNumaraIleMusteriIdGetirRequest
    {
        [Required(ErrorMessage="Kart numarasi zorunludur")]
        [RegularExpression(@"^\d{4}$", ErrorMessage="Kart numarasi 4 haneli olmalidir")]
        public string KartNumara { get; set; }
    }
}
